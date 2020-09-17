using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFacilityRepository : IDisposable
    {
        Task<bool> FacilityExistsAsync(Guid id);
        Task<FacilityDetailDto> GetFacilityAsync(Guid id);
        Task<int> CountAsync(FacilitySpec spec);
        Task<IReadOnlyList<FacilitySummaryDto>> GetFacilityListAsync(FacilitySpec spec);
        Task<IReadOnlyList<FacilityMapSummaryDto>> GetFacilityListAsync(FacilityMapSpec spec);
        Task<Guid> CreateFacilityAsync(FacilityCreateDto facility);
        Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates);

        // Retention Records
        Task<bool> RetentionRecordExistsAsync(Guid id);
        Task<RetentionRecordDetailDto> GetRetentionRecordAsync(Guid id);
        Task<Guid> CreateRetentionRecordAsync(RetentionRecordCreateDto create);
        Task UpdateRetentionRecordAsync(Guid id, RetentionRecordEditDto edit);
    }
}
