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


        // GetScoreByIdAsync


        // GetScoreEditByFacilityIdAsync


        // GetScoreByFacilityIdAsync


        // CreateScoreAsync


        // UpdateScoreAsync


        // UpdateScoreStatusAsync

    }
}
