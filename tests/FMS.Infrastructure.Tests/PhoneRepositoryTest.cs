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
    public class PhoneRepositoryTest
    {
        private FmsDbContext _context;
        private PhoneRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new PhoneRepository(_context);

            _context.Phones.Add(new Phone
            {
                Id = Guid.NewGuid(),
                ContactId = Guid.NewGuid(),
                Number = "NUMBER",
                PhoneType = "TYPE",
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
        
        [Test]
        public async Task PhoneNumberExistsAsync_ReturnTrue_WhenNumberIsValid()
        {
            var PhoneNumber = new Phone
            {
                Id = Guid.NewGuid(),
                ContactId = Guid.NewGuid(),
                Number = "VALID_NUMBER",
                PhoneType = "VALID_TYPE",
                Active = true
            };
            _context.Phones.Add(PhoneNumber);
            await _context.SaveChangesAsync();

            var result = await _repository.PhoneNumberExistsAsync("VALID_NUMBER");

            result.Should().BeTrue();
        }
        [Test]
        public async Task PhoneNumberExistsAsync_ReturnFalse_WhenNumberIsInvalid()
        {
            var PhoneNumber = new Phone
            {
                Id = Guid.NewGuid(),
                ContactId = Guid.NewGuid(),
                Number = "INVALID_NUMBER",
                PhoneType = "VALID_TYPE",
                Active = true
            };
            _context.Phones.Add(PhoneNumber);
            await _context.SaveChangesAsync();

            var result = await _repository.PhoneNumberExistsAsync("VALID_NUMBER");

            result.Should().BeFalse();
        }

    }
}
