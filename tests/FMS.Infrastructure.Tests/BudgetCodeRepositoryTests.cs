using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Domain.Entities;
using FMS.Domain.Dto;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class BudgetCodeRepositoryTests
    {
        private FmsDbContext _context;
        private BudgetCodeRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new BudgetCodeRepository(_context);

            // Seed test data if needed
            _context.BudgetCodes.Add(new BudgetCode
            {
                Id = Guid.NewGuid(),
                Code = "VALID_CODE",
                Name = "VALID_NAME",
                Active = true
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
                _repository.Dispose();
                _disposed = true;
            }
        }

        // GetBudgetCodeAsync
        [Test]
        public async Task GetBudgetCodeAsync_ReturnsCorrectBudgetCode_WhenIdIsValid()
        {
            var budgetCode = new BudgetCode
            {
                Id = Guid.NewGuid(),
                Code = "VALID_CODE",
                Name = "VALID_NAME",
                Active = true
            };
            _context.BudgetCodes.Add(budgetCode);
            await _context.SaveChangesAsync();

            var result = await _repository.GetBudgetCodeAsync(budgetCode.Id);

            result.Should().NotBeNull();
            result.Code.Should().Be("VALID_CODE");
            result.Name.Should().Be("VALID_NAME");
        }

        [Test]
        public async Task GetBudgetCodeAsync_ReturnsNull_WhenIdIsInvalid()
        {
            var result = await _repository.GetBudgetCodeAsync(Guid.NewGuid());
            result.Should().BeNull();
        }

        // GetBudgetCodeListAsync
        [Test]
        public async Task GetBudgetCodeListAsync_ReturnsAllBudgetCodes()
        {
            var budgetCodes = new[]
            {
                new BudgetCode { Id = Guid.NewGuid(), Code = "CODE1", Name = "Budget Code 1", Active = true },
                new BudgetCode { Id = Guid.NewGuid(), Code = "CODE2", Name = "Budget Code 2", Active = true }
            };

            _context.BudgetCodes.AddRange(budgetCodes);
            await _context.SaveChangesAsync();

            var result = await _repository.GetBudgetCodeListAsync();

            result.Should().NotBeNullOrEmpty();
            result.Count.Should().Be(3); // Including the initial seed data
        }

        // CreateBudgetCodeAsync
        [Test]
        public async Task CreateBudgetCodeAsync_Succeeds_WhenDataIsValid()
        {
            var budgetCodeCreateDto = new BudgetCodeCreateDto
            {
                Code = "NEW_CODE",
                Name = "NEW_NAME",
                OrganizationNumber = "ORG123",
                ProjectNumber = "PROJ456"
            };

            var newId = await _repository.CreateBudgetCodeAsync(budgetCodeCreateDto);
            var createdBudgetCode = await _context.BudgetCodes.FindAsync(newId);

            createdBudgetCode.Should().NotBeNull();
            createdBudgetCode.Code.Should().Be("NEW_CODE");
            createdBudgetCode.Name.Should().Be("NEW_NAME");
        }

        [Test]
        public void CreateBudgetCodeAsync_ThrowsException_WhenCodeAlreadyExists()
        {
            var budgetCodeCreateDto = new BudgetCodeCreateDto
            {
                Code = "VALID_CODE", // Name already exists from seed data
                Name = "NEW_NAME",
                OrganizationNumber = "ORG789",
                ProjectNumber = "PROJ101"
            };

            Func<Task> action = async () => await _repository.CreateBudgetCodeAsync(budgetCodeCreateDto);
            action.Should().ThrowAsync<ArgumentException>();
        }

        [Test]
        public void CreateBudgetCodeAsync_ThrowsException_WhenNameAlreadyExists()
        {
            var budgetCodeCreateDto = new BudgetCodeCreateDto
            {
                Code = "NEW_CODE",
                Name = "VALID_NAME", // Name already exists from seed data
                OrganizationNumber = "ORG789",
                ProjectNumber = "PROJ101"
            };

            Func<Task> action = async () => await _repository.CreateBudgetCodeAsync(budgetCodeCreateDto);
            action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateBudgetCodeAsync
        [Test]
        public async Task UpdateBudgetCodeAsync_Succeeds_WhenDataIsValid()
        {
            var existingBudgetCode = await _context.BudgetCodes.FirstAsync();
            var budgetCodeEditDto = new BudgetCodeEditDto(existingBudgetCode)
            {
                Code = "UPDATED_CODE",
                Name = "UPDATED_NAME"
            };

            await _repository.UpdateBudgetCodeAsync(existingBudgetCode.Id, budgetCodeEditDto);
            var updatedBudgetCode = await _context.BudgetCodes.FindAsync(existingBudgetCode.Id);

            updatedBudgetCode.Code.Should().Be("UPDATED_CODE");
            updatedBudgetCode.Name.Should().Be("UPDATED_NAME");
        }

        [Test]
        public void UpdateBudgetCodeAsync_ThrowsException_WhenIdDoesNotExist()
        {
            var budgetCodeEditDto = new BudgetCodeEditDto
            {
                Code = "INVALID_CODE",
                Name = "VALID_NAME"
            };

            Func<Task> action = async () => await _repository.UpdateBudgetCodeAsync(Guid.NewGuid(), budgetCodeEditDto);
            action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateBudgetCodeStatusAsync
        [Test]
        public async Task UpdateBudgetCodeStatusAsync_UpdatesStatusCorrectly()
        {
            // Arrange: Seed the database with a test budget code
            var budgetCode = new BudgetCode
            {
                Id = Guid.NewGuid(),
                Code = "UPDATE_TEST",
                Name = "UPDATE_NAME",
                Active = true
            };
            _context.BudgetCodes.Add(budgetCode);
            await _context.SaveChangesAsync(); // Ensure the changes are saved asynchronously

            // Act: Call the method to update the budget code status
            await _repository.UpdateBudgetCodeStatusAsync(budgetCode.Id, false);

            // Assert: Verify that the status was updated correctly
            var updatedBudgetCode = await _context.BudgetCodes.FindAsync(budgetCode.Id);
            updatedBudgetCode.Should().NotBeNull();
            updatedBudgetCode.Active.Should().BeFalse();
        }
        [Test]
        public async Task GetBudgetCodeAsync_ReturnsNull_WhenGuidIsEmpty()
        {
            // Act
            var result = await _repository.GetBudgetCodeAsync(Guid.Empty);

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task UpdateBudgetCodeAsync_ThrowsArgumentNullException_WhenBudgetCodeUpdatesIsNull()
        {
            // Arrange
            var validId = Guid.NewGuid();

            // Act
            Func<Task> act = async () => await _repository.UpdateBudgetCodeAsync(validId, null);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }
        [Test]
        public async Task CreateBudgetCodeAsync_ThrowsArgumentException_WhenCodeOrNameAlreadyExists()
        {
            // Arrange
            var existingBudgetCode = new BudgetCode
            {
                Id = Guid.NewGuid(),
                Code = "DUPLICATE_CODE",
                Name = "DUPLICATE_NAME",
                Active = true
            };
            _context.BudgetCodes.Add(existingBudgetCode);
            await _context.SaveChangesAsync();

            // Prepare a new budget code with the same Code and Name
            var newBudgetCode = new BudgetCodeCreateDto
            {
                Code = "DUPLICATE_CODE",  // Same code as the existing one
                Name = "DUPLICATE_NAME"  // Same name as the existing one
            };

            // Act
            Func<Task> act = async () => await _repository.CreateBudgetCodeAsync(newBudgetCode);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>();
        }
    }
}
