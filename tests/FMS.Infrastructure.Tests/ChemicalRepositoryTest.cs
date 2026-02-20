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

        // ChemicalCommonNAmeExistAsync


        // GetChemicalByIdAsync


        // GetChemicalByNameAsync


        // GetChemicalByChemIdAsync


        // GetChemicalListAsync


        // CreateChemicalAsync


        // UpdateChemicalAsync


        // UpdateChemicalStatusAsync
    }
}