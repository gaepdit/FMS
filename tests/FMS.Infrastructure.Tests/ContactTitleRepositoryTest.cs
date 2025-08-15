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
    public class ContactTitleRepositoryTest
    {
        private FmsDbContext _context;
        private ContactTitleRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ContactTitleRepository(_context);

            _context.ContactTitles.Add(new ContactTitle
            {
                Id = Guid.NewGuid(),
                Name = "Contact Title",
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

        // ContactTitleExistAsync
        [Test]
        public async Task ContactTitleExistAsync_ReturnTrue_ContactTitleExist()
        {
            var existingCT = await _context.ContactTitles.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.ContactTitleExistsAsync(existingCT);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ContactTitleExistAsync_ReturnFalse_ContactTitleDoesNotExist()
        {
            var nonExistingCT = Guid.NewGuid();
            var results = await _repository.ContactTitleExistsAsync(nonExistingCT);
            results.Should().BeFalse();
        }

        // GetContactTypeByIdAsync
        [Test]
        public async Task GetContactTitleByIdAsync_WhenIdExist()
        {
            var existingCT = await _context.ContactTitles.FirstAsync();
            var result = await _repository.GetContactTitleByIdAsync(existingCT.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<ContactTitleEditDto>();
            result.Id.Should().Be(existingCT.Id);
            result.Name.Should().Be(existingCT.Name);
        }
        [Test]
        public async Task GetContactTitleByIdAsync_WhenIdDoesNotExist_ReturnsNull()
        {
            var nonExistingId = Guid.NewGuid();
            var result = await _repository.GetContactTitleByIdAsync(nonExistingId);

            result.Should().BeNull();
        }

        // GetContactTitleListAsync
        [Test]
        public async Task GetContactTitleListAsync_ReturnsAllContactTitles()
        {
            var results = await _repository.GetContactTitleListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        // CreateContactTitleAsync
        [Test]
        public async Task CreateContactTitleAsync_CreateNewContactTitle_WhenDataIsValid()
        {
            var dto = new ContactTitleCreateDto { Name = "UniqueName" };

            var newId = await _repository.CreateContactTitleAsync(dto);
            var createdContactTitle = await _context.ContactTitles.FindAsync(newId);

            createdContactTitle.Should().NotBeNull();
            createdContactTitle.Name.Should().Be("UniqueName");
        }
        [Test]
        public void CreateContactTitleAsync_ThrowsArgumentException_WhereNameAlreadyExist()
        {
            var existingContactTitle = new ContactTitle { Id = Guid.NewGuid(), Name = "DuplicateName" };
            _context.ContactTitles.Add(existingContactTitle);
            _context.SaveChanges();

            var dto = new ContactTitleCreateDto { Name = "DuplicateName" };

            Func<Task> action = async () => await _repository.CreateContactTitleAsync(dto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Contact Title 'DuplicateName' already exist.");
        }

        // UpdateContactTitleAsync
        [Test]
        public async Task UpdateContactTitleAsync_UpdatesExistingContactTitle_WhenDataIsValid()
        {
            var existingContactTitle = new ContactTitle { Id = Guid.NewGuid(), Name = "OriginalName" };
            _context.ContactTitles.Add(existingContactTitle);
            await _context.SaveChangesAsync();

            var updateDto = new ContactTitleEditDto { Name = "UpdatedName" };
            await _repository.UpdateContactTitleAsync(existingContactTitle.Id, updateDto);

            var updatedContactTitle = await _context.ContactTitles.FindAsync(existingContactTitle.Id);
            updatedContactTitle.Name.Should().Be("UpdatedName");
        }
        [Test]
        public void UpdateContactTitleAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var updateDto = new ContactTitleEditDto { Name = "NonExistent" };

            Func<Task> action = async () => await _repository.UpdateContactTitleAsync(Guid.NewGuid(), updateDto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Contact Title ID not found.");
        }
        [Test]
        public void UpdateContactTitleAsync_ThrowsArgumentException_WhenNameAlreadyExists()
        {
            var contact1 = new ContactTitle { Id = Guid.NewGuid(), Name = "Name1" };
            var contact2 = new ContactTitle { Id = Guid.NewGuid(), Name = "Name2" };
            _context.ContactTitles.AddRange(contact1, contact2);
            _context.SaveChanges();

            var updateDto = new ContactTitleEditDto { Name = "Name2" };

            Func<Task> action = async () => await _repository.UpdateContactTitleAsync(contact1.Id, updateDto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type 'Name2' already exists.");
        }

        // UpdateContactTitleStatusAsync


    }
}
