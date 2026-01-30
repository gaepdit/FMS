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


        // OrganizationalUnitNameExistsAsync


        // GetOrganizationalUnitAsync


        // GetOrganizationalUnitListAsync


        // CreateOrganizationalUnitAsync


        // UpdateOrganizationalUnitAsync


        // UpdateOrganizationalUnitStatusAsync
    }
}
