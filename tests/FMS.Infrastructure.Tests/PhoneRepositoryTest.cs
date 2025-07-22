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

        // PhoneNumberExistsAsync
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

        // GetPhoneByIdAsync
        [Test]
        public async Task GetPhoneByIdAsync_WhenIdExist()
        {
            var existingPhone = await _context.Phones.FirstAsync();
            var result = await _repository.GetPhoneByIdAsync(existingPhone.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<PhoneEditDto>();
            result.Id.Should().Be(existingPhone.Id);
            result.Number.Should().Be(existingPhone.Number);
        }
        [Test]
        public async Task GetPhoneByIdAsync_WhenIdDoesNotExist_ReturnsNull()
        {
            var nonExistingId = Guid.NewGuid();
            var result = await _repository.GetPhoneByIdAsync(nonExistingId);

            result.Should().BeNull();
        }

        // GetPhoneByIdAndContactIdAsync
        [Test]
        public async Task GetPhoneByIdAndContactIdAsync_WhenBothIdExist()
        {
            var existingPhone = await _context.Phones.FirstAsync();
            var result = await _repository.GetPhoneByIdAndContactIdAsync(existingPhone.Id, existingPhone.ContactId);

            result.Should().NotBeNull();
            result.Should().BeOfType<PhoneEditDto>();
            result.Id.Should().Be(existingPhone.Id);
            result.Number.Should().Be(existingPhone.Number);
        }
        [Test]
        public async Task GetPhoneByIdAndContactIdAsync_WhenBothDoNotExist_ReturnsNull()
        {
            var nonExistingId = Guid.NewGuid();
            var nonExistingContactId = Guid.NewGuid();
            var result = await _repository.GetPhoneByIdAndContactIdAsync(nonExistingId, nonExistingContactId);

            result.Should().BeNull();
        }

        // GetPhoneListByContactIdAsync
        [Test]
        public async Task GetPhoneListByContactIdAsync_WhenContactIdExist()
        {
            var existingPhone = await _context.Phones.FirstAsync();
            var results = await _repository.GetPhoneListByContactIdAsync(existingPhone.ContactId);

            results.Should().NotBeNullOrEmpty();
        }

        // CreatePhoneAsync
        public async Task CreatePhoneAsync_CreateNewPhone_WhenDataIsValid()
        {
            var dto = new PhoneCreateDto { Number = "UNIQUE_NUMBER" };

            var newId = await _repository.CreatePhoneAsync(dto);
            var createdPhone = await _context.Phones.FindAsync(newId);

            createdPhone.Should().NotBeNull();
            createdPhone.Number.Should().Be(dto.Number);
        }
        public void CreatePhoneAsync_ThrowArgumentException_WhereNumberAlreadyExist()
        {
            var existingPhone = new Phone { Id = Guid.NewGuid(), Number = "DUPLICATE_NUMBER" };
            _context.Phones.Add(existingPhone);
            _context.SaveChanges();

            var dto = new PhoneCreateDto { Number = "DUPLICATE_NUMBER" };

            Func<Task> action = async () => await _repository.CreatePhoneAsync(dto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Phone Number 'DUPLICATE_NUMBER' already exist.");
        }

        // UpdatePhoneAsync
        [Test]
        public async Task UpdatePhoneAsync_UpdateExistingPhone_WhenDataIsValid()
        {
            var existingPhone = new Phone { Id = Guid.NewGuid(), Number = "ORIGINAL_NUMBER" };
            _context.Phones.Add(existingPhone);
            await _context.SaveChangesAsync();

            var updateDto = new PhoneEditDto { Id = existingPhone.Id, Number = "UPDATED_NAME" };
            await _repository.UpdatePhoneAsync(updateDto);

            var updatedPhone = await _context.Phones.FindAsync(existingPhone.Id);
            updatedPhone.Number.Should().Be("UPDATED_NAME");
        }
        public async Task UpdateContactTypeAsync_ThrowsArgumentException_WhenNumberAlreadyExist()
        {

        }
        // UpdatePhoneStatusAsync

    }
}
