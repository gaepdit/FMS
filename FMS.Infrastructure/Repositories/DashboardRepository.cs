using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FMS.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly FmsDbContext _context;
        public DashboardRepository(FmsDbContext context) => _context = context;

        public async Task<bool> UserExistsAsync(Guid id) =>
            await _context.Users.AnyAsync(e => e.Id == id);

        public async Task<List<DashboardUserFacilitiesDto>> GetUserFacilitiesById(Guid id, bool includeInactive = false)
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
                .Where(e => e.ComplianceOfficerId == id)
                .OrderByDescending(e => e.Active)
                .Select(e => new DashboardUserFacilitiesDto(e))
                .ToListAsync();
        }

        public async Task<List<DashboardUnitFacilitiesDto>> GetUnitFacilitiesById(Guid id, bool includeInactive = false)
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
                .Where(e => e.OrganizationalUnitId == id)
                .OrderByDescending(e => e.Active)
                .Select(e => new DashboardUnitFacilitiesDto(e))
                .ToListAsync();
        }

        public async Task<List<DashboardProgramFacilitiesDto>> GetProgramFacilitiesById(Guid id, bool includeInactive = false)
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
                .Where(e => e.FacilityTypeId == id)
                .OrderByDescending(e => e.Active)
                .Select(e => new DashboardProgramFacilitiesDto(e))
                .ToListAsync();
        }

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
