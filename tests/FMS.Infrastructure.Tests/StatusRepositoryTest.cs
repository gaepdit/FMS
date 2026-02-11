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
        [Test]
        public async Task CreateStatusAsync_CreatesStatus_WithValidData()
        {
            var dto = new StatusCreateDto
            {
                FacilityId = Guid.NewGuid(),
                SourceDate = new DateOnly(2026, 1, 1),
                SoilDate = new DateOnly(2026, 1, 1),
                GroundwaterDate = new DateOnly(2026, 1, 1),
                OverallDate = new DateOnly(2026, 1, 1),
                ISWQS = true,
                LandFill = true,
                SolidWastePermitNumber = "VALID_SWPN",
                GAPSScore = 1,
                GAPSModelDate = new DateOnly(2026, 1, 1),
                GAPSNoOfUnknowns = 1,
                Comments = "VALID_COMMENTS",
                Lien = true,
                FinancialAssurance = true,
                CostEstimate = 1,
                CostEstimateDate = new DateOnly(2026, 1, 1),
                ReportComments = "VALID_RCOMMENTS"
            };
            var newStatus = await _repository.CreateStatusAsync(dto);

            _context.ChangeTracker.Clear();
            var createdStatus = await _context.Statuses.FindAsync(newStatus);

            createdStatus.Should().BeEquivalentTo(dto, o => o
                .Excluding(e => e.SourceStatusId)
                .Excluding(e => e.SoilStatusId)
                .Excluding(e => e.GroundwaterStatusId)
                .Excluding(e => e.OverallStatusId)
                .Excluding(e => e.FundingSourceId));
        }

        // UpdateStatusAsync
        [Test]
        public async Task UpdateStatusAsync_UpdatesExistingStatus_WhenDataIsValid()
        {
            var existingStatus = new Status
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
                Comments = "VALID_COMMENTS",
                Lien = true,
                FinancialAssurance = true,
                GAPSModelDate = new DateOnly(2026, 1, 1),
                GAPSNoOfUnknowns = 1,
                GAPSAssessmentId = Guid.NewGuid(),
                CostEstimate = 1,
                CostEstimateDate = new DateOnly(2026, 1, 1),
                AbandonedInactiveId = Guid.NewGuid(),
                ReportComments = "VALID_RCOMMENTS"
            };
            _context.Statuses.Add(existingStatus);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var updateDto = new StatusEditDto
            {
                Id = existingStatus.Id,
                FacilityId = Guid.NewGuid(),
                SourceStatusId = Guid.NewGuid(),
                SourceDate = new DateOnly(2026, 2, 2),
                SoilStatusId = Guid.NewGuid(),
                SoilDate = new DateOnly(2026, 2, 2),
                GroundwaterStatusId = Guid.NewGuid(),
                GroundwaterDate = new DateOnly(2026, 2, 2),
                OverallStatusId = Guid.NewGuid(),
                OverallDate = new DateOnly(2026, 2, 2),
                ISWQS = false,
                FundingSourceId = Guid.NewGuid(),
                LandFill = false,
                SolidWastePermitNumber = "NEW_SWPN",
                GAPSScore = 1,
                Comments = "NEW_COMMENTS",
                Lien = false,
                FinancialAssurance = false,
                GAPSModelDate = new DateOnly(2026, 2, 2),
                GAPSNoOfUnknowns = 2,
                GAPSAssessmentId = Guid.NewGuid(),
                CostEstimate = 2,
                CostEstimateDate = new DateOnly(2026, 2, 2),
                AbandonedInactiveId = Guid.NewGuid(),
                ReportComments = "NEW_RCOMMENTS"
            };
            await _repository.UpdateStatusAsync(existingStatus.Id, updateDto);
            _context.ChangeTracker.Clear();

            var updatedStatus = await _context.Statuses.FindAsync(existingStatus.Id);
            updatedStatus.Should().BeEquivalentTo(updateDto, o => o.Excluding(e => e.Active));
        }
        [Test]
        public async Task UpdateStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new StatusEditDto { Id = invalidId, FacilityId = Guid.NewGuid() };

            var action = async () => await _repository.UpdateStatusAsync(invalidId, updateDto);

            await action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateStatusStatusAsync
        [Test]
        public async Task UpdateStatusActiveAsync_UpdatesStatusCorrectly()
        {
            var existingStatus = await _context.Statuses.FirstAsync(c => c.Active);

            await _repository.UpdateStatusStatusAsync(existingStatus.Id, false);
            _context.ChangeTracker.Clear();

            var updatedStatus = await _context.Statuses.FindAsync(existingStatus.Id);
            updatedStatus.Should().NotBeNull();
            updatedStatus!.Active.Should().BeFalse();
        }

        [Test]
        public async Task UpdateStatusActiveAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var action = async () => await _repository.UpdateStatusStatusAsync(invalidId, false);
            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
