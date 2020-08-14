using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityRepository
    {
        Task<bool> FacilityExistsAsync(Guid id);
        Task<FacilityDetailDto> GetFacilityAsync(Guid id);

        Task<int> CountAsync(FacilitySpec spec);
        Task<IReadOnlyList<FacilitySummaryDto>> GetFacilityListAsync(FacilitySpec spec);

        Task<bool> CreateFacilityAsync(FacilityCreateDto facility);
        Task<bool> UpdateFacilityAsync(Guid id, FacilityEditDto facility);
    }
}
