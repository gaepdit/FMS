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
    public class ContactTypeRepositoryTest
    {
        private FmsDbContext _context;
        private ContactTypeRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ContactTypeRepository(_context);

            _context.ContactType.Add(new ContactType
            {
                Id = Guid.NewGuid(),
                Name = "Contact Type",
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

        //ContactTypeExistAsync
        [Test]
        public async Task ContactTypeExistAsync_ReturnTrue_ContactTypeExist()
        {
            var existingCT = await _context.ContactType.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.ContactTypeExistsAsync(existingCT);
            results.Should().BeTrue();
        }
    }
}
