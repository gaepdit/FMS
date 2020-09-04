using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityStatusRepository : IDisposable
    {
        Task<bool> FacilityStatusExistsAsync(Guid id);
        Task<FacilityStatusDetailDto> GetFacilityStatusAsync(Guid id);
        Task<int> CountAsync(FacilityStatusSpec spec);
        Task<IReadOnlyList<FacilityStatusSummaryDto>> GetFacilityStatusListAsync();
        Task<Guid> CreateFacilityStatusAsync(FacilityStatusCreateDto facilityStatus);
        Task UpdateFacilityStatusAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates);
    }
}
