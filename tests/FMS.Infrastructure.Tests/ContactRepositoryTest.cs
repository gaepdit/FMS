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
                Id = Guid.NewGuid(),
                GivenName = "VALID_GN",
                FamilyName = "VALID_FN",
                Company = "VALID_COMPANY",
                Address = "VALID_ADDRESS",
                City = "VALID_CITY",
                State = "VALID_STATE",
                PostalCode = "VALID_PC",
                Email = "VALID_EMAIL"
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
        [Test]
        public async Task GetContactByIdAsync_ReturnsContact_WhenIdExist()
        {
            var existingContact = await _context.Contacts.FirstAsync();
            var results = await _repository.GetContactByIdAsync(existingContact.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<Contact>();
            results.Id.Should().Be(existingContact.Id);
            results.GivenName.Should().Be(existingContact.GivenName);
        }
        [Test]
        public async Task GetContactByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetContactByIdAsync(nonExistingId);

            results.Should().BeNull();
        }

        // GetContactsByFavilityIdAsync


        // GetContactsByFacilityIdAndTypeAsync


        //CreateContactAsync


        // UpdateContactASync


        // UpdateContactActiveAsync
    }
}
