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
    public class FacilityStatusRepositoryTests
    {
        private FmsDbContext _context;
        private FacilityStatusRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new FacilityStatusRepository(_context);

            _context.FacilityStatuses.Add(new FacilityStatus
            {
                Id = Guid.NewGuid(),
                Status = "VALID_STATUS",
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

        // FacilityStatusTitleExistAsync
        [Test]
        public async Task FacilityStatusExistAsync_ReturnTrue_WhenFacilityStatusExist()
        {
            var existingFS = await _context.FacilityStatuses.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.FacilityStatusExistsAsync(existingFS);
            results.Should().BeTrue();
        }
        [Test]
        public async Task FacilityStatusExistAsync_ReturnFalse_WhenFacilityStatusDoesNotExist()
        {
            var nonExistingFS = Guid.NewGuid();
            var results = await _repository.FacilityStatusExistsAsync(nonExistingFS);
            results.Should().BeFalse();
        }

        //FacilityStatusStatusExistAsync
        [Test]
        public async Task FacilityStatusStatusExistsAsync_ReturnsTrue_WhenFacilityStatusStatusExist()
        {
            var existingFacilityStatus = new FacilityStatus { Id = Guid.NewGuid(), Status = "NEW_STATUS" };
            _context.FacilityStatuses.Add(existingFacilityStatus);
            await _context.SaveChangesAsync();
            
            var results = await _repository.FacilityStatusStatusExistsAsync(existingFacilityStatus.Status);
            results.Should().BeTrue();
        }
        [Test]
        public async Task FacilityStatusStatusExistsAsync_ReturnsFalse_WhenFacilityStatusStatusDoesNotExist()
        {
            var nonExistingFacilityStatusStatus = "NONEXISTING_STATUS";
            var results = await _repository.FacilityStatusStatusExistsAsync(nonExistingFacilityStatusStatus);
            results.Should().BeFalse();
        }


        //GetFacilityStatusAsync
        [Test]
        public async Task GetFacilityStatusAsync_ReturnsFacilityStatusEditDto_WhenIdIsValid()
        {
            var existingFacilityStatus = new FacilityStatus { Id = Guid.NewGuid(), Status = "NEW_STATUS" };
            _context.FacilityStatuses.Add(existingFacilityStatus);
            await _context.SaveChangesAsync();

            var results = await _repository.GetFacilityStatusAsync(existingFacilityStatus.Id);
            results.Should().NotBeNull();
            results.Should().BeOfType<FacilityStatusEditDto>();
        }
        [Test]
        public async Task GetFacilityStatusAsync_ReturnsNull_WhenIdIsInvalid()
        {
            var invalidId = Guid.NewGuid();

            var results = await _repository.GetFacilityStatusAsync(invalidId);
            results.Should().BeNull();
        }

        // GetFacilityStatusListAsync
        [Test]
        public async Task GetFacilityStatusListAsync_ReturnsAllFacilityStatuses() 
        {
            var results = await _repository.GetFacilityStatusListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        // CreateFacilityStatusAsync
        [Test]
        public async Task CreateFacilityStatusAsync_CreateNewFacilityStatus_WhenDataIsValid()
        {
            var dto = new FacilityStatusCreateDto { Status = "NEW_STATUS" };

            var newId = await _repository.CreateFacilityStatusAsync(dto);
            var createdFacilityStatus = await _context.FacilityStatuses.FindAsync(newId);

            createdFacilityStatus.Should().NotBeNull();
            createdFacilityStatus.Name.Should().Be("NEW_STATUS");
        }

        // UpdateFacilityStatusAsync
        [Test]
        public async Task UpdateFacilityStatusAsync_UpdatesExistingFacilityStatus_WhenDataIsValid()
        {
            var existingFacilityStatus = new FacilityStatus { Id = Guid.NewGuid(), Status = "VALID_STATUS" };
            _context.FacilityStatuses.Add(existingFacilityStatus);
            await _context.SaveChangesAsync();

            var updateDto = new FacilityStatusEditDto { Status = "NEW_STATUS" };
            await _repository.UpdateFacilityStatusAsync(existingFacilityStatus.Id, updateDto);

            var updatedFacilityStatus = await _context.FacilityStatuses.FindAsync(existingFacilityStatus.Id);
            updatedFacilityStatus.Name.Should().Be("NEW_STATUS");
        }
        [Test]
        public async Task UpdateFacilityStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new FacilityStatusEditDto { Status = "NON_EXISTENT" };

            Func<Task> action = async () => await _repository.UpdateFacilityStatusAsync(invalidId, updateDto);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateFacilityStatusStatusAsync
        [Test]
        public async Task UpdateFacilityStatusStatusAsync_UpdatesStatusCorrectly()
        {
            var FacilityStatus = new FacilityStatus { Id = Guid.NewGuid(), Status = "VALID_STATUS", Active = true };
            _context.FacilityStatuses.Add(FacilityStatus);
            await _context.SaveChangesAsync();

            await _repository.UpdateFacilityStatusStatusAsync(FacilityStatus.Id, false);

            var updatedFacilityStatus = await _context.FacilityStatuses.FindAsync(FacilityStatus.Id);
            updatedFacilityStatus.Active.Should().BeFalse();
        }

        [Test]
        public async Task UpdateFacilityStatusStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateFacilityStatusStatusAsync(Guid.NewGuid(), false);
            await action.Should().ThrowAsync<ArgumentException>();
        }

    }
}
