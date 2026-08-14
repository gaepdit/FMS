using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly FmsDbContext _context;
        public DashboardRepository(FmsDbContext context) => _context = context;

        public static DateOnly GetFiscalYearStartDate()
        {
            var today = DateTime.Today;
            DateTime fiscalYearStart = new(today.Month < 7 ? today.Year - 1 : today.Year, 7, 1, 0, 0, 0,
                DateTimeKind.Local);
            return DateOnly.FromDateTime(fiscalYearStart);
        }

        public async Task<bool> UserExistsAsync(Guid id) =>
            await _context.Users.AnyAsync(e => e.Id == id);

        public async Task<ApplicationUser> GetApplicationUser (Guid id) =>
            await _context.Users.SingleOrDefaultAsync(e => e.Id == id);

        public async Task<Guid> GetCOIdFromUser(UserView user)
        {
            var coId = await _context.ComplianceOfficers
                .Where(e => e.GivenName == user.GivenName && e.FamilyName == user.FamilyName)
                .Where(e => e.Active)
                .Select(e => e.Id)
                .FirstOrDefaultAsync();
            return coId;
        }

        public async Task<List<OrganizationalUnitSummaryDto>> GetUserUnitListFromProgramById(Guid id, bool includeInactive = false)
        {
            var currentUser = await GetApplicationUser(id);

            var units = await _context.OrganizationalUnits.AsNoTracking()
                .Where(e => includeInactive || e.Active)
                .Where(e => e.UserProgram.Name == currentUser.UserProgram.Name)
                .OrderByDescending(e => e.Active)
                .Select(e => new OrganizationalUnitSummaryDto(e))
                .ToListAsync();
            return units;
        }

        #region Facilities

        public async Task<List<DashboardUserFacilitiesDto>> GetUserHSIFacilitiesById(Guid id, bool includeInactive = false)
        {
            var facilities = await _context.Facilities.AsNoTracking()
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.County)
                .Include(e => e.Events)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.LocationDetails)
                .ThenInclude(e => e.LocationClass)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.StatusDetails)
                .ThenInclude(e => e.OverallStatus)
                .Where(e => includeInactive || e.Active)
                .Where(e => e.ComplianceOfficerId == id)
                .Where(e => e.FacilityType.Name != "RN")
                .OrderBy(e => e.Name)
                .Select(e => new DashboardUserFacilitiesDto(e))
                .AsSplitQuery()
                .ToListAsync();
            return facilities;
        }

        public async Task<List<DashboardUnitFacilitiesDto>> GetUnitHSIFacilitiesById(Guid id, bool includeInactive = false)
        {
            var currentUser = await GetApplicationUser(id);

            return await _context.Facilities.AsNoTracking()
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.County)
                .Include(e => e.Events)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.LocationDetails)
                .ThenInclude(e => e.LocationClass)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.StatusDetails)
                .ThenInclude(e => e.OverallStatus)
                .Where(e => includeInactive || e.Active)
                .Where(e => e.OrganizationalUnitId == currentUser.UserUnit.Id)
                .Where(e => e.ComplianceOfficer.Active)
                .Where(e => e.FacilityType.Name != "RN")
                .OrderBy(e => e.ComplianceOfficer.FamilyName)
                .ThenBy(e => e.Name)
                .Select(e => new DashboardUnitFacilitiesDto(e))
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<List<DashboardProgramFacilitiesDto>> GetProgramHSIFacilitiesById(Guid id, bool includeInactive = false)
        {
            return await _context.Facilities.AsNoTracking()
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.County)
                .Include(e => e.Events)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.LocationDetails)
                .ThenInclude(e => e.LocationClass)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.StatusDetails)
                .ThenInclude(e => e.OverallStatus)
                .Where(e => includeInactive || e.Active)
                .Where(e => e.OrganizationalUnit.UserProgram.Id == id)
                .Where(e => e.ComplianceOfficer.Active)
                .Where(e => e.FacilityType.Name != "RN")
                .OrderBy(e => e.OrganizationalUnit.Name)
                .ThenBy(e => e.ComplianceOfficer.FamilyName)
                .ThenBy(e => e.Name)
                .Select(e => new DashboardProgramFacilitiesDto(e))
                .AsSplitQuery()
                .ToListAsync();
        }

        #endregion

        #region Events

        public async Task<List<DashboardUserEventsDto>> GetEventsByUserId(Guid id, bool includeInactive = false)
        {
            var fiscalYearStart = GetFiscalYearStartDate();

            return await _context.Events.AsNoTracking()
                .Include(e => e.Facility)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.Facility.OrganizationalUnit)
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Include(e => e.EventContractor)
                .Where(e => includeInactive || e.Active)
                .Where(e => e.ComplianceOfficerId.Equals(id))
                .Where(e => e.CompletionDate >= fiscalYearStart || (e.CompletionDate == null && e.StartDate == null) || (e.CompletionDate == null))
                .OrderByDescending(e => e.StartDate)
                .ThenByDescending(e => e.CompletionDate)
                .ThenByDescending(e => e.DueDate)
                .Select(e => new DashboardUserEventsDto(e))
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<List<DashboardUnitEventsDto>> GetUnitEventsByUserId(Guid id, bool includeInactive = false)
        {
            var currentUser = await GetApplicationUser(id);
            var fiscalYearStart = GetFiscalYearStartDate();

            return await _context.Events.AsNoTracking()
                .Include(e => e.Facility)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Include(e => e.EventContractor)
                .Where(e => includeInactive || e.Active)
                .Where(e => e.Facility.OrganizationalUnit.Id.Equals(currentUser.UserUnit.Id))
                .Where(e => e.CompletionDate >= fiscalYearStart || (e.CompletionDate == null && e.StartDate == null) || (e.CompletionDate == null))
                .OrderBy(e => e.ComplianceOfficer.FamilyName)
                .ThenByDescending(e => e.StartDate)
                .ThenByDescending(e => e.CompletionDate)
                .ThenByDescending(e => e.DueDate)
                .Select(e => new DashboardUnitEventsDto(e))
                .AsSplitQuery()
                .ToListAsync();
        }

        public async Task<List<DashboardProgramEventsDto>> GetProgramEventsByUserId(Guid id, bool includeInactive = false)
        {
            var currentUser = await GetApplicationUser(id);
            var fiscalYearStart = GetFiscalYearStartDate();

            return await _context.Events.AsNoTracking()
                .Include(e => e.Facility)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Include(e => e.EventContractor)
                .Include(e => e.Facility.OrganizationalUnit)
                .Where(e => includeInactive || e.Active)
                .Where(e => e.Facility.OrganizationalUnit.UserProgram.Id.Equals(currentUser.UserProgram.Id))
                .Where(e => e.CompletionDate >= fiscalYearStart || (e.CompletionDate == null && e.StartDate == null) || (e.CompletionDate == null))
                .OrderBy(e => e.Facility.OrganizationalUnit.Name)
                .ThenBy(e => e.ComplianceOfficer.FamilyName)
                .ThenByDescending(e => e.StartDate)
                .ThenByDescending(e => e.CompletionDate)
                .ThenByDescending(e => e.DueDate)
                .Select(e => new DashboardProgramEventsDto(e))
                .AsSplitQuery()
                .ToListAsync();
        }

        #endregion

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~DashboardRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
