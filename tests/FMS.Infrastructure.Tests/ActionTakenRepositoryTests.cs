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

        // ActionTakenNameExistsAsync
        [Test]
        public async Task ActionTakenNameExistsAsync_ReturnsTrue_WhenNameIsValid()
        {
            var newActionTaken = new ActionTaken
            {
                Id = Guid.NewGuid(),
                Name = "VALID_NAME",
                Active = true
            };
            _context.ActionsTaken.Add(newActionTaken);
            await _context.SaveChangesAsync();

            var result = await _repository.ActionTakenNameExistsAsync("VALID_NAME");
            result.Should().BeTrue();
        }
        [Test]
        public async Task ActionTakenNameExistsAsync_ReturnsFalse_WhenNameIsInvalid()
        {
            var newActionTaken = new ActionTaken
            {
                Id = Guid.NewGuid(),
                Name = "VALID_NAME",
                Active = true
            };
            _context.ActionsTaken.Add(newActionTaken);
            await _context.SaveChangesAsync();

            var result = await _repository.ActionTakenNameExistsAsync("INVALID_NAME");
            result.Should().BeFalse();
        }

        // GetActionTakenAsync


        // GetActionTakenListAsync


        // CreateActionTakenAsync


        // UpdateActionTakenAsync



    }

}

