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
            if (!_context.Cabinets.Any()) _context.Cabinets.AddRange(RepositoryData.Cabinets);

            if (!_context.BudgetCodes.Any()) _context.BudgetCodes.AddRange(RepositoryData.BudgetCodes);

            if (!_context.FacilityStatuses.Any()) _context.FacilityStatuses.AddRange(RepositoryData.FacilityStatuses);

            if (!_context.FacilityTypes.Any()) _context.FacilityTypes.AddRange(RepositoryData.FacilityTypes);

            if (!_context.OrganizationalUnits.Any())
                _context.OrganizationalUnits.AddRange(RepositoryData.OrganizationalUnits);

            if (!_context.Files.Any()) _context.Files.AddRange(RepositoryData.Files);

            if (!_context.ComplianceOfficers.Any()) _context.ComplianceOfficers.AddRange(RepositoryData.ComplianceOfficers);

            if (!_context.Counties.Any()) _context.Counties.AddRange(RepositoryData.Counties);

            if (!_context.Chemicals.Any()) _context.Chemicals.AddRange(RepositoryData.Chemicals);

            if (!_context.ActionTaken.Any()) _context.ActionTaken.AddRange(RepositoryData.ActionsTaken);

            if (!_context.ContactType.Any()) _context.ContactType.AddRange(RepositoryData.ContactTypes);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            if (!_context.EventType.Any()) _context.EventType.AddRange(RepositoryData.EventTypes);

            if (!_context.AllowedActionTaken.Any())
                _context.AllowedActionTaken.AddRange(RepositoryData.AllowedActionsTaken);

            if (!_context.FundingSource.Any())
                _context.FundingSource.AddRange(RepositoryData.FundingSources);

            if (!_context.ParcelType.Any())
                _context.ParcelType.AddRange(RepositoryData.ParcelTypes);

            if (!_context.ContactTitle.Any()) _context.ContactTitle.AddRange(RepositoryData.ContactTitles);

            if (!_context.Contact.Any()) _context.Contact.AddRange(RepositoryData.Contacts);

            if (!_context.Phone.Any()) _context.Phone.AddRange(RepositoryData.Phones);

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            if (!_context.Facilities.Any()) _context.Facilities.AddRange(RepositoryData.Facilities());

            _context.SaveChanges();
            _context.ChangeTracker.Clear();

            if (!_context.HsrpFacilityProperties.Any()) _context.HsrpFacilityProperties.AddRange(RepositoryData.hsrpFacilityProperties);

            if (!_context.Event.Any()) _context.Event.AddRange(RepositoryData.Events);

            if (!_context.Location.Any()) _context.Location.AddRange(RepositoryData.Locations);

            if (!_context.OverallStatus.Any()) _context.OverallStatus.AddRange(RepositoryData.OverallStatuses);

            if (!_context.Parcel.Any()) _context.Parcel.AddRange(RepositoryData.Parcels);

            if (!_context.Score.Any()) _context.Score.AddRange(RepositoryData.Scores);

            if (!_context.OnSiteScore.Any()) _context.OnSiteScore.AddRange(RepositoryData.OnSiteScores);

            if (!_context.SoilStatus.Any()) _context.SoilStatus.AddRange(RepositoryData.SoilStatuses);

            if (!_context.SourceStatus.Any()) _context.SourceStatus.AddRange(RepositoryData.SourceStatuses);

            if (!_context.GroundwaterStatus.Any()) _context.GroundwaterStatus.AddRange(RepositoryData.GroundwaterStatuses);

            if (!_context.GroundwaterScore.Any()) _context.GroundwaterScore.AddRange(RepositoryData.GroundwaterScores);

            if (!_context.Substance.Any()) _context.Substance.AddRange(RepositoryData.Substances);

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