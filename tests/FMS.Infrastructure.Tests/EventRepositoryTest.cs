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
    public class EventRepositoryTest
    {
        private FmsDbContext _context;
        private EventRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new EventRepository(_context);

            _context.Events.Add (new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
                ParentId = Guid.NewGuid(),
                Comment = "VALID_COMMENT",
                Active = true,
                
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

        // EventExistAsync
        [Test]
        public async Task EventExistAsync_ReturnsTrue_WhenEventExist()
        {
            var existingEvent = await _context.Events.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.EventExistsAsync(existingEvent);
            results.Should().BeTrue();
        }
        [Test]
        public async Task EventExistAsync_ReturnsFalse_WhenDataIsInvalid()
        {
            var nonExistingEvent = Guid.NewGuid();
            var results = await _repository.EventExistsAsync(nonExistingEvent);
            results.Should().BeFalse();
        }

        // GetEventByIdAsync
        [Test]
        public async Task GetEventByIdAsync_ReturnsEventEditDto_WhenIdExist()
        {
            var existingEvent = await _context.Contacts.FirstAsync();
            var results = await _repository.GetEventByIdAsync(existingEvent.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<EventEditDto>();
            results.Id.Should().Be(existingEvent.Id);
            results.Active.Should().Be(existingEvent.Active);
        }
        [Test]
        public async Task GetEventByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonexistingEvent = Guid.NewGuid();
            var results = await _repository.GetEventByIdAsync(nonexistingEvent);
            results.Should().BeNull();
        }

        // GetEventSummaryByIdAsync



        // GetEventByFacilityIdAsync



        // GetEventByFacilityIdAndParentIdAsync



        // CreateEventAsync



        // UpdateEventAsync



        // UpdateEventStatusAsync



        // DeleteEventByIdAsync
    }
}
