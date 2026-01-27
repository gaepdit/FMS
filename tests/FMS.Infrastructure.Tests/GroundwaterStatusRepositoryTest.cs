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
using FMS.Domain.Repositories;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class GroundwaterStatusRepositoryTests
    {
        private FmsDbContext _context;
        private GroundwaterStatusRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new GroundwaterStatusRepository(_context);

            _context.GroundwaterStatuses.Add(new GroundwaterStatus
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

        // GroundwaterStatusExistsAsync
        [Test]
        public async Task GroundwaterStatusExistsAsync_ReturnsTrue_WhenGroundwaterStatusExist()
        {
            var existingGWS = await _context.GroundwaterStatuses.Select(e => e.Id).FirstAsync();
            var results = await _repository.GroundwaterStatusExistsAsync(existingGWS);
            results.Should().BeTrue();
        }
        [Test]
        public async Task GroundwaterStatusExistAsync_ReturnsFalse_WhenGroundwaterStatusDoesNotExist()
        {
            var nonexistingGWS = Guid.NewGuid();
            var results = await _repository.GroundwaterStatusExistsAsync(nonexistingGWS);
            results.Should().BeFalse();
        }

        // GroundwaterStatusNameExistsAsync
        [Test]
        public async Task GroundwaterStatusNameExistsAsync_ReturnsTrue_WhenNameExist()
        {
            var existingGWS = new GroundwaterStatus { Name = "EXISTING_NAME", Description = "EXISTING_DESCRIPTION" };
            _context.GroundwaterStatuses.Add(existingGWS);
            await _context.SaveChangesAsync();

            var results = await _repository.GroundwaterStatusNameExistsAsync(existingGWS.Name);
            results.Should().BeTrue();
        }
        [Test]
        public async Task GroundwaterStatusNameExistsAsync_ReturnsFalse_WhenNameDoesNotExist()
        {
            var nonexistingGWS = "NONEXISTING_NAME";
            var results = await _repository.GroundwaterStatusNameExistsAsync(nonexistingGWS);
            results.Should().BeFalse();
        }

        // GroundwaterStatusDescriptionExistsAsync
        [Test]
        public async Task GroundwaterStatusDescriptionExistsAsync_ReturnsTrue_WhenDescriptionExist()
        {
            var existingGWS = new GroundwaterStatus { Name = "EXISTING_NAME", Description = "EXISTING_DESCRIPTION" };
            _context.GroundwaterStatuses.Add(existingGWS);
            await _context.SaveChangesAsync();

            var results = await _repository.GroundwaterStatusDescriptionExistsAsync(existingGWS.Description);
            results.Should().BeTrue();
        }
        [Test]
        public async Task GroundwaterStatusDescriptionExistsAsync_ReturnsFalse_WhenDescriptionDoesNotExist()
        {
            var nonexistingGWS = "NONEXISTING_DESCRIPTION";
            var results = await _repository.GroundwaterStatusDescriptionExistsAsync(nonexistingGWS);
            results.Should().BeFalse();
        }
        // GetGroundwaterStatusListAsync
        [Test]
        public async Task GetGroundwaterStatusListAsync_ReturnsAllGroundwaterStatuses()
        {
            var results = await _repository.GetGroundwaterStatusListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        // GetGroundwaterStatusNameAsync
        [Test]
        public async Task GetGroundwaterStatusNameAsync_ReturnsName_WhenNameExist()
        {
            var existingGWS = new GroundwaterStatus { Id = Guid.NewGuid(), Name = "EXISTING_NAME", Description = "EXISTING_DESCRIPTION" };
            _context.GroundwaterStatuses.Add(existingGWS);
            await _context.SaveChangesAsync();

            var results = await _repository.GetGroundwaterStatusNameAsync(existingGWS.Id);
            results.Should().BeEquivalentTo(existingGWS.Name);
        }

        // CreateGroundwaterStatusAsync
        [Test]
        public async Task CreateGroundwaterStatusAsync_CreateNewGroundwaterStatus_WhenDataIsValid()
        {
            var dto = new GroundwaterStatusCreateDto { Name = "NEW_NAME", Description = "NEW_DESCRIPTION" };

            var newId = await _repository.CreateGroundwaterStatusAsync(dto);
            var createdGroundwaterStatus = await _context.GroundwaterStatuses.FindAsync(newId);

            createdGroundwaterStatus.Should().NotBeNull();
            createdGroundwaterStatus.Name.Should().Be("NEW_NAME");
            createdGroundwaterStatus.Description.Should().Be("NEW_DESCRIPTION");
        }

        // UpdateGroundwaterStatusAsync
        [Test]
        public async Task UpdateGroundwaterStatusAsync_UpdatesExistingGroundwaterStatus_WhenDataIsValid()
        {
            var existingGWS= new GroundwaterStatus { Id = Guid.NewGuid(), Name = "VALID_NAME", Description = "VALID_DESCRIPTION" };
            _context.GroundwaterStatuses.Add(existingGWS);
            await _context.SaveChangesAsync();

            var updateDto = new GroundwaterStatusEditDto { Name = "NEW_NAME", Description = "NEW_DESCRIPTION"};
            await _repository.UpdateGroundwaterStatusAsync(existingGWS.Id, updateDto);

            var updatedGWS= await _context.GroundwaterStatuses.FindAsync(existingGWS.Id);
            updatedGWS.Name.Should().Be("NEW_NAME");
            updatedGWS.Description.Should().Be("NEW_DESCRIPTION");

        }
        [Test]
        public async Task UpdateGroundwaterStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new GroundwaterStatusEditDto { Name = "NON_EXISTENT" };

            Func<Task> action = async () => await _repository.UpdateGroundwaterStatusAsync(invalidId, updateDto);
            await action.Should().ThrowAsync<ArgumentException>();
        }


        // UpdateGroundwaterStatusStatusAsync
    }
}