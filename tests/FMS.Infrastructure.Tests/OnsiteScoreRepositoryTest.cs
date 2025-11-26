using AwesomeAssertions;
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
    public class OnsiteScoreRepositoryTest
    {
        private FmsDbContext _context;
        private OnsiteScoreRepository _repository;
        private SubstanceRepository _substanceRepository;
        private bool _disposed;


        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _substanceRepository = new SubstanceRepository(_context);
            _repository = new OnsiteScoreRepository(_context, _substanceRepository);

            _context.OnsiteScores.Add(new OnsiteScore
            {
                Id = Guid.Empty,
                FacilityId = Guid.Empty,
                OnsiteScoreValue = 1.23m
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
                _context.Database.EnsureCreated();
                _context.Dispose();
                _repository.Dispose();
                _substanceRepository.Dispose();
                _disposed = true;
            }
        }

        // OnsiteScoreExistAsync
        [Test]
        public async Task OnsiteScoreExistAsync_ReturnTrue_OnsiteScoreExist()
        {
            var existingCT = await _context.OnsiteScores.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.OnsiteScoreExistsAsync(existingCT);
            results.Should().BeTrue();
        }
        [Test]
        public async Task OnsiteScoreExistAsync_ReturnFalse_OnsiteScoreDoesNotExist()
        {
            var nonExistingCT = Guid.NewGuid();
            var results = await _repository.OnsiteScoreExistsAsync(nonExistingCT);
            results.Should().BeFalse();
        }

        // GetOnsiteScoreByFacilityIdAsync
        [Test]
        public async Task GetOnsiteScoreByIdAsync_WhenFacilityIdExist()
        {
            var existingOSS = await _context.OnsiteScores.FirstAsync();
            var result = await _repository.GetOnsiteScoreByFacilityIdAsync(existingOSS.FacilityId);

            result.Should().NotBeNull();
            result.Should().BeOfType<OnsiteScoreEditDto>();
            result.FacilityId.Should().Be(existingOSS.FacilityId);
        }

        //[Test]
        //public async Task GetOnsiteScoreByIdAsync_WhenFacilityIdDoesNotExist_ReturnsNull()
        //{
        //    var nonExistingId = Guid.NewGuid();
        //    var result = await _repository.GetOnsiteScoreByFacilityIdAsync(nonExistingId);

        //    result.Should().BeNull();
        //}

        // CreateOnsiteScoreAsync
        [Test]
        public async Task CreateOnsiteScoreAsync_CreatesOnsiteScore_WithValidData()
        {
            var dto = new OnsiteScoreCreateDto
            {
                FacilityId = Guid.NewGuid(),
                OnsiteScoreValue = 1.23m,
                A = 1,
                B = 2,
                C = 3
            };

            var newId = await _repository.CreateOnsiteScoreAsync(dto);

            _context.ChangeTracker.Clear();
            var createdOSS = await _context.OnsiteScores.FindAsync(newId);

            createdOSS.Should().NotBeNull();
            createdOSS.OnsiteScoreValue.Should().Be(1.23m);
            createdOSS.A.Should().Be(1);
            createdOSS.B.Should().Be(2);
            createdOSS.C.Should().Be(3);
        }

        // UpdateOnsiteScoreAsync
        [Test]
        public async Task UpdateOnsiteScoreAsync_UpdatesExistingOnsiteScore_WhenDataIsValid()
        {

            var existingOSS = new OnsiteScore
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = Guid.NewGuid(),
                OnsiteScoreValue = 1.23m,
                A = 01,
                B = 02,
                C = 03,
                Description = "VALID_DESCRIPTION",
                ChemName1D = "VALID_CN1D",
                SubstanceId = Guid.NewGuid(),
                Substance = null,
                Other1D = "VALID_O1D",
                D2 = 11,
                D3 = 12,
                CASNO = "VALID_CASNO",
                E1 = 21,
                E2 = 22
            };
            _context.OnsiteScores.Add(existingOSS);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var updateDto = new OnsiteScoreEditDto
            {
                Id = existingOSS.Id,
                Active = false,
                FacilityId = existingOSS.FacilityId,
                OnsiteScoreValue = 3.21m,
                A = 10,
                B = 20,
                C = 30,
                Description = "NEW_DESCRIPTION",
                ChemName1D = "NEW_CN1D",
                SubstanceId = Guid.NewGuid(),
                Substance = null,
                Other1D = "NEW_O1D",
                D2 = 110,
                D3 = 120,
                CASNO = "NEW_CASNO",
                E1 = 210,
                E2 = 220
            };
            await _repository.UpdateOnsiteScoreAsync(updateDto);
            _context.ChangeTracker.Clear();

            var updatedOSS = await _context.OnsiteScores.FindAsync(existingOSS.Id);

            updatedOSS.Should().BeEquivalentTo(updateDto);
        }
        [Test]
        public async Task UpdateOnsiteScoreAsync_ThrowsInvalidOperationException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var updateDto = new OnsiteScoreEditDto { Id = invalidId, Active = true };

            var action = async () => await _repository.UpdateOnsiteScoreAsync(updateDto);

            await action.Should().ThrowAsync<ArgumentException>();
        }

        // UpdateOnsiteScoreStatusAsync
        [Test]
        public async Task UpdateOnsiteScoreStatusAsync_UpdatesStatusCorrectly()
        {
            var existingOnsiteScore = await _context.OnsiteScores.FirstAsync(c => c.Active);

            await _repository.UpdateOnsiteScoreStatusAsync(existingOnsiteScore.Id, false);
            _context.ChangeTracker.Clear();

            var updatedOnsiteScore = await _context.OnsiteScores.FindAsync(existingOnsiteScore.Id);
            updatedOnsiteScore.Should().NotBeNull();
            updatedOnsiteScore!.Active.Should().BeFalse();
        }

        [Test]
        public async Task UpdateOnsiteScoreActiveAsync_ThrowsInvalidOperationException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var action = async () => await _repository.UpdateOnsiteScoreStatusAsync(invalidId, false);
            await action.Should().ThrowAsync<InvalidOperationException>();
        }
    }
}
