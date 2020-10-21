using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityStatusRepository : IDisposable
    {
        Task<bool> FacilityStatusExistsAsync(string status);
        Task<bool> FacilityStatusExistsAsync(Guid id);
        Task<bool> FacilityStatusStatusExistsAsync(string facilityStatusStatus, Guid? ignoreId = null);
        Task<FacilityStatusEditDto> GetFacilityStatusAsync(Guid id);
        Task<int> CountAsync(FacilityStatusSpec spec);
        Task<IReadOnlyList<FacilityStatusSummaryDto>> GetFacilityStatusListAsync();
        Task<Guid> CreateFacilityStatusAsync(FacilityStatusCreateDto facilityStatus);
        Task UpdateFacilityStatusAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates);
        Task UpdateFacilityStatusStatusAsync(Guid id, bool active);
    }
}
