using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class EventTypeRepositoryTests
    {
        private FmsDbContext _context;
        private EventTypeRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new EventTypeRepository(_context);

            _context.EventTypes.Add(new EventType
            {
                Id = Guid.NewGuid(),
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

        // EventTypeExistAsync
        [Test]
        public async Task EventTypeExistAsync_ReturnsTrue_EventTypeExist()
        {
            var existingET = await _context.EventTypes.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.EventTypeExistsAsync(existingET);
            results.Should().BeTrue();
        }

        [Test]
        public async Task EventTypeExistAsync_ReturnsFalse_EventTypeDoesNotExist()
        {
            var nonExistingET = Guid.NewGuid();
            var results = await _repository.EventTypeExistsAsync(nonExistingET);
            results.Should().BeFalse();
        }


        // EventTypeNameExistAsync
        [Test]
        public async Task EventTypeNameExistAsync_ReturnsTrue_EventTypeNameExist()
        {
            var existingET = new EventType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.EventTypes.Add(existingET);
            await _context.SaveChangesAsync();

            var results = await _repository.EventTypeNameExistsAsync(existingET.Name);
            results.Should().BeTrue();
        }
        [Test]
        public async Task EventTypeNameExistAsync_ReturnsFalse_EventTypeNameDoesNotExist()
        {
            var existingET = new EventType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.EventTypes.Add(existingET);
            await _context.SaveChangesAsync();

            var results = await _repository.EventTypeNameExistsAsync("INVALID_NAME");
            results.Should().BeFalse();
        }


        // GetEventTypeByIdAsync
        [Test]
        public async Task GetEventTypeByIdAsync_ReturnsEventTypeEditEto_WhenIdExist()
        {
            var existingET = await _context.EventTypes.FirstAsync();
            var results = await _repository.GetEventTypeByIdAsync(existingET.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<EventTypeEditDto>();
            results.Id.Should().Be(existingET.Id);
            results.Name.Should().Be(existingET.Name);
        }
        [Test]
        public async Task GetEventTypeByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetEventTypeByIdAsync(nonExistingId);

            results.Should().BeNull();
        }

        // GetEventTypeNameAsync
        [Test]
        public async Task GetEventTypeNameAsync_ReturnsEventTypeName_WhenIdExist()
        {
            var existingET = new EventType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.EventTypes.Add(existingET);
            await _context.SaveChangesAsync();

            var results = await _repository.GetEventTypeNameAsync(existingET.Id);

            results.Should().Be(existingET.Name);
        }
        [Test]
        public async Task GetEventTypeNameAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetEventTypeNameAsync(nonExistingId);
            
            results.Should().BeNull();
        }


        // GetEventTypeListAsync
        [Test]
        public async Task GetEventTypeListAsync_ReturnsAllEventTypes()
        {
            var results = await _repository.GetEventTypeListAsync();
            results.Should().NotBeNullOrEmpty();
        }


        // GetActionTakenListByEventType
        [Test]
        public async Task GetActionTakenListByEventType_ReturnsList_WhenDataIsValid()
        {
            var existingActionTaken = new ActionTaken { Id = Guid.NewGuid(), Name = "VALID_ATNAME" };
            _context.ActionsTaken.Add(existingActionTaken);
            await _context.SaveChangesAsync();

            var existingEventType = new EventType { Id = Guid.NewGuid(), Name = "VALID_ETNAME" };
            _context.EventTypes.Add(existingEventType);
            await _context.SaveChangesAsync();

            var ETSummaryDto = new EventTypeSummaryDto(existingEventType);

            var existingAllowedActionTaken = new AllowedActionTaken
            {
                Id = Guid.NewGuid(),
                EventTypeId = existingEventType.Id,
                ActionTakenId = existingActionTaken.Id
            };
            _context.AllowedActionsTaken.Add(existingAllowedActionTaken);
            await _context.SaveChangesAsync();

            var results = await _repository.GetActionTakenListByEventType(ETSummaryDto);

            results.Should().NotBeNullOrEmpty();
        }


        // CreateEventTypeAsync
        [Test]
        public async Task CreateEventTypeAsync_CreatesEventType_WhenDataIsValid()
        {
            var dto = new EventTypeCreateDto { Name = "VALID_NAME" };

            var newId = await _repository.CreateEventTypeAsync(dto);
            var results = await _repository.GetEventTypeByIdAsync(newId);

            results.Name.Should().Be(dto.Name);

        }


        // UpdateEventTypeAsync
        [Test]
        public async Task UpdateEventTypeAsync_UpdatesEventType_WhenDataIsValid()
        {
            var existingEventType = new EventType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.EventTypes.Add(existingEventType);
            await _context.SaveChangesAsync();

            var updateDto = new EventTypeEditDto { Name = "NEW_NAME" };
            await _repository.UpdateEventTypeAsync(existingEventType.Id, updateDto);

            var updatedEventType = await _context.EventTypes.FindAsync(existingEventType.Id);
            updatedEventType.Name.Should().Be("NEW_NAME");
        }
        [Test]
        public async Task UpdateEventTypeAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new EventTypeEditDto { Name = "NON_EXISTENT" };

            Func<Task> action = async () => await _repository.UpdateEventTypeAsync(invalidId, updateDto);
            await action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateEventTypeStatusAsync
        [Test]
        public async Task UpdateEventTypeStatusAsync_UpdatesStatusCorrectly()
        {
            var eventType = new EventType { Id = Guid.NewGuid(), Name = "VALID_NAME", Active = true };
            _context.EventTypes.Add(eventType);
            await _context.SaveChangesAsync();

            await _repository.UpdateEventTypeStatusAsync(eventType.Id, false);

            var updatedEventType = await _context.EventTypes.FindAsync(eventType.Id);
            updatedEventType.Active.Should().BeFalse();
        }

        [Test]
        public async Task UpdateEventTypeStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateEventTypeStatusAsync(Guid.NewGuid(), false);
            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
