using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class EventTypeRepositoryTests
    {
        private FmsDbContext _context;
        private EventTypeRepository _repository;
        private bool _disposed;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);

            _context.EventTypes.Add(new EventType
            {
                Id = Guid.Empty,
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

        private void Dispose()
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



        // GetEventTypeByIdAsync
        


        // GetEventTypeNameAsync



        // GetEventTypeListAsync



        // GetActionTakenListByEventType



        // CreateEventTypeAsync
        
        
        
        // UpdateEventTypeAsync



        // UpdateEventTypeStatusAsync
    }
}
