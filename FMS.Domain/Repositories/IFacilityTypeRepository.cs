using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityTypeRepository : IDisposable
    {
        Task<bool> FacilityTypeExistsAsync(Guid id);
        Task<FacilityTypeDetailDto> GetFacilityTypeAsync(Guid id);
        Task<int> CountAsync(FacilityTypeSpec spec);
        Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync();
        Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityStatus);
        Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates);
        Task<int> DeleteFacilityTypeAsync(FacilityTypeDetailDto deletedFacilityType);
    }
}
