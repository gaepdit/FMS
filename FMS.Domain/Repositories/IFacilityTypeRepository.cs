using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityTypeRepository : IDisposable
    {
        Task<bool> FacilityTypeExistsAsync(Guid id);
        Task<bool> FacilityTypeExistsAsync(int code);
        Task<bool> FacilityTypeCodeExistsAsync(int facilityTypeCode, Guid? ignoreId = null);
        Task<int> CountAsync(FacilityTypeSpec spec);
        Task<FacilityTypeEditDto> GetFacilityTypeAsync(Guid id);
        Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync();
        Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityType);
        Task<Guid> CreateFacilityTypeInternalAsync(FacilityTypeCreateDto facilityType);
        Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates);
        Task UpdateFacilityTypeInternalAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates);
        Task UpdateFacilityTypeStatusAsync(Guid id, bool active);
    }
}
