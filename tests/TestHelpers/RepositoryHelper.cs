using System;
using System.Linq;
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
            if (!_context.Cabinets.Any())
            {
                System.Collections.Generic.List<FMS.Domain.Entities.Cabinet> cabinets = RepositoryData.Cabinets;
                _context.Cabinets.AddRange(cabinets);
            }

            if (!_context.BudgetCodes.Any()) _context.BudgetCodes.AddRange(RepositoryData.BudgetCodes);

            if (!_context.FacilityStatuses.Any()) _context.FacilityStatuses.AddRange(RepositoryData.FacilityStatuses);

            if (!_context.FacilityTypes.Any()) _context.FacilityTypes.AddRange(RepositoryData.FacilityTypes);

            if (!_context.OrganizationalUnits.Any())
                _context.OrganizationalUnits.AddRange(RepositoryData.OrganizationalUnits);

            if (!_context.Files.Any()) _context.Files.AddRange(RepositoryData.Files);

            //if (!_context.ComplianceOfficers.Any()) _context.ComplianceOfficers.AddRange(RepositoryData.ComplianceOfficers);

            //if (!_context.Counties.Any()) _context.Counties.AddRange(RepositoryData.Counties);

            //if (!_context.Chemicals.Any()) _context.Chemicals.AddRange(RepositoryData.Chemicals);

            //if (!_context.ActionsTaken.Any()) _context.ActionsTaken.AddRange(RepositoryData.ActionsTaken);

            //if (!_context.ContactTypes.Any()) _context.ContactTypes.AddRange(RepositoryData.ContactTypes);

            //_context.SaveChanges();
            //_context.ChangeTracker.Clear();

            //if (!_context.EventTypes.Any()) _context.EventTypes.AddRange(RepositoryData.EventTypes);

            //if (!_context.AllowedActionsTaken.Any())
            //    _context.AllowedActionsTaken.AddRange(RepositoryData.AllowedActionsTaken);

            //if (!_context.FundingSources.Any())
            //    _context.FundingSources.AddRange(RepositoryData.FundingSources);

            //if (!_context.ParcelTypes.Any())
            //    _context.ParcelTypes.AddRange(RepositoryData.ParcelTypes);

            //if (!_context.ContactTitles.Any()) _context.ContactTitles.AddRange(RepositoryData.ContactTitles);

            //if (!_context.Contacts.Any()) _context.Contacts.AddRange(RepositoryData.Contacts);

            //if (!_context.Phones.Any()) _context.Phones.AddRange(RepositoryData.Phones);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            if (!_context.Facilities.Any()) _context.Facilities.AddRange(RepositoryData.Facilities());

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            //if (!_context.HsrpFacilityProperties.Any()) _context.HsrpFacilityProperties.AddRange(RepositoryData.hsrpFacilityProperties);

            //if (!_context.Events.Any()) _context.Events.AddRange(RepositoryData.Events);

            //if (!_context.Locations.Any()) _context.Locations.AddRange(RepositoryData.Locations);

            //if (!_context.OverallStatuses.Any()) _context.OverallStatuses.AddRange(RepositoryData.OverallStatuses);

            //if (!_context.Parcels.Any()) _context.Parcels.AddRange(RepositoryData.Parcels);

            //if (!_context.Scores.Any()) _context.Scores.AddRange(RepositoryData.Scores);

            //if (!_context.OnSiteScores.Any()) _context.OnSiteScores.AddRange(RepositoryData.OnSiteScores);

            //if (!_context.SoilStatuses.Any()) _context.SoilStatuses.AddRange(RepositoryData.SoilStatuses);

            //if (!_context.SourceStatuses.Any()) _context.SourceStatuses.AddRange(RepositoryData.SourceStatuses);

            //if (!_context.GroundwaterStatuses.Any()) _context.GroundwaterStatuses.AddRange(RepositoryData.GroundwaterStatuses);

            //if (!_context.GroundwaterScores.Any()) _context.GroundwaterScores.AddRange(RepositoryData.GroundwaterScores);

            //if (!_context.Substances.Any()) _context.Substances.AddRange(RepositoryData.Substances);

            //_context.SaveChanges();
            //_context.ChangeTracker.Clear();

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