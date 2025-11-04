using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ILocationRepository : IDisposable
    {
        Task<bool> LocationExistsAsync(Guid id);

        Task<LocationEditDto> GetLocationByIdAsync(Guid? id);

        Task<LocationEditDto> GetLocationByFacilityIdAsync(Guid facilityId);

        Task<List<LocationSummaryDto>> GetLocationListAsync();

        Task<Guid> CreateLocationAsync(LocationCreateDto location);

        Task UpdateLocationAsync(Guid facilityId, LocationEditDto locationUpdates);

        Task UpdateLocationStatusAsync(Guid id, bool active);
    }
}
