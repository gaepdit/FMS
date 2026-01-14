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


        // GroundwaterStatusDescriptionExistsAsync


        // GetGroundwaterStatusListAsync


        // GetGroundwaterStatusNameAsync


        // CreateGroundwaterStatusAsync


        // UpdateGroundwaterStatusAsync


        // UpdateGroundwaterStatusStatusAsync
    }
}