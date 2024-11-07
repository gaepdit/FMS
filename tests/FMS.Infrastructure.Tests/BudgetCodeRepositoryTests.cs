using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NSubstitute;
using System;
using System.Linq;
using System.Threading.Tasks;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Domain.Entities;
using Microsoft.AspNetCore.Http;
using FluentAssertions;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class BudgetCodeRepositoryTests
    {
        private FmsDbContext _context;
        private BudgetCodeRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            // Create a mock IHttpContextAccessor
            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();

            // Pass the mock to the FmsDbContext constructor
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new BudgetCodeRepository(_context);

            // Seed test data if needed
            _context.BudgetCodes.Add(new BudgetCode
            {
                Id = Guid.NewGuid(),
                Code = "TEST",
                Name = "Test Budget Code",
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
                _disposed = true;
            }
        }

        [Test]
        public async Task BudgetCodeExistsAsync_ReturnsTrue_WhenBudgetCodeExists()
        {
            var existingId = await _context.BudgetCodes.Select(b => b.Id).FirstAsync();
            var result = await _repository.BudgetCodeExistsAsync(existingId);
            result.Should().BeTrue();
        }
        [Test]
        public async Task BudgetCodeExistsAsync_ReturnsFalse_WhenBudgetCodeDoesNotExist()
        {
            // Generate a new Guid that does not exist in the database
            var nonExistentId = Guid.NewGuid();

            // Call the method and check the result
            var result = await _repository.BudgetCodeExistsAsync(nonExistentId);

            // Assert that the result is false
            result.Should().BeFalse();

        }
    }
}
