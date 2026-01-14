using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class HsrpFacilityPropertiesRepository : IHsrpFacilityPropertiesRepository
    {
        public readonly FmsDbContext _context;
        public HsrpFacilityPropertiesRepository(FmsDbContext context) => _context = context;


        public Task<bool> HsrpFacilityPropertiesExistsAsync(Guid facilityId) =>
            _context.HsrpFacilityProperties.AnyAsync(e => e.FacilityId == facilityId);

        public async Task<HsrpFacilityPropertiesDetailDto> GetHsrpFacilityPropertiesByFacilityIdAsync(Guid facilityId)
        {
            var hsrpFacilityProperties = await _context.HsrpFacilityProperties.AsNoTracking()
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .SingleOrDefaultAsync(e => e.FacilityId == facilityId);
            if (hsrpFacilityProperties == null)
            {
                return null;
            }
            return new HsrpFacilityPropertiesDetailDto(hsrpFacilityProperties);
        }

        public async Task<HsrpFacilityPropertiesEditDto> GetHsrpFacilityPropertiesByIdAsync(Guid? id)
        {
            var hsrpFacilityProperties = await _context.HsrpFacilityProperties.AsNoTracking()
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .SingleOrDefaultAsync(e => e.Id == id);
            return hsrpFacilityProperties == null ? null : new HsrpFacilityPropertiesEditDto(hsrpFacilityProperties);
        }

        public async Task<HsrpFacilityPropertiesEditDto> GetHsrpFacilityPropertiesEditByFacilityIdAsync(Guid? id)
        {
            var hsrpFacilityProperties = await _context.HsrpFacilityProperties.AsNoTracking()
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .SingleOrDefaultAsync(e => e.FacilityId == id);
            return hsrpFacilityProperties == null ? null : new HsrpFacilityPropertiesEditDto(hsrpFacilityProperties);
        }

        public Task<Guid> CreateHsrpFacilityPropertiesAsync(HsrpFacilityPropertiesCreateDto hsrpFacilityProperties)
        {
            Prevent.Null(hsrpFacilityProperties, nameof(hsrpFacilityProperties));
            Prevent.NullOrEmpty(hsrpFacilityProperties.FacilityId, nameof(hsrpFacilityProperties.FacilityId));

            return CreateHsrpFacilityPropertiesInternalAsync(hsrpFacilityProperties);
        }

        private async Task<Guid> CreateHsrpFacilityPropertiesInternalAsync(HsrpFacilityPropertiesCreateDto hsrpFacilityProperties)
        {
            var newHsrpFacilityProperties = new HsrpFacilityProperties(Guid.NewGuid(), hsrpFacilityProperties);

            _context.HsrpFacilityProperties.Add(newHsrpFacilityProperties);

            await _context.SaveChangesAsync();
            return newHsrpFacilityProperties.Id;
        }

        public async Task UpdateHsrpFacilityPropertiesAsync(Guid facilityId, HsrpFacilityPropertiesEditDto hsrpFacilityPropertiesUpdates)
        {
            Prevent.Null(hsrpFacilityPropertiesUpdates, nameof(hsrpFacilityPropertiesUpdates));
            Prevent.NullOrEmpty(hsrpFacilityPropertiesUpdates.FacilityId, nameof(hsrpFacilityPropertiesUpdates.FacilityId));

            var hsrpFacilityProperties = await _context.HsrpFacilityProperties
                .SingleOrDefaultAsync(e => e.FacilityId == facilityId);

            if (hsrpFacilityProperties == null)
            {
                throw new KeyNotFoundException($"HSRP Facility Properties with Facility ID {facilityId} not found.");
            }

            hsrpFacilityProperties.DateListed = hsrpFacilityPropertiesUpdates.DateListed;
            hsrpFacilityProperties.OrganizationalUnitId = hsrpFacilityPropertiesUpdates.OrganizationalUnitId;
            hsrpFacilityProperties.ComplianceOfficerId = hsrpFacilityPropertiesUpdates.ComplianceOfficerId;
            hsrpFacilityProperties.VRPDate = hsrpFacilityPropertiesUpdates.VRPDate;
            hsrpFacilityProperties.BrownfieldDate = hsrpFacilityPropertiesUpdates.BrownfieldDate;
            hsrpFacilityProperties.DateDeListed = hsrpFacilityPropertiesUpdates.DateDeListed;
            hsrpFacilityProperties.VRPTerminated = hsrpFacilityPropertiesUpdates.VRPTerminated;
            hsrpFacilityProperties.BrownfieldTerminated = hsrpFacilityPropertiesUpdates.BrownfieldTerminated;

            _context.HsrpFacilityProperties.Update(hsrpFacilityProperties);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~HsrpFacilityPropertiesRepository()
        {
            Dispose(false);
        }

        // Implement IDisposable pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here if necessary
                }

                // Dispose unmanaged resources here if necessary
                _disposed = true;
            }
        }

        #endregion
    }
}
