using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using Xunit.Extensions.AssertExtensions;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class ChemicalRepositoryTests
    {
        private FmsDbContext _context;
        private ChemicalRepository _repository;
        private bool _disposed;


        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ChemicalRepository(_context);

            _context.Chemicals.Add(new Chemical
            {
                Id = Guid.NewGuid(),
                CasNo = "VALID_CASNO",
                ChemicalName = "VALID_CHEMNAME",
                CommonName = "VALID_COMNAME",
                ToxValue = "VALID_TOVALUE",
                MCLs = "VALID_MCLS",
                FinalRc = "VALID_FINALRC",
                RQ = "VALID_RQ"
            });

            _context.SaveChanges();
            
        }

        [TearDown]
        public void TearDown()
        {
            Dispose();
        }

        private void Dispose()
        {
            if (!_disposed)
            {
                _context.Dispose();
                _repository.Dispose();
                _disposed = true;
            }
        }

        // ChemicalExistAsync
        [Test]
        public async Task ChemicalExistAsync_ReturnsTrue_WhenChemicalExist()
        {
            var existingChemical = await _context.Chemicals.Select(e => e.Id).FirstAsync();
            var results = await _repository.ChemicalExistsAsync(existingChemical);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ChemicalExistAsync_ReturnsFalse_ContactDoesNotExist()
        {
            var nonExistingChemical = Guid.NewGuid();
            var results = await _repository.ChemicalExistsAsync(nonExistingChemical);
            results.Should().BeFalse();
        }

        // ChemicalCasNoExistsAsync
        [Test]
        public async Task ChemicalCasNoExistAsync_ReturnsTrue_WhenChemicalExist()
        {
            var existingChemical = await _context.Chemicals.Select(e => e.CasNo).FirstAsync();
            var results = await _repository.ChemicalCasNoExistsAsync(existingChemical);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ChemicalCasNoExistAsync_ReturnsFalse_CasNoDoesNotExist()
        {
            var nonExistingChemical = "NONEXISTING_CASNO";
            var results = await _repository.ChemicalCasNoExistsAsync(nonExistingChemical);
            results.Should().BeFalse();
        }

        // ChemicalChemicalNameExistsAsync
        [Test]
        public async Task ChemicalChemicalNameExistAsync_ReturnsTrue_WhenChemicalNameExist()
        {
            var existingChemical = await _context.Chemicals.Select(e => e.ChemicalName).FirstAsync();
            var results = await _repository.ChemicalChemicalNameExistsAsync(existingChemical);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ChemicalChemicalNameExistsAsync_ReturnsFalse_ChemicalNameDoesNotExist()
        {
            var nonExistingChemical = "NONEXISTING_CHEMNAME";
            var results = await _repository.ChemicalChemicalNameExistsAsync(nonExistingChemical);
            results.Should().BeFalse();
        }

        // ChemicalCommonNameExistsAsync
        [Test]
        public async Task ChemicalCommonNameExistsAsync_ReturnsTrue_WhenCommonNameExist()
        {
            var existingChemical = await _context.Chemicals.Select(e => e.CommonName).FirstAsync();
            var results = await _repository.ChemicalCommonNameExistsAsync(existingChemical);
            results.Should().BeTrue();
        }
        [Test]
        public async Task ChemicalCommonNameExistsAsync_ReturnsFalse_CommonNameDoesNotExist()
        {
            var nonExistingChemical = "NONEXISTING_COMMONNAME";
            var results = await _repository.ChemicalCommonNameExistsAsync(nonExistingChemical);
            results.Should().BeFalse();
        }

        // GetChemicalByIdAsync
        [Test]
        public async Task GetChemicalByIdAsync_ReturnsChemicalEditDto_WhenIdExist()
        {
            var existingChemical = await _context.Chemicals.FirstAsync();
            var results = await _repository.GetChemicalByIdAsync(existingChemical.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<ChemicalEditDto>();
        }
        [Test]
        public async Task GetChemicalByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingChemical = Guid.NewGuid();
            var results = await _repository.GetChemicalByIdAsync(nonExistingChemical);
            results.Should().BeNull();
        }


        // GetChemicalByNameAsync not working
        [Test]
        public async Task GetChemicalByNameAsync_ReturnsChemical_WhenNameExist()
        {
            var existingChemical = await _context.Chemicals.FirstAsync();
            var results = await _repository.GetChemicalByNameAsync(existingChemical.Name);

            results.Should().NotBeNull();
            results.Should().BeOfType<Chemical>();
        }
        [Test]
        public async Task GetChemicalByNameAsync_ReturnsNull_WhenNameDoesNotExist()
        {
            var nonExistingChemical = "NONEXISTING_NAME";
            var results = await _repository.GetChemicalByNameAsync(nonExistingChemical);
            results.Should().BeNull();
        }

        // GetChemicalByChemIdAsync
        [Test]
        public async Task GetChemicalByChemIdAsync_ReturnsChemical_WhenIdExist()
        {
            var existingChemical = await _context.Chemicals.FirstAsync();
            var results = await _repository.GetChemicalByChemIdAsync(existingChemical.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<Chemical>();
        }
        [Test]
        public async Task GetChemicalByChemIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingChemical = Guid.NewGuid();
            var results = await _repository.GetChemicalByChemIdAsync(nonExistingChemical);
            results.Should().BeNull();
        }

        // GetChemicalListAsync


        // CreateChemicalAsync


        // UpdateChemicalAsync


        // UpdateChemicalStatusAsync
    }
}