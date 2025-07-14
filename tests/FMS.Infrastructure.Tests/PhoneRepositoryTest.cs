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
        private PhoneRepositoryTest _repository;
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
        public async Task PhoneNumberExistsAsync_ReturnCorrectPhoneNumber_WhenIdIsValid()
        {
            var PhoneNumber = new Phone
            {
                Id = Guid.NewGuid(),
                Number = "VALID_NUMBER",
                Active = true
            };
            _context.Phones.Add(PhoneNumber);
            await _context.SaveChangesAsync();

            var result = await _repository.PhoneNumberExistsAsync(PhoneNumber.Id);
        }
    }
}
