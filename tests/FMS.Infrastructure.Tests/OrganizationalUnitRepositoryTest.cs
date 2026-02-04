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
    public class OrganizationalUnitRepositoryTest
    {
        private FmsDbContext _context;
        private OrganizationalUnitRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new OrganizationalUnitRepository(_context);

            _context.OrganizationalUnits.Add(new OrganizationalUnit
            {
                Id = Guid.NewGuid(),
                Name = "VALID_ORGUNIT",
                Active = true
            });
            _context.SaveChanges();
        }

        [TearDown]
        public void Teardown()
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

        // OrganizationalUnitExistsAsync
        [Test]
        public async Task OrganizationalUnitExistsAsync_ReturnsTrue_WhenOrganizationalUnitExist()
        {
            var existingOU = await _context.OrganizationalUnits.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.OrganizationalUnitExistsAsync(existingOU);
            results.Should().BeTrue();
        }
        [Test]
        public async Task OrganizationalUnitExistsAsync_ReturnsFalse_WhenIdDoesNotExist()
        {
            var nonexistingOU = Guid.NewGuid();
            var results = await _repository.OrganizationalUnitExistsAsync(nonexistingOU);
            results.Should().BeFalse();
        }

        // OrganizationalUnitNameExistsAsync
        [Test]
        public async Task OrganizationalUnitNameExistsAsync_ReturnsTrue_WhenOrganizationalUnitNameExist()
        {
            var existingOU = new OrganizationalUnit { Id = Guid.NewGuid(), Name = "NEW_NAME" };
            _context.OrganizationalUnits.Add(existingOU);
            await _context.SaveChangesAsync();

            var results = await _repository.OrganizationalUnitNameExistsAsync(existingOU.Name);
            results.Should().BeTrue();
        }
        [Test]
        public async Task OrganizationalUnitNameExistsAsync_ReturnsFalse_WhenOrganizationalUnitNameDoesNotExist()
        {
            var nonExistingOU= "NONEXISTING_NAME";
            var results = await _repository.OrganizationalUnitNameExistsAsync(nonExistingOU);
            results.Should().BeFalse();
        }

        // GetOrganizationalUnitAsync
        [Test]
        public async Task GetOrganizationalUnitAsync_ReturnsOrganizationalUnitEditDto_WhenIdIsValid()
        {
            var existingOU = new OrganizationalUnit { Id = Guid.NewGuid(), Name = "NEW_NAME" };
            _context.OrganizationalUnits.Add(existingOU);
            await _context.SaveChangesAsync();

            var results = await _repository.GetOrganizationalUnitAsync(existingOU.Id);
            results.Should().NotBeNull();
            results.Should().BeOfType<OrganizationalUnitEditDto>();
        }
        [Test]
        public async Task GetOrganizationalUnitAsync_ReturnsNull_WhenIdIsInvalid()
        {
            var invalidId = Guid.NewGuid();

            var results = await _repository.GetOrganizationalUnitAsync(invalidId);
            results.Should().BeNull();
        }

        // GetOrganizationalUnitListAsync
        [Test]
        public async Task GetOrganizationalUnitListAsync_ReturnsAllOrganizationalUnits()
        {
            var results = await _repository.GetOrganizationalUnitListAsync();
            results.Should().NotBeNullOrEmpty();
        }

        // CreateOrganizationalUnitAsync
        [Test]
        public async Task CreateOrganizationalUnitAsync_CreateNewOrganizationalUnit_WhenDataIsValid()
        {
            var dto = new OrganizationalUnitCreateDto { Name = "NEW_NAME" };

            var newId = await _repository.CreateOrganizationalUnitAsync(dto);
            var createdOU= await _context.OrganizationalUnits.FindAsync(newId);

            createdOU.Should().NotBeNull();
            createdOU.Name.Should().Be("NEW_NAME");
        }

        // UpdateOrganizationalUnitAsync


        // UpdateOrganizationalUnitStatusAsync
    }
}
