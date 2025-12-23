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
        public async Task SourceStatusNameExistAsync_ReturnsTrue_WhenSourceStatusNameExist()
        {
            var existingSS = await _context.SourceStatuses.Select (ft => ft.Name).FirstAsync();
            var results = await _repository.SourceStatusNameExistsAsync(existingSS);
            results.Should().BeTrue();
        }
        public async Task SourceStatusNameExistAsync_ReturnsFalse_WhenSourceStatusNameDoesNotExist()
        {
            var nonExistingSS = "INVALID_NAME";
            var results = await _repository.SourceStatusNameExistsAsync(nonExistingSS);
            results.Should().BeFalse();
        }

        // SourceStatusDescriptionExistAsync


        // GetSourceStatusAsync


        // GetSourceStatusByNameAsync


        // GetSourceStatusListAsync


        // CreateSourceStatusAsync


        // UpdateSourceStatusAsync


        // UpdateSourceStatusStatusAsync

    }
}
