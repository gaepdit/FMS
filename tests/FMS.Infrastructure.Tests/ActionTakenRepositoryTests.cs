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
    public class ActionTakenRepositoryTests
    {
        private FmsDbContext _context;
        private ActionTakenRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ActionTakenRepository(_context);

            // Seed test data if needed
            _context.ActionsTaken.Add(new ActionTaken
            {
                Id = Guid.NewGuid(),
                Name = "Test Action Taken",
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
        // ActionTakenExistAsync
        [Test]
        public async Task ActionTakenExistAsync_ReturnsTrue_WhenIdIsValid()
        {
            var validAT = new ActionTaken { Id = Guid.NewGuid(), Name = "VALID_NAME" };

            _context.ActionsTaken.Add(validAT);
            await _context.SaveChangesAsync();

            var result = await _repository.ActionTakenExistsAsync(validAT.Id);
            result.Should().BeTrue();
        }
        [Test]
        public async Task ActionTakenExistAsync_ReturnsFalse_WhenIdIsInvalid()
        {
            var invalidId = Guid.NewGuid();

            var result = await _repository.ActionTakenExistsAsync(invalidId);
            result.Should().BeFalse();
        }

        // ActionTakenNameExistsAsync
        [Test]
        public async Task ActionTakenNameExistsAsync_ReturnsTrue_WhenNameIsValid()
        {
            var newAT = new ActionTaken { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.ActionsTaken.Add(newAT);
            await _context.SaveChangesAsync();

            var result = await _repository.ActionTakenNameExistsAsync("VALID_NAME");
            result.Should().BeTrue();
        }
        [Test]
        public async Task ActionTakenNameExistsAsync_ReturnsFalse_WhenNameIsInvalid()
        {
            var newAT = new ActionTaken { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.ActionsTaken.Add(newAT);
            await _context.SaveChangesAsync();

            var result = await _repository.ActionTakenNameExistsAsync("INVALID_NAME");
            result.Should().BeFalse();
        }

        // GetActionTakenAsync
        [Test]
        public async Task GetActionTakenAsync_WhenIdExist()
        {
            var existingAT = await _context.ActionsTaken.FirstAsync();
            var result = await _repository.GetActionTakenAsync(existingAT.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<ActionTakenEditDto>();
            result.Name.Should().Be(existingAT.Name);
        }
        [Test]
        public async Task GetActionTakenAsync_WhenIdDoesNotExist_ReturnNull()
        {
            var nonExistingId = Guid.NewGuid();
            var result = await _repository.GetActionTakenAsync(nonExistingId);

            result.Should().BeNull();
        }

        // CreateActionTakenAsync
        [Test]
        public async Task CreateActionTakenAsync_CreateActionTaken_WhenDataIsValid()
        {
            var dto = new ActionTakenCreateDto { Name = "UNIQUE_NAME" };

            var newId = await _repository.CreateActionTakenAsync(dto);
            var createdAT = await _context.ActionsTaken.FindAsync(newId);

            createdAT.Should().NotBeNull();
            createdAT.Name.Should().Be(dto.Name);
        }
        [Test]
        public async Task CreateActionTakenAsync_ThrowException_WhereNameAlreadyExist()
        {
            var existingAT = new ActionTaken { Id = Guid.NewGuid(), Name = "DUPLICATE_NAME" };
            _context.ActionsTaken.Add(existingAT);
            await _context.SaveChangesAsync();

            var dto = new ActionTakenCreateDto { Name = "DUPLICATE_NAME" };

            Func<Task> action = async () => await _repository.CreateActionTakenAsync(dto);
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("Action Taken " + existingAT.Name + " already exist.");
        }

        // UpdateActionTakenAsync
        [Test]
        public async Task UpdateActionTakenAsync_UpdateExistingPhone_WhenDataIsValid()
        {
            var existingAT = new ActionTaken { Id = Guid.NewGuid(), Name = "ORIGINAL_NAME" };
            _context.ActionsTaken.Add(existingAT);
            await _context.SaveChangesAsync();

            var updateDto = new ActionTakenEditDto { Name = "UPDATED_NAME" };
            await _repository.UpdateActionTakenAsync(existingAT.Id, updateDto);

            var updatedAT = await _context.ActionsTaken.FindAsync(existingAT.Id);
            updatedAT.Name.Should().Be("UPDATED_NAME");
        }
        [Test]
        public async Task UpdateActionTakenAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new ActionTakenEditDto { Name = "NON_EXISTENT" };

            Func<Task> action = async () => await _repository.UpdateActionTakenAsync(invalidId, updateDto);
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("Action Taken ID not found. (Parameter 'id')");
        }
        [Test]
        public async Task UpdateActionTakenAsync_ThrowsArgumentExeption_WhenNameAlreadyExist()
        {
            var existingAT = new ActionTaken { Id = Guid.NewGuid(), Name = "DUPLICATE_NAME" };
            _context.ActionsTaken.Add(existingAT);
            await _context.SaveChangesAsync();

            var existingAT2 = new ActionTaken { Id = Guid.NewGuid(), Name = "FILLER_NAME" };
            _context.ActionsTaken.Add(existingAT2);
            await _context.SaveChangesAsync();

            var updateDto = new ActionTakenEditDto { Name = "DUPLICATE_NAME" };

            Func<Task> action = async () => await _repository.UpdateActionTakenAsync(existingAT2.Id, updateDto);
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("Action Taken Name 'DUPLICATE_NAME' already exist.");
        }

        //UpdateActionTakenStatusAsync
        [Test]
        public async Task UpdateActionTakenStatusAsync_UpdatesStatusCorrectly()
        {
            var existingAT = new ActionTaken { Id = Guid.NewGuid(), Name = "VALID_NAME", Active = true };
            _context.ActionsTaken.Add(existingAT);
            await _context.SaveChangesAsync();

            await _repository.UpdateActionTakenStatusAsync(existingAT.Id, false);

            var updatedAT = await _context.ActionsTaken.FindAsync(existingAT.Id);
            updatedAT.Active.Should().BeFalse();
        }
        [Test]
        public async Task UpdateActionTakenStatusAsync_ThrowsInvalidOperationException_WhenIdDoesNotExist()
        {
            var updateDto = new ActionTaken { Id = Guid.NewGuid(), Name = "VALID_NAME", Active = true };

            Func<Task> action = async () => await _repository.UpdateActionTakenStatusAsync(updateDto.Id, false);
            await action.Should().ThrowAsync<ArgumentException>().WithMessage("Action Taken ID not found");
        }
    }
}

