using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityTypeRepository : IDisposable
    {
        Task<bool> FacilityTypeExistsAsync(Guid id);
        Task<bool> FacilityTypeNameExistsAsync(string name, Guid? ignoreId = null);
        Task<FacilityTypeEditDto> GetFacilityTypeAsync(Guid id);
        Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync();
        Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityType);
        Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates);
        Task UpdateFacilityTypeStatusAsync(Guid id, bool active);
    }
}