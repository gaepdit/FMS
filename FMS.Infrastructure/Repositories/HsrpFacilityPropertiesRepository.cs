using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class HsrpFacilityPropertiesRepository : IHsrpFacilityPropertiesRepository
    {
        public readonly FmsDbContext _context;
        public HsrpFacilityPropertiesRepository(FmsDbContext context) => _context = context;


        public Task<bool> HsrpFacilityPropertiesExistsAsync(Guid id) =>
            _context.HsrpFacilityProperties.AnyAsync(e => e.Id == id);

        public async Task<HsrpFacilityPropertiesDetailDto> GetHsrpFacilityPropertiesByFacilityIdAsync(Guid facilityId)
        {
            var hsrpFacilityProperties = await _context.HsrpFacilityProperties.AsNoTracking()
                .SingleOrDefaultAsync(e => e.FacilityId == facilityId);
            if (hsrpFacilityProperties == null)
            {
                return null;
            }
            return new HsrpFacilityPropertiesDetailDto(hsrpFacilityProperties);
        }

        public Task<Guid> CreateHsrpFacilityPropertiesAsync(HsrpFacilityPropertiesCreateDto hsrpFacilityProperties)
        {
            Prevent.Null(hsrpFacilityProperties, nameof(hsrpFacilityProperties));
            Prevent.NullOrEmpty(hsrpFacilityProperties.AdditionalOrgUnit, nameof(hsrpFacilityProperties.AdditionalOrgUnit));
            Prevent.NullOrEmpty(hsrpFacilityProperties.Geologist, nameof(hsrpFacilityProperties.Geologist));
            return CreateHsrpFacilityPropertiesInternalAsync(hsrpFacilityProperties);
        }

        public async Task UpdateHsrpFacilityPropertiesAsync(Guid id, HsrpFacilityPropertiesEditDto hsrpFacilityPropertiesUpdates)
        {
            Prevent.Null(hsrpFacilityPropertiesUpdates, nameof(hsrpFacilityPropertiesUpdates));
            Prevent.NullOrEmpty(hsrpFacilityPropertiesUpdates.AdditionalOrgUnit, nameof(hsrpFacilityPropertiesUpdates.AdditionalOrgUnit));
            Prevent.NullOrEmpty(hsrpFacilityPropertiesUpdates.Geologist, nameof(hsrpFacilityPropertiesUpdates.Geologist));
            var hsrpFacilityProperties = await _context.HsrpFacilityProperties
                .SingleOrDefaultAsync(e => e.Id == id);
            if (hsrpFacilityProperties == null)
            {
                throw new KeyNotFoundException($"HSRP Facility Properties with ID {id} not found.");
            }
            hsrpFacilityProperties.AdditionalOrgUnit = hsrpFacilityPropertiesUpdates.AdditionalOrgUnit;
            hsrpFacilityProperties.Geologist = hsrpFacilityPropertiesUpdates.Geologist;
            hsrpFacilityProperties.VRPDate = hsrpFacilityPropertiesUpdates.VRPDate;
            hsrpFacilityProperties.BrownfieldDate = hsrpFacilityPropertiesUpdates.BrownfieldDate;
            _context.HsrpFacilityProperties.Update(hsrpFacilityProperties);
            await _context.SaveChangesAsync();
        }

        private async Task<Guid> CreateHsrpFacilityPropertiesInternalAsync(HsrpFacilityPropertiesCreateDto hsrpFacilityProperties)
        {
            var newHsrpFacilityProperties = new HsrpFacilityProperties(Guid.NewGuid(), hsrpFacilityProperties);
            _context.HsrpFacilityProperties.Add(newHsrpFacilityProperties);
            await _context.SaveChangesAsync();
            return newHsrpFacilityProperties.Id;
        }

        public async Task<bool> HsrpFacilityPropertiesNameExistsAsync(string name, Guid? ignoreId = null)
        {
            return await _context.HsrpFacilityProperties.AnyAsync(e => e.AdditionalOrgUnit == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));
        }

        public async Task<bool> HsrpFacilityPropertiesDescriptionExistsAsync(string description, Guid? ignoreId = null)
        {
            return await _context.HsrpFacilityProperties.AnyAsync(e => e.Geologist == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));
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
