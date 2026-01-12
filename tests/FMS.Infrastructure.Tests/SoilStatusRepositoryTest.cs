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
using System.Collections.Generic;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class SoilStatusRepositoryTests
    {
        private FmsDbContext _context;
        private SoilStatusRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new SoilStatusRepository(_context);

            _context.SoilStatuses.Add(new SoilStatus
            {
                Id = Guid.NewGuid(),
                Name = "VALID_NAME",
                Description = "VALID_DESCRIPTION",
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
                _context.Database.EnsureCreated();
                _context.Dispose();
                _repository.Dispose();
                _disposed = true;
            }
        }

        // SoilStatusExistsAsync
        [Test]
        public async Task SoilStatusExistAsync_ReturnsTrue_WhenSoilStatusExist()
        {
            var existingSS = await _context.SoilStatuses.Select(e => e.Id).FirstAsync();
            var results = await _repository.SoilStatusExistsAsync(existingSS);
            results.Should().BeTrue();
        }
        [Test]
        public async Task SoilStatusExistAsync_ReturnsFalse_WhenSoilStatusDoesNotExist()
        {
            var nonexistingSS = Guid.NewGuid();
            var results = await _repository.SoilStatusExistsAsync(nonexistingSS);
            results.Should().BeFalse();
        }

        // SoilStatusNameExistsAsync
        [Test]
        public async Task SoilStatusNameExistsAsync_ReturnsTrue_WhenNameExist()
        {
            var existingSS = new SoilStatus { Id = Guid.NewGuid(), Name = "NEW_NAME" };
            _context.SoilStatuses.Add(existingSS);
            await _context.SaveChangesAsync();

            var results = await _repository.SoilStatusNameExistsAsync(existingSS.Name);
            results.Should().BeTrue();
        }
        [Test]
        public async Task SoilStatusNameExistsAsync_ReturnsFalse_WhenNameDoesNotExist()
        {
            var nonexistingSS = "NONEXISTING_NAME";

            var results = await _repository.SoilStatusNameExistsAsync(nonexistingSS);
            results.Should().BeFalse();
        }

        // SoilStatusDescriptionExistsAsync
        [Test]
        public async Task SoilStatusDescriptionExistsAsync_ReturnsTrue_WhenDescriptionExist()
        {
            var existingSS = new SoilStatus { Id = Guid.NewGuid(), Description = "NEW_DESCRIPTION" };
            _context.SoilStatuses.Add(existingSS);
            await _context.SaveChangesAsync();

            var results = await _repository.SoilStatusDescriptionExistsAsync(existingSS.Description);
            results.Should().BeTrue();
        }
        [Test]
        public async Task SoilStatusDescriptionExistsAsync_ReturnsFalse_WhenDescriptionDoesNotExist()
        {
            var nonexistingSS = "NONEXISTING_DESCRIPTION";

            var results = await _repository.SoilStatusDescriptionExistsAsync(nonexistingSS);
            results.Should().BeFalse();
        }

        // GetSoilStatusAsync
        [Test]
        public async Task GetSoilStatusAsync_ReturnsSoilStatusEditDto_WhenSoilStatusExist()
        {
            var existingSS = await _context.SoilStatuses.Select(e => e.Id).FirstAsync();
            var results = await _repository.GetSoilStatusAsync(existingSS);
            results.Should().BeOfType<SoilStatusEditDto>();
        }
        [Test]
        public async Task GetSoilStatusAsync_ReturnsNull_WhenSoilStatusDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var results = await _repository.GetSoilStatusAsync(invalidId);
            results.Should().BeNull();
        }

        // GetSoilStatusByNameAsync
        [Test]
        public async Task GetSoilStatusByNameAsync_ReturnsSoilStatusEditDto_WhenSoilStatusNameExist()
        {
            var existingSS = await _context.SoilStatuses.Select(e => e.Name).FirstAsync();
            var results = await _repository.GetSoilStatusByNameAsync(existingSS);
            results.Should().BeOfType<SoilStatusEditDto>();
        }
        [Test]
        public async Task GetSoilStatusByNameAsync_ReturnsNull_WhenSoilStatusNameDoesNotExist()
        {
            var nonexistingName = "NONEXISTING_NAME";
            var results = await _repository.GetSoilStatusByNameAsync(nonexistingName);
            results.Should().BeNull();
        }

        // GetSoilStatusListAsync
        [Test]
        public async Task GetSoilStatusListAsync_ReturnsAllSoilStatuses()
        {
            var results = await _repository.GetSoilStatusListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        // CreateSoilStatusAsync
        [Test]
        public async Task CreateSoilStatusAsync_CreateNewSoilStatus_WhenDataIsValid()
        {
            var dto = new SoilStatusCreateDto { Name = "NEW_NAME", Description = "NEW_DESCRIPTION"};

            var newId = await _repository.CreateSoilStatusAsync(dto);
            var result = await _repository.GetSoilStatusByNameAsync(dto.Name);

            result.Should().NotBeNull();
            result.Name.Should().Be("NEW_NAME");
            result.Description.Should().Be("NEW_DESCRIPTION");
        }

        // UpdateSoilStatusAsync
        [Test]
        public async Task UpdateSoilStatusAsync_UpdatesExistingSoilStatus_WhenDataIsValid()
        {
            var existingSS= new SoilStatus { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME", Description = "ORIGINAL_DESCRIPTION" };
            _context.SoilStatuses.Add(existingSS);
            await _context.SaveChangesAsync();

            var updateDto = new SoilStatusEditDto { Id = existingSS.Id, Name = "UPDATED_NAME", Description = "UPDATED_DESCRIPTION" };
            await _repository.UpdateSoilStatusAsync(existingSS.Id, updateDto);

            var updatedSS= await _context.SoilStatuses.FindAsync(existingSS.Id);
            updatedSS.Name.Should().Be("UPDATED_NAME");
            updatedSS.Description.Should().Be("UPDATED_DESCRIPTION");

        }
        [Test]
        public async Task UpdateSoilStatusAsync_ThrowsKeyNotFoundException_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var updateDto = new SoilStatusEditDto { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME", Description = "ORIGINAL_DESCRIPTION" };

            Func<Task> action = async () => await _repository.UpdateSoilStatusAsync(nonExistingId, updateDto);
            await action.Should().ThrowAsync<KeyNotFoundException>();
        }
        [Test]
        public async Task UpdateSoilStatusAsync_ThrowsKeyNotFoundException_WhenNameAlreadyExist()
        {
            var existingSS = new SoilStatus { Id = Guid.NewGuid(), Name = "DUPLICATE_NAME", Description = "ORIGINAL_DESCRIPTION" };
            var existingSS2 = new SoilStatus { Id = Guid.NewGuid(), Name = "DUPLICATE_NAME2", Description = "ORIGINAL_DESCRIPTION2" };

            _context.SoilStatuses.Add(existingSS);
            _context.SoilStatuses.Add(existingSS2);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var updateDto = new SoilStatusEditDto { Id = existingSS2.Id, Name = "DUPLICATE_NAME", Description = "UPDATED_DESCRIPTION" };

            Func<Task> action = async () => await _repository.UpdateSoilStatusAsync(existingSS2.Id, updateDto);
            await action.Should().ThrowAsync<ArgumentException>();
        }
        [Test]
        public async Task UpdateSoilStatusAsync_ThrowsKeyNotFoundException_WhenDescriptionAlreadyExist()
        {
            var existingSS = new SoilStatus { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME", Description = "DUPLICATE_DESCRIPTION" };
            var existingSS2 = new SoilStatus { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME2", Description = "DUPLICATE_DESCRIPTION2" };

            _context.SoilStatuses.Add(existingSS);
            _context.SoilStatuses.Add(existingSS2);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var updateDto = new SoilStatusEditDto { Id = existingSS2.Id, Name = "UPDATED_NAME", Description = "DUPLICATE_DESCRIPTION" };

            Func<Task> action = async () => await _repository.UpdateSoilStatusAsync(existingSS2.Id, updateDto);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateSoilStatusStatusAsync
    }
}
