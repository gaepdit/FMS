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
    public class LocationRepository : ILocationRepository
    {
        public readonly FmsDbContext _context;
        public LocationRepository(FmsDbContext context) => _context = context;

        public Task<bool> LocationExistsAsync(Guid id) =>
            _context.Locations.AnyAsync(e => e.Id == id);

        public async Task<LocationEditDto> GetLocationByIdAsync(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            var location = await _context.Locations.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (location == null)
            {
                return null;
            }
            return new LocationEditDto(location);
        }

        public async Task<LocationEditDto> GetLocationByFacilityIdAsync(Guid facilityId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));

            var location = await _context.Locations.AsNoTracking()
                .SingleOrDefaultAsync(e => e.FacilityId == facilityId);
            if (location == null)
            {
                return null;
            }
            return new LocationEditDto(location);
        }

        public async Task<List<LocationSummaryDto>> GetLocationListAsync() =>
            await _context.Locations.AsNoTracking()
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.Name)
                .Select(e => new LocationSummaryDto(e))
                .ToListAsync();
        

        public Task<Guid> CreateLocationAsync(LocationCreateDto location)
        {
            Prevent.Null(location, nameof(location));
            Prevent.NullOrEmpty(location.FacilityId, nameof(location.FacilityId));

            return CreateLocationInternalAsync(location);
        }

        private async Task<Guid> CreateLocationInternalAsync(LocationCreateDto location)
        {
            Prevent.Null(location, nameof(location));
            Prevent.NullOrEmpty(location.FacilityId, nameof(location.FacilityId));

            if (await _context.Locations.AnyAsync(e => e.FacilityId == location.FacilityId))
            {
                throw new ArgumentException($"Location for Facility '{location.FacilityId}' already exists.");
            }

            var newLocation = new Location(Guid.NewGuid(), location);

            await _context.Locations.AddAsync(newLocation);
            await _context.SaveChangesAsync();

            return newLocation.Id;
        }

        public Task UpdateLocationAsync(Guid facilityId, LocationEditDto locationUpdates)
        {
            Prevent.Null(locationUpdates, nameof(locationUpdates));
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));
            
            return UpdateLocationInternalAsync(facilityId, locationUpdates);
        }

        private async Task<Guid> UpdateLocationInternalAsync(Guid facilityId, LocationEditDto locationUpdates)
        {
            var location = await _context.Locations
                .SingleOrDefaultAsync(e => e.FacilityId == facilityId);

            location.FacilityId = locationUpdates.FacilityId;
            location.Name = locationUpdates.Name;
            location.Description = locationUpdates.Class;

            _context.Locations.Update(location);
            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return location.Id;
        }

        public async Task UpdateLocationStatusAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));

            var existingLocation = await _context.Locations
                .SingleOrDefaultAsync(e => e.Id == id);

            if (existingLocation == null)
            {
                throw new KeyNotFoundException($"Location with ID '{id}' not found.");
            }

            existingLocation.Active = active;

            _context.Locations.Update(existingLocation);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~LocationRepository()
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
