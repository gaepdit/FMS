using System;
using System.Collections.Generic;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using TestSupport.EfHelpers;

namespace TestHelpers
{
    public class RepositoryHelper : IDisposable
    {
        private readonly DbContextOptions<FmsDbContext> _options = SqliteInMemory.CreateOptions<FmsDbContext>();
        private readonly FmsDbContext _context;

        private RepositoryHelper()
        {
            _context = new FmsDbContext(_options, default);
            _context.Database.EnsureCreated();
            SeedAllData();
        }

        public static RepositoryHelper CreateRepositoryHelper() => new();

        public void ClearChangeTracker() => _context.ChangeTracker.Clear();

        private void SeedAllData()
        {
            if (!_context.Cabinets.Any()) _context.Cabinets.AddRange(RepositoryData.Cabinets);
            if (!_context.BudgetCodes.Any()) _context.BudgetCodes.AddRange(RepositoryData.BudgetCodes);
            if (!_context.FacilityStatuses.Any()) _context.FacilityStatuses.AddRange(RepositoryData.FacilityStatuses);
            if (!_context.FacilityTypes.Any()) _context.FacilityTypes.AddRange(RepositoryData.FacilityTypes);
            if (!_context.OrganizationalUnits.Any())
                _context.OrganizationalUnits.AddRange(RepositoryData.OrganizationalUnits);
            if (!_context.Files.Any()) _context.Files.AddRange(RepositoryData.Files);
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            if (!_context.Facilities.Any()) _context.Facilities.AddRange(RepositoryData.Facilities());
            _context.SaveChanges();
            _context.ChangeTracker.Clear();
            _context.RetentionRecords.AddRange(RepositoryData.RetentionRecords);
            _context.SaveChanges();
        }

        public IFacilityRepository GetFacilityRepository()
        {
            return new FacilityRepository(new FmsDbContext(_options, default));
        }

        public IFileRepository GetFileRepository()
        {
            return new FileRepository(new FmsDbContext(_options, default));
        }

        public IItemsListRepository GetItemsListRepository()
        {
            return new ItemsListRepository(new FmsDbContext(_options, default));
        }

        public ICabinetRepository GetCabinetRepository()
        {
            return new CabinetRepository(new FmsDbContext(_options, default));
        }

                // Copies of same data from RepositoryData
        // to prevent entity tracking errors when seeding
        // test database above 

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _context?.Dispose();
        }
    }
}