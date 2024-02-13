using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;

namespace FMS.Domain.Repositories
{
    public interface IFacilityRepository : IDisposable
    {
        Task<bool> FacilityExistsAsync(Guid id);
        Task<FacilityDetailDto> GetFacilityAsync(Guid id);
        Task<int> CountAsync(FacilitySpec spec);
        Task<PaginatedList<FacilitySummaryDto>> GetFacilityPaginatedListAsync(
            FacilitySpec spec, int pageNumber, int pageSize);
        Task<IReadOnlyList<FacilityDetailDto>> GetFacilityDetailListAsync(FacilitySpec spec);
        Task<IReadOnlyList<FacilityMapSummaryDto>> GetFacilityListAsync(FacilityMapSpec spec);
        Task<IEnumerable<RetentionRecordDetailDto>> GetRetentionRecordsListAsync(FacilitySpec spec);
        Task<Guid> CreateFacilityAsync(FacilityCreateDto newFacility, bool newFileId = true);
        Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates);
        Task DeleteFacilityAsync(Guid id);
        Task UndeleteFacilityAsync(Guid id);
        Task<bool> FacilityNumberExists(string facilityNumber, Guid? ignoreId = null);
        Task<bool> FileLabelExists(string fileLabel);
        Task<int> GetNextSequenceForCountyAsync(int countyId);

        // Retention Records
        Task<bool> RetentionRecordExistsAsync(Guid id);
        Task<RetentionRecordDetailDto> GetRetentionRecordAsync(Guid id);
        Task<Guid> CreateRetentionRecordAsync(Guid facilityId, RetentionRecordCreateDto create);
        Task UpdateRetentionRecordAsync(Guid id, RetentionRecordEditDto edit);
        Task<FacilityBasicDto> GetFacilityForRetentionRecord(Guid recordId);
    }
}