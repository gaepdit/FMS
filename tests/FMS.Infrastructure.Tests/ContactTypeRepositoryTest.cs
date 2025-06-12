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

            _context.ContactTypes.Add(new ContactType
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
            var existingCT = await _context.ContactTypes.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.ContactTypeExistsAsync(existingCT);
            results.Should().BeTrue();      
        }
        [Test]
        public async Task ContactTypeExistAsync_ReturnFalse_ContactTypeDoesNotExist()
        {
            var nonExistingCT = Guid.NewGuid();
            var results = await _repository.ContactTypeExistsAsync(nonExistingCT);
            results.Should().BeFalse();
        }

        //GetContactTypeByIdAsync
        [Test]
        public async Task GetContactTypeByIdAsync_WhenIdExist()
        {
            var existingCT = await _context.ContactTypes.FirstAsync();
            var result = await _repository.GetContactTypeByIdAsync(existingCT.Id);

            result.Should().NotBeNull();
            result.Should().BeOfType<ContactTypeEditDto>();
            result.Id.Should().Be(existingCT.Id);
            result.Name.Should().Be(existingCT.Name);
        }
        [Test]
        public async Task GetContactTypeByIdAsync_WhenIdDoesNotExist_ReturnsNull()
        {
            var nonExistingId = Guid.NewGuid();
            var result = await _repository.GetContactTypeByIdAsync(nonExistingId);

            result.Should().BeNull();
        }

        //GetContactTypeListAsync
        [Test]
        public async Task GetContactTypeListAsync_ReturnsAllContactTypes()
        {
            var results = await _repository.GetContactTypeListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        //CreateContactTypeAsync
        [Test]
        public async Task CreateContactTypeAsync_CreateNewContactType_WhenDataIsValid()
        {
            var dto = new ContactTypeCreateDto { Name = "UniqueName" };

            var newId = await _repository.CreateContactTypeAsync(dto);
            var createdContactType = await _context.ContactTypes.FindAsync(newId);

            createdContactType.Should().NotBeNull();
            createdContactType.Name.Should().Be("UniqueName");
        }
        [Test]
        public void CreateContactTypeAsync_ThrowsArgumentException_WhereNameAlreadyExist()
        {
            var existingContactType = new ContactType { Id = Guid.NewGuid(), Name = "DuplicateName" };
            _context.ContactTypes.Add(existingContactType);
            _context.SaveChanges();

            var dto = new ContactTypeCreateDto { Name = "DuplicateName" };

            Func<Task> action = async () => await _repository.CreateContactTypeAsync(dto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Contact Type 'DuplicateName' already exist.");
        }

        //UpdateContactTypeAsync
        [Test]
        public async Task UpdateContactTypeAsync_UpdatesExistingContactType_WhenDataIsValid()
        {
            var existingContactType = new ContactType { Id = Guid.NewGuid(), Name = "OriginalName" };
            _context.ContactTypes.Add(existingContactType);
            await _context.SaveChangesAsync();

            var updateDto = new ContactTypeEditDto { Name = "UpdatedName" };
            await _repository.UpdateContactTypeAsync(existingContactType.Id, updateDto);

            var updatedContactType = await _context.ContactTypes.FindAsync(existingContactType.Id);
            updatedContactType.Name.Should().Be("UpdatedName");
        }
        [Test]
        public void UpdateContactTypeAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var updateDto = new ContactTypeEditDto { Name = "NonExistent" };

            Func<Task> action = async () => await _repository.UpdateContactTypeAsync(Guid.NewGuid(), updateDto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Contact Type ID not found.");
        }
        [Test]
        public void UpdateContactTypeAsync_ThrowsArgumentException_WhenNameAlreadyExists()
        {
            var contact1 = new ContactType { Id = Guid.NewGuid(), Name = "Name1"};
            var contact2 = new ContactType { Id = Guid.NewGuid(), Name = "Name2"};
            _context.ContactTypes.AddRange(contact1, contact2);
            _context.SaveChanges();

            var updateDto = new ContactTypeEditDto { Name = "Name2"};

            Func<Task> action = async () => await _repository.UpdateContactTypeAsync(contact1.Id, updateDto);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Facility Type 'Name2' already exists.");
        }
        //UpdateContactTypeStatusAsync
        public async Task UpdateContactTypeStatusAsync_UpdatesStatusCorrectly()
        {
            var contactType = new ContactType { Id = Guid.NewGuid(), Name = "StatusTest", Active = true };
            _context.ContactTypes.Add(contactType);
            await _context.SaveChangesAsync();

            await _repository.UpdateContactTypeStatusAsync(contactType.Id, false);

            var updatedContactType = await _context.ContactTypes.FindAsync(contactType.Id);
            updatedContactType.Active.Should().BeFalse();
        }

        [Test]
        public void UpdateContactTypeStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            Func<Task> action = async () => await _repository.UpdateContactTypeStatusAsync(Guid.NewGuid(), false);
            action.Should().ThrowAsync<ArgumentException>().WithMessage("Contact Type ID not found");
        }
    }
}
