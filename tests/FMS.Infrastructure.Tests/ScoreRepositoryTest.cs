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
    public class ScoreRepositoryTests
    {
        private FmsDbContext _context;
        private ScoreRepository _repository;
        private bool _disposed = false;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<FmsDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;
            var httpContextAccessor = Substitute.For<HttpContextAccessor>();
            _context = new FmsDbContext(options, httpContextAccessor);
            _repository = new ScoreRepository(_context);

            _context.Scores.Add(new Score
            {
                Id = Guid.NewGuid(),
                FacilityId = Guid.NewGuid(),
                ScoredDate = new DateOnly(2025, 1, 1),
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

        // ScoreExistAsync
        [Test]
        public async Task ScoreExistAsync_ReturnsTrue_ScoreExist()
        {
            var existingScore = await _context.Scores.Select(ft => ft.Id).FirstAsync();
            var results = await _repository.ScoreExistsAsync(existingScore);
            results.Should().BeTrue();
        }

        [Test]
        public async Task ScoreExistAsync_ReturnsFalse_ScoreDoesNotExist()
        {
            var nonExistingScore = Guid.NewGuid();
            var results = await _repository.ScoreExistsAsync(nonExistingScore);
            results.Should().BeFalse();
        }


        // GetScoreByIdAsync
        [Test]
        public async Task GetScoreByIdAsync_ReturnsScoreEditDto_WhenIdExist()
        {
            var existingScore = await _context.Scores.FirstAsync();
            var results = await _repository.GetScoreByIdAsync(existingScore.Id);

            results.Should().NotBeNull();
            results.Should().BeOfType<ScoreEditDto>();
            results.Id.Should().Be(existingScore.Id);
            results.Active.Should().Be(existingScore.Active);
        }

        [Test]
        public async Task GetScoreByIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetScoreByIdAsync(nonExistingId);

            results.Should().BeNull();
        }

        // GetScoreEditByFacilityIdAsync
        [Test]
        public async Task GetScoreByIdAsync_WhenIdExist()
        {
            var existingScore = await _context.Scores.FirstAsync();
            var result = await _repository.GetScoreEditByFacilityIdAsync(existingScore.FacilityId);

            result.Should().NotBeNull();
            result.Should().BeOfType<ScoreEditDto>();
            result.FacilityId.Should().Be(existingScore.FacilityId);
            result.Active.Should().Be(existingScore.Active);
        }
        [Test]
        public async Task GetScoreByIdAsync_WhenIdDoesNotExist_ReturnsNull()
        {
            var nonExistingId = Guid.NewGuid();
            var result = await _repository.GetScoreEditByFacilityIdAsync(nonExistingId);

            result.Should().BeNull();
        }

        // GetScoreByFacilityIdAsync
        [Test]
        public async Task GetScoreByFacilityIdAsync_ReturnsScore_WhenIdExist()
        {
            var existingScore = await _context.Scores.FirstAsync();
            var results = await _repository.GetScoreByIdAsync(existingScore.Id);

            results.Should().NotBeNull();
            results.FacilityId.Should().Be(existingScore.FacilityId);
        }

        [Test]
        public async Task GetScoreByFacilityIdAsync_ReturnsNull_WhenIdDoesNotExist()
        {
            var nonExistingId = Guid.NewGuid();
            var results = await _repository.GetScoreByFacilityIdAsync(nonExistingId);

            results.Should().BeEmpty();
        }

        // CreateScoreAsync
        [Test]
        public async Task CreateScoreAsync_CreatesScore_WithValidData()
        {
            var dto = new ScoreCreateDto
            {
                FacilityId = Guid.NewGuid(),
                ScoredDate = new DateOnly(2025, 1, 1),
                Comments = "VALID_COMMENTS",
                UseComments = true
            };

            var newId = await _repository.CreateScoreAsync(dto);

            _context.ChangeTracker.Clear();
            var createdScore = await _context.Scores.FindAsync(newId);

            createdScore.Should().NotBeNull();
            createdScore!.ScoredDate.Should().Be(dto.ScoredDate);
            createdScore.Comments.Should().Be(dto.Comments);
        }

        // UpdateScoreAsync
        [Test]
        public async Task UpdateScoresAsync_UpdatesExistingScore_WhenDataIsValid()
        {
            var existingScore = new Score
            {
                Active = true,
                FacilityId = Guid.NewGuid(),
                ScoredDate = new DateOnly(2025, 1, 1),
                ScoredById = Guid.NewGuid(),
                Comments = "VALID_COMMENTS",
                UseComments = true
            };
            _context.Scores.Add(existingScore);
            await _context.SaveChangesAsync();
            _context.ChangeTracker.Clear();

            var updateDto = new ScoreEditDto
            {
                Active = false,
                FacilityId = Guid.NewGuid(),
                ScoredDate = new DateOnly(2025, 1, 30),
                ScoredById = Guid.NewGuid(),
                Comments = "NEW_COMMENTS",
                UseComments = false
            };
            await _repository.UpdateScoreAsync(existingScore.FacilityId, updateDto);
            _context.ChangeTracker.Clear();

            var updatedScore = await _context.Scores.FindAsync(existingScore.Id);

            updatedScore.Should().BeEquivalentTo(updateDto, options => options
                .Excluding(e => e.Id));
        }

        [Test]
        public async Task UpdateScoresAsync_ThrowsKeyNotFoundException_WhenIdDoesNotExist()
        {
            var invalidFacilityId = Guid.NewGuid();
            var updateDto = new ScoreEditDto { FacilityId = invalidFacilityId, Comments = "NEW_COMMENTS" };

            var action = async () => await _repository.UpdateScoreAsync(invalidFacilityId, updateDto);

            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

        // UpdateScoreStatusAsync
        [Test]
        public async Task UpdateScoreStatusAsync_UpdatesStatusCorrectly()
        {
            var existingScore = new Score { Id = Guid.NewGuid(), Active = true };
            _context.Scores.Add(existingScore);
            await _context.SaveChangesAsync();

            await _repository.UpdateScoreStatusAsync(existingScore.Id, false);

            var updatedScore = await _context.Scores.FindAsync(existingScore.Id);
            updatedScore.Active.Should().BeFalse();

        }

        [Test]
        public async Task UpdateScoreStatusAsync_ThrowsArgumentException_WhenIdDoesNotExist()
        {
            var invalidId = Guid.NewGuid();
            var action = async () => await _repository.UpdateScoreStatusAsync(invalidId, false);
            await action.Should().ThrowAsync<ArgumentException>();
        }
    }
}
