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
        [Test]
        public async Task ContactTypeExistAsync_ReturnFalse_ContactTypeDoesNotExist()
        {
            var nonExistingCT = Guid.NewGuid();
            var results = await _repository.ContactTypeExistsAsync(nonExistingCT);
            results.Should().BeFalse();
        }

        //GetContactTypeByIdAsync

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
            var createdContactType = await _context.ContactType.FindAsync(newId);

            createdContactType.Should().NotBeNull();
            createdContactType.Name.Should().Be("UniqueName");
        }
        [Test]
        public void CreateContactTypeAsync_ThrowsArgumentException_WhereNameAlreadyExist()
        {
            var existingContactType = new ContactType { Id = Guid.NewGuid(), Name = "DuplicateName" };
            _context.ContactType.Add(existingContactType);
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
            _context.ContactType.Add(existingContactType);
            await _context.SaveChangesAsync();

            var updateDto = new ContactTypeEditDto { Name = "UpdatedName" };
            await _repository.UpdateContactTypeAsync(existingContactType.Id, updateDto);

            var updatedContactType = await _context.ContactType.FindAsync(existingContactType.Id);
            updatedContactType.Name.Should().Be("UpdatedName");
        }
        [Test]
        public void UpdateContactTypeAsync_T
        //UpdateContactTypeStatusAsync
    }
}
