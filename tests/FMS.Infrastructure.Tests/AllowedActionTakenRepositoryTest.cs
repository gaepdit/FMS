using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Xunit.Extensions.AssertExtensions;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class AllowedActionTakenRepositoryTest
    {
        private FmsDbContext _context;
        private AllowedActionTakenRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new AllowedActionTakenRepository(_context);

            _context.AllowedActionsTaken.Add(new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                EventType = new EventType { Name = "VALID_ET", Id = Guid.NewGuid() },
                ActionTaken = new ActionTaken { Name = "VALID_AT", Id = Guid.NewGuid() },
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

        // AllowedActionTakenExistAsync (id only)
        [Test]
        public async Task AllowedActionTakenExistAsync_ReturnsTrue_WhenAllowedActionTakenExist()
        {
            var existingAAT = await _context.AllowedActionsTaken.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.AllowedActionTakenExistsAsync(existingAAT);
            results.Should().BeTrue();
        }
        [Test]
        public async Task AllowedActionTakenExistAsync_ReturnsFalse_WhenDataIsInvalid()
        {
            var nonExistingAAT = Guid.NewGuid();
            var results = await _repository.AllowedActionTakenExistsAsync(nonExistingAAT);
            results.Should().BeFalse();
        }

        // AllowedActionTakenExistAsync (eventTypeId and actionTakenId)
        public async Task AllowedActionTakenExistAsync_ReturnsTrue_WhenEventTypeIdAndActionTakenIdExist()
        {
            var existingAAT = await _context.AllowedActionsTaken.FirstAsync();
            var results = await _repository.AllowedActionTakenExistsAsync(existingAAT.EventTypeId, existingAAT.ActionTakenId);
            results.Should().BeTrue();
        }
        [Test]
        public async Task AllowedActionTakenExistAsync_ReturnsFalse_WhenEventTypeIdAndActionTakenIdIsInvalid()
        {
            var nonExistingEventTypeId = Guid.NewGuid();
            var nonExistingActionTakenId = Guid.NewGuid();

            var results = await _repository.AllowedActionTakenExistsAsync(nonExistingEventTypeId, nonExistingActionTakenId);
            results.Should().BeFalse();
        }

        // GetAllowedActionTakenByAATIdAsync
        [Test]
        public async Task GetAllowedActionTakenByAATIdAsync_ReturnsAllowedActionTakenSpec_WhenIdExist()
        {
            var existingAAT = await _context.AllowedActionsTaken.FirstAsync();
            var results = await _repository.GetAllowedActionTakenByAATIdAsync(existingAAT.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<AllowedActionTakenSpec>();
        }
        [Test]
        public async Task GetAllowedActionTakenByAATIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonexistingAAT = Guid.NewGuid();
            var results = await _repository.GetAllowedActionTakenByAATIdAsync(nonexistingAAT);
            results.Should().BeNull();
        }

        // GetAllowedActionTakenListAsync
        [Test]
        public async Task GetAllowedActionTakenListAsync_ReturnsAllAllowedActionTypes_WhenEventTypeIdIsValid()
        {
            var existingEventType = new EventType { Name = "VALID_ET", Id = Guid.NewGuid()};

            var existingAAT = new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                EventType = existingEventType,
                ActionTaken = new ActionTaken { Name = "VALID_AT" },
                Active = true
            };
            var existingAAT2 = new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                EventType = existingEventType,
                ActionTaken = new ActionTaken { Name = "VALID_AT2" },
                Active = true
            };
            _context.AllowedActionsTaken.Add(existingAAT);
            _context.AllowedActionsTaken.Add(existingAAT2);

            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var results = await _repository.GetAllowedActionTakenListAsync(existingEventType.Id);
            results.Should().HaveCount(2);
            results.Should().NotBeNullOrEmpty();
        }
        [Test]
        public async Task GetAllowedActionTakenListAsync_ReturnsNull_WhenEventTypeIdIsInvalid()
        {
            var nonExistingETID = Guid.NewGuid();
            var results = await _repository.GetAllowedActionTakenListAsync(nonExistingETID);
            results.Should().BeEmpty();
        }

        // CreateAllowedActionTakenAsync
        [Test]
        public async Task CreateAllowedActionTakenAsync_CreatesAllowedActionTaken_WhenDataIsValid()
        {
            var dto = new AllowedActionTakenSpec
            {
                EventTypeId = Guid.NewGuid(),
                ActionTakenId = Guid.NewGuid(),
                Active = true
            };

            var newAATId = await _repository.CreateAllowedActionTakenAsync(dto);
            var results = await _context.AllowedActionsTaken.FindAsync(newAATId);

            results.Should().NotBeNull();
            results.EventTypeId.Should().Be(dto.EventTypeId);
        }
        [Test]
        public async Task CreateAllowedActionTakenAsync_ReturnsArgumentException_WhenDataAlreadyExist()
        {
            var existingAAT = new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                EventTypeId = Guid.NewGuid(),
                ActionTakenId = Guid.NewGuid(),
                Active = true
            };
            _context.AllowedActionsTaken.Add(existingAAT);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var dto = new AllowedActionTakenSpec
            {
                EventTypeId = existingAAT.EventTypeId,
                ActionTakenId = existingAAT.ActionTakenId,
                Active = true
            };

            Func<Task> action = async () => await _repository.CreateAllowedActionTakenAsync(dto);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // DeleteAllowedActionTakenAsync
        [Test]
        public async Task DeleteAllowedActionTakenAsync_DeletesAllowedActionTaken_WhenIdExist()
        {
            var existingAAT = new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                EventType = new EventType { Name = "VALID_ET", Id = Guid.NewGuid() },
                ActionTaken = new ActionTaken { Name = "VALID_AT", Id = Guid.NewGuid() },
                Active = true

            };
            _context.AllowedActionsTaken.Add(existingAAT);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            await _repository.DeleteAllowedActionTakenAsync(existingAAT.Id);
            var results = await _repository.AllowedActionTakenExistsAsync(existingAAT.Id);

            results.Should().BeFalse();
        }
        [Test]
        public async Task DeleteAllowedActionTakenAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var action = async () => await _repository.DeleteAllowedActionTakenAsync(invalidId);
            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}