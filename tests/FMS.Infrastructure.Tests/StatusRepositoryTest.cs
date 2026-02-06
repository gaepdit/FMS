using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Tests
{
    [TestFixture]
    public class StatusRepositoryTest
    {
        private FmsDbContext _context;
        private StatusRepository _repository;
        private bool _disposed;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid().ToString()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new StatusRepository(_context);


            _context.Statuses.Add(new Status
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
                SourceStatusId = Guid.NewGuid(),
                SourceDate = new DateOnly(2026, 1, 1),
                SoilStatusId = Guid.NewGuid(),
                SoilDate = new DateOnly(2026, 1, 1),
                GroundwaterStatusId = Guid.NewGuid(),
                GroundwaterDate = new DateOnly(2026, 1, 1),
                OverallStatusId = Guid.NewGuid(),
                OverallDate = new DateOnly(2026, 1, 1),
                ISWQS = true,
                FundingSourceId = Guid.NewGuid(),
                LandFill = true,
                SolidWastePermitNumber = "VALID_SWPN",
                GAPSScore = 1,
                GAPSModelDate = new DateOnly(2026, 1, 1),
                GAPSNoOfUnknowns = 1,
                GAPSAssessmentId = Guid.NewGuid(),
                Comments = "VALID_COMMENTS",
                Lien = true,
                FinancialAssurance = true,
                CostEstimate = 1,
                CostEstimateDate = new DateOnly(2026, 1, 1),
                AbandonedInactiveId = Guid.NewGuid(),
                ReportComments = "VALID_RCOMMENTS"
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

        // StatusExistsAsync
        [Test]
        public async Task StatusExistsAsync_ReturnsTrue_WhenStatusExist()
        {
            var existingStatus = await _context.Statuses.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.StatusExistsAsync(existingStatus);
            results.Should().BeTrue();
        }
        [Test]
        public async Task StatusExistsAsync_ReturnsFalse_WhenStatusDoesNotExist()
        {
            var nonExistingStatus = Guid.NewGuid();
            var results = await _repository.StatusExistsAsync(nonExistingStatus);
            results.Should().BeFalse();
        }

        // GetStatusAsync
        [Test]
        public async Task GetStatusAsync_ReturnsStatusEditDto_WhenStatusExist()
        {
            var existingStatus = await _context.Statuses.Select(ft => ft.FacilityId).FirstAsync();
            var results = await _repository.GetStatusAsync(existingStatus);

            results.Should().NotBeNull();
            results.Should().BeOfType<StatusEditDto>();
            results.FacilityId.Should().Be(existingStatus);
        }
        [Test]
        public async Task GetStatusAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetStatusAsync(nonExistingId);
            results.Should().BeNull();
        }

        // CreateStatusAsync


        // UpdateStatusAsync


        // UpdateStatusStatusAsync
    }
}
