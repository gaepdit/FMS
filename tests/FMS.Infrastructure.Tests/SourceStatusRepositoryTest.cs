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
    public class SourceStatusRepositoryTest
    {
        private FmsDbContext _context;
        private SourceStatusRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new SourceStatusRepository(_context);

            _context.SourceStatuses.Add(new SourceStatus
            {
                Id = Guid.NewGuid(),
                Name = "VALID_SS",
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
                _context.Database.EnsureDeleted();
                _context.Dispose();
                _repository.Dispose();
                _disposed = true;
            }
        }

        // SourceStatusExistAsync
        [Test]
        public async Task SourceStatusExistAsync_ReturnTrue_WhenSourceStatusExist()
        {
            var existingSS = await _context.SourceStatuses.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.SourceStatusExistsAsync(existingSS);
            results.Should().BeTrue();
        }
        [Test]
        public async Task SourceStatusExistAsync_ReturnFalse_WhenSourceStatusDoesNotExist()
        {
            var nonExistingSS = Guid.NewGuid();
            var results = await _repository.SourceStatusExistsAsync(nonExistingSS);
            results.Should().BeFalse();
        }

        // SourceStatusNameExistAsync
        [Test]
        public async Task SourceStatusNameExistAsync_ReturnsTrue_WhenSourceStatusNameExist()
        {
            var existingSS = await _context.SourceStatuses.Select (ft => ft.Name).FirstAsync();
            var results = await _repository.SourceStatusNameExistsAsync(existingSS);
            results.Should().BeTrue();
        }
        [Test]
        public async Task SourceStatusNameExistAsync_ReturnsFalse_WhenSourceStatusNameDoesNotExist()
        {
            var nonExistingSS = "INVALID_NAME";
            var results = await _repository.SourceStatusNameExistsAsync(nonExistingSS);
            results.Should().BeFalse();
        }

        // SourceStatusDescriptionExistAsync
        [Test]
        public async Task SourceStatusDescriptionExistAsync_ReturnsTrue_WhenSourceStatusDescriptionExist()
        {
            var existingSS = await _context.SourceStatuses.Select(ft => ft.Description).FirstAsync();
            var results = await _repository.SourceStatusDescriptionExistsAsync(existingSS);
            results.Should().BeTrue();
        }
        [Test]
        public async Task SourceStatusDescriptionExistAsync_ReturnsFalse_WhenSourceStatusDescriptionDoesNotExist()
        {
            var nonExistingSS = "INVALID_Description";
            var results = await _repository.SourceStatusDescriptionExistsAsync(nonExistingSS);
            results.Should().BeFalse();
        }

        // GetSourceStatusAsync
        [Test]
        public async Task GetSourceStatusAsync_ReturnsSourceStatusEditDto_WhenIdExist()
        {
            var existingSS = await _context.SourceStatuses.FirstAsync();
            var result = await _repository.GetSourceStatusAsync(existingSS.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<SourceStatusEditDto>();
            result.Name.Should().Be(existingSS.Name);
            result.Description.Should().Be(existingSS.Description);
        }
        [Test]
        public async Task GetSourceStatusAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingSS = Guid.NewGuid();
            var result = await _repository.GetSourceStatusAsync(nonExistingSS);

            result.Should().BeNull();
        }

        // GetSourceStatusByNameAsync
        [Test]
        public async Task GetSourceStatusByNameAsync_ReturnsSourceStatusEditDto_WhenNameExist()
        {
            var existingSS = await _context.SourceStatuses.FirstAsync();
            var result = await _repository.GetSourceStatusByNameAsync(existingSS.Name);

            result.Should().NotBeNull();
            result.Should().BeOfType<SourceStatusEditDto>();
            result.Id.Should().Be(existingSS.Id);
            result.Description.Should().Be(existingSS.Description);
        }
        [Test]
        public async Task GetSourceStatusByNameAsync_ReturnsNull_WhenNameDoesNotExist()
        {
            var nonExistingSS = "NONEXISTING_NAME";
            var result = await _repository.GetSourceStatusByNameAsync(nonExistingSS);

            result.Should().BeNull();
        }

        // GetSourceStatusListAsync
        [Test]
        public async Task GetSourceStatusListByContactIdAsync_WhenSourceStatusesExist()
        {
            var results = await _repository.GetSourceStatusListAsync();

            results.Should().NotBeNullOrEmpty();
        }

        // CreateSourceStatusAsync
        [Test]
        public async Task CreateSourceStatusAsync_CreateNewSourceStatus_WhenDataIsValid()
        {
            var dto = new SourceStatusCreateDto { Name = "NEW_NAME", Description = "NEW_DESCRIPTION" };

            var resultBool = await _repository.CreateSourceStatusAsync(dto);
            var result = await _repository.GetSourceStatusByNameAsync(dto.Name);

            resultBool.Should().BeTrue();
            result.Name.Should().Be(dto.Name);
            result.Description.Should().Be(dto.Description);
        }
        [Test]
        public void CreateSounceStatusAsync_ThrowArgumentException_WhereSourceStatusAlreadyExist()
        {
            var existingSS = new SourceStatus { Name = "NEW_NAME", Description = "NEW_DESCRIPTION" };
            _context.SourceStatuses.Add(existingSS);
            _context.SaveChanges();

            var dto = new SourceStatusCreateDto { Name = "NEW_NAME", Description = "NEW_DESCRIPTION" };

            Func<Task> action = async () => await _repository.CreateSourceStatusAsync(dto);
            action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateSourceStatusAsync
        [Test]
        public async Task UpdateSourceStatusAsync_UpdateExistingSourceStatus_WhenDataIsValid()
        {
            var existingSS = new SourceStatus { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME", Description = "ORIGINAL_DESCRIPTION" };
            _context.SourceStatuses.Add(existingSS);
            await _context.SaveChangesAsync();

            var updateDto = new SourceStatusEditDto { Id = existingSS.Id, Name = "UPDATED_NAME", Description = "UPDATED_DESCRIPTION" };
            await _repository.UpdateSourceStatusAsync(existingSS.Id, updateDto);

            var updatedSS = await _context.SourceStatuses.FindAsync(existingSS.Id);
            updatedSS.Name.Should().Be("UPDATED_NAME");
            updatedSS.Description.Should().Be("UPDATED_DESCRIPTION");
        }
        [Test]
        public async Task UpdateSourceStatusAsync_ThrowsKeyNotFoundException_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var updateDto = new SourceStatusEditDto { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME", Description = "ORIGINAL_DESCRIPTION" };

            Func<Task> action = async () => await _repository.UpdateSourceStatusAsync(nonExistingId, updateDto);
            await action.Should().ThrowAsync<KeyNotFoundException>();
        }
        //[Test]
        //public async Task UpdateSourceStatusAsync_ThrowsKeyNotFoundException_WhenNameAlreadyExist()
        //{
        //    var existingSS = new SourceStatus { Id = Guid.NewGuid(), Name = "DUPLICATE_NAME", Description = "ORIGINAL_DESCRIPTION" };
        //    _context.SourceStatuses.Add(existingSS);
        //    await _context.SaveChangesAsync();

        //    var updateDto = new SourceStatusEditDto { Id = existingSS.Id, Name = "DUPLICATE_NAME", Description = "UPDATED_DESCRIPTION" };

        //    Func<Task> action = async () => await _repository.UpdateSourceStatusAsync(existingSS.Id, updateDto);
        //    await action.Should().ThrowAsync<KeyNotFoundException>();
        //}

        // UpdateSourceStatusStatusAsync
        [Test]
        public async Task UpdateSourceStatusTypeStatusAsync_UpdatesStatusCorrectly()
        {
            var SourceStatus = new SourceStatus { 
                Id = Guid.NewGuid(),
                Name = "ORIGINAL_NAME",
                Description = "ORIGINAL_DESCRIPTION",
                Active = true
            };
            _context.SourceStatuses.Add(SourceStatus);
            await _context.SaveChangesAsync();

            await _repository.UpdateSourceStatusStatusAsync(SourceStatus.Id, false);

            var updatedSourceStatus = await _context.SourceStatuses.FindAsync(SourceStatus.Id);
            updatedSourceStatus.Active.Should().BeFalse();
        }
        [Test]
        public async Task UpdateSourceStatusStatusAsync_ThrowsKeyNotFoundException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateSourceStatusStatusAsync(Guid.NewGuid(), false);
            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

    }
}
