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
    public class ParcelTypeRepositoryTests
    {
        private FmsDbContext _context;
        private ParcelTypeRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ParcelTypeRepository(_context);

            _context.ParcelTypes.Add(new ParcelType
            {
                Id = Guid.NewGuid(),
                Name = "VALID_PARCELTYPE",
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

        // ParcelTypeExistAsync
        [Test]
        public async Task ParcelTypeExistAsync_ReturnTrue_ParcelTypeExist()
        {
            var existingPT = await _context.ParcelTypes.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.ParcelTypeExistsAsync(existingPT);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ParcelTypeExistAsync_ReturnFalse_ParcelTypeDoesNotExist()
        {
            var nonExistingPT = Guid.NewGuid();
            var results = await _repository.ParcelTypeExistsAsync(nonExistingPT);
            results.Should().BeFalse();
        }

        // ParcelTypeNameExistsAsync
        [Test]
        public async Task ParcelTypeNameExistsAsync_ReturnsTrue_WhenNameIsValid()
        {
            var newPT = new ParcelType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.ParcelTypes.Add(newPT);
            await _context.SaveChangesAsync();

            var result = await _repository.ParcelTypeNameExistsAsync("VALID_NAME");
            result.Should().BeTrue();
        }
        [Test]
        public async Task ParcelTypeNameExistsAsync_ReturnsFalse_WhenNameIsInvalid()
        {
            var newPT = new ParcelType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.ParcelTypes.Add(newPT);
            await _context.SaveChangesAsync();

            var result = await _repository.ParcelTypeNameExistsAsync("INVALID_NAME");
            result.Should().BeFalse();
        }

        // GetParcelTypeListAsync
        [Test]
        public async Task GetParcelTypeListAsync_ReturnsAllParcelTypes()
        {
            var results = await _repository.GetParcelTypeListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        // CreateParcelTypeAsync
        [Test]
        public async Task CreateParcelTypeAsync_CreateNewParcelType_WhenDataIsValid()
        {
            var dto = new ParcelTypeCreateDto { Name = "VALID_PACRCELTYPE" };

            var newId = await _repository.CreateParcelTypeAsync(dto);
            var createdParcelType = await _context.ParcelTypes.FindAsync(newId);

            createdParcelType.Should().NotBeNull();
            createdParcelType.Name.Should().Be("VALID_PACRCELTYPE");
        }

        // UpdateParcelTypeAsync
        [Test]
        public async Task UpdateParcelTypeAsync_UpdatesExistingParcelType_WhenDataIsValid()
        {
            var existingParcelType = new ParcelType { Id = Guid.NewGuid(), Name = "VALID_PARCELTYPE" };
            _context.ParcelTypes.Add(existingParcelType);
            await _context.SaveChangesAsync();

            var updateDto = new ParcelTypeEditDto { Name = "NEW_PARCELTYPE" };
            await _repository.UpdateParcelTypeAsync(existingParcelType.Id, updateDto);

            var updatedParcelType = await _context.ParcelTypes.FindAsync(existingParcelType.Id);
            updatedParcelType.Name.Should().Be("NEW_PARCELTYPE");
        }
        [Test]
        public async Task UpdateParcelTypeAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new ParcelTypeEditDto { Name = "NON_EXISTENT" };

            Func<Task> action = async () => await _repository.UpdateParcelTypeAsync(invalidId, updateDto);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateParcelTypeStatusAsync
        [Test]
        public async Task UpdateParcelTypeStatusAsync_UpdatesStatusCorrectly()
        {
            var parcelType = new ParcelType { Id = Guid.NewGuid(), Name = "VALID_PARCELTYPE", Active = true };
            _context.ParcelTypes.Add(parcelType);
            await _context.SaveChangesAsync();

            await _repository.UpdateParcelTypeStatusAsync(parcelType.Id, false);

            var updatedParcelType = await _context.ParcelTypes.FindAsync(parcelType.Id);
            updatedParcelType.Active.Should().BeFalse();
        }

        [Test]
        public async Task UpdateParcelTypeStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateParcelTypeStatusAsync(Guid.NewGuid(), false);
            await action.Should().ThrowAsync<ArgumentException>();
        }

    }
}
