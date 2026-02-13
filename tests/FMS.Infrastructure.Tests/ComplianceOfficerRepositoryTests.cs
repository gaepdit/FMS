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
    public class ComplianceOfficerRepositoryTests
    {
        private FmsDbContext _context;
        private ComplianceOfficerRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ComplianceOfficerRepository(_context);

            _context.ComplianceOfficers.Add(new ComplianceOfficer
            {
                Id = Guid.NewGuid(),
                GivenName = "VALID_GIVENNAME",
                FamilyName ="VALID_FAMILYNAME",
                Email = "VALID_EMAIL",
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

        // ComplianceOfficerIdExistsAsync
        [Test]
        public async Task ComplianceOfficerIdExistsAsync_ReturnsTrue_WhenIdExist()
        {
            var existingCO = await _context.ComplianceOfficers.Select(e => e.Id).FirstAsync();
            var results = await _repository.ComplianceOfficerIdExistsAsync(existingCO);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ComplianceOfficerIdExistsAsync_ReturnsFalse_WhenIdDoesNotExist()
        {
            var nonExistingCO = Guid.NewGuid();
            var results = await _repository.ComplianceOfficerIdExistsAsync(nonExistingCO);
            results.Should().BeFalse();
        }

        // GetComplianceOfficerAsync
        [Test]
        public async Task GetComplianceOfficerAsync_ReturnsComplianceOfficerDetailDto_WhenIdExist()
        {
            var existingCO = await _context.ComplianceOfficers.FirstAsync();
            var results = await _repository.GetComplianceOfficerAsync(existingCO.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<ComplianceOfficerDetailDto>();
            results.GivenName.Should().Be(existingCO.GivenName);
        }
        [Test]
        public async Task GetComplianceOfficerAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingCO = Guid.NewGuid() ;
            var results = await _repository.GetComplianceOfficerAsync(nonExistingCO);

            results?.Should().BeNull();
        }

        // GetComplianceOfficerListAsync
        [Test]
        public async Task GetComplianceOfficerListAsync_ReturnsAllComplianceOfficers()
        {
            var results = await _repository.GetComplianceOfficerListAsync();
            results.Should().NotBeNullOrEmpty();
        }


        // TryCreateComplianceOfficerAsync


        // UpdateComplianceOfficerStatusAsync


        // 
    }
}
