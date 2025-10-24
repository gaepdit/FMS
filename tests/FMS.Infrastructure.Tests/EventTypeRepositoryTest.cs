using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
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
        

        // GetEventTypeNameAsync
        [Test]
        public async Task GetEventTypeByIdAsync_ReturnsEventType_WhenIdExist()
        {
            var existingET = new EventType { Id = Guid.NewGuid(), Name = "VALID_NAME" };
            _context.EventTypes.Add(existingET);
            await _context.SaveChangesAsync();

            var results = await _repository.GetEventTypeByIdAsync(existingET.Id);
            results.Should().NotBeNull();
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


        // GetEventTypeListAsync
        [Test]
        public async Task GetEventTypeListAsync_ReturnsAllEventTypes()
        {
            var results = await _repository.GetEventTypeListAsync();
            results.Should().NotBeNullOrEmpty();
        }


        // GetActionTakenListByEventType



        // CreateEventTypeAsync



        // UpdateEventTypeAsync



        // UpdateEventTypeStatusAsync
    }
}
