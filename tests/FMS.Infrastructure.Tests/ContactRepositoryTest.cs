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
using static System.Collections.Specialized.BitVector32;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class ContactRepositoryTest
    {
        private FmsDbContext _context;
        private ContactRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
            .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
            .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ContactRepository(_context);

            _context.Contacts.Add(new Contact
            {
                Id = Guid.Empty,
                GivenName = "VALID_GN",
                FamilyName = "VALID_FN",
                ContactTitleId = Guid.Empty,
                ContactTypeId = Guid.Empty,
                Company = "VALID_COMPANY",
                Address = "VALID_ADDRESS",
                City = "VALID_CITY",
                State = "VALID_STATE", 
                PostalCode = "VALID_PC",
                Email = "VALID_EMAIL"
            });
            _context.SaveChanges();
            _repository = new ContactRepository(_context);

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

        // ContactExistAsync
        [Test]
        public async Task ContactExistAsync_ReturnsTrue_ContactExist()
        {
            var existingContact = await _context.Contacts.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.ContactExistsAsync(existingContact);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ContactExistAsync_ReturnsFalse_ContactDoesNotExist()
        {
            var nonExistingContact = Guid.NewGuid();
            var results = await _repository.ContactExistsAsync(nonExistingContact);
            results.Should().BeFalse();
        }

        // GetContactByIdAsync
        /*[Test] 
        public async Task GetContactByIdAsync_ReturnsContactEditDto_WhenIdExist()
        {
            var existingContact = await _context.Contacts.FirstAsync();
            var results = await _repository.GetContactByIdAsync(existingContact.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<ContactEditDto>();
            results.Id.Should().Be(existingContact.Id);
            results.GivenName.Should().Be(existingContact.GivenName);
        }*/

        [Test]
        public async Task GetContactByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetContactByIdAsync(nonExistingId);

            results.Should().BeNull();
        }

        // GetContactsByFacilityIdAsync
        /*[Test]
        public async Task GetContactsByFacilityIdAsync_ReturnsContact_WhenFacilityIdExist()
        {
            var existingContact = new Contact
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
                Company = "VALID_COMPANY",
                Active = true,
            };
            _context.Contacts.Add(existingContact);
            await _context.SaveChangesAsync();

            var results = await _repository.GetContactsByFacilityIdAsync(existingContact.FacilityId);

            results.Should().NotBeNull();
            results.Should().HaveCount(1);

        }*/
        [Test]
        public async Task GetContactsByFacilityIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetContactsByFacilityIdAsync(nonExistingId);

            results.Should().BeEmpty();
        }

        // GetContactsByFacilityIdAndTypeAsync
        /*[Test]
        public async Task GetContactsByFacilityIdAndTypeAsync_ReturnsContact_WhenFacilityIdExist()
        {
            var existingContact = new Contact
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
                ContactTypeId = Guid.NewGuid(),
                Company = "VALID_COMPANY",
                Active = true,
            };
            _context.Contacts.Add(existingContact);
            await _context.SaveChangesAsync();

            var results = await _repository.GetContactsByFacilityIdAndTypeAsync(existingContact.FacilityId, existingContact.ContactTypeId);

            results.Should().NotBeNull();
            results.Should().HaveCount(1);
        }*/
        [Test]
        public async Task GetContactsByFacilityIdAndTypeAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingFacilityId = Guid.NewGuid();
            var nonExistingContactTypeId = Guid.NewGuid();
            var results = await _repository.GetContactsByFacilityIdAndTypeAsync(nonExistingFacilityId, nonExistingContactTypeId);

            results.Should().BeEmpty();
        }

        // CreateContactAsync
        [Test]
        public async Task CreateContactAsync_CreatesContact_WithValidData()
        {
            var dto = new ContactCreateDto
            {
                Id = Guid.NewGuid(),
                FamilyName = "VALID_FN",
                Address = "VALID_ADDRESS",
                Email = "VALID_EMAIL"
            };

            var newId = await _repository.CreateContactAsync(dto);
            var createdContact = await _context.Contacts.FindAsync(newId);

            createdContact.Should().NotBeNull();
            createdContact.FamilyName.Should().Be("VALID_FN");
            createdContact.Email.Should().Be("VALID_EMAIL");
        }

        // UpdateContactAsync
        [Test]
        public async Task UpdateContactsAsync_UpdatesExistingContact_WhenDataIsValid()
        {
            var existingContact = new Contact
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
                FamilyName = "VALID_FN",
                GivenName = "VALID_GN",
                ContactTitleId = Guid.NewGuid(),
                ContactTypeId = Guid.NewGuid(),
                Company = "VALID_COMPANY",
                Address = "VALID_ADDRESS",
                City = "VALID_CITY",
                State = "VALID_STATE",
                PostalCode = "VALID_PC",
                Email = "VALID_EMAIL",
                Active = true,
            };
            _context.Contacts.Add(existingContact);
            await _context.SaveChangesAsync();

            var updateDto = new ContactEditDto
            {
                Id = existingContact.Id,
                FamilyName = "NEW_FN",
                GivenName = "NEW_GN",
                ContactTitleId = Guid.NewGuid(),
                ContactTypeId = Guid.NewGuid(),
                Company = "NEW_COMPANY",
                Address = "NEW_ADDRESS",
                City = "NEW_CITY",
                State = "NEW_STATE",
                PostalCode = "NEW_PC",
                Email = "NEW_EMAIL",
                Active = false,
            };
            await _repository.UpdateContactAsync(updateDto);
            _context.ChangeTracker.Clear();
            var updatedContact = await _context.Contacts.FindAsync(existingContact.Id);

            updatedContact.Should().BeEquivalentTo(updateDto, options => options
                .Excluding(e => e.FacilityId)
                .Excluding(e => e.Status));

        }
        [Test]
        public async Task UpdateContactsAsync_ThrowsInvalidOperationException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new ContactEditDto { Id = invalidId, FamilyName = "NEW_FN" };

            Func<Task> action = async () => await _repository.UpdateContactAsync(updateDto);
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("Contact with ID " + invalidId + " does not exist.");
        }

        // UpdateContactActiveAsync
        [Test]
        public async Task UpdateContactActiveAsync_UpdatesStatusCorrectly()
        {
            var existingContact = await _context.Contacts.FirstAsync(c => c.Active);

            await _repository.UpdateContactActiveAsync(existingContact.Id, false);

            var updatedContact = await _context.Contacts.FindAsync(existingContact.Id);
            updatedContact.Active.Should().BeFalse();
        }
        [Test]
        public async Task UpdateContactActiveAsync_ThrowsInvalidOperationException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            Func<Task> action = async ()=> await _repository.UpdateContactActiveAsync(invalidId, false);
            await action.Should().ThrowAsync<InvalidOperationException>().WithMessage("Contact with ID " + invalidId + " does not exist.");

        }
    }
}
