using FMS.Domain.Dto;
using System;
using System.Collections;
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
        Task<IReadOnlyList<FacilityDetailDto>> GetFacilityDetailListAsync(FacilitySpec spec);
        Task<IReadOnlyList<FacilityMapSummaryDto>> GetFacilityListAsync(FacilityMapSpec spec);
        Task<Guid> CreateFacilityAsync(FacilityCreateDto facility);
        Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates);
        Task<bool> FacilityNumberExists(string facilityNumber, Guid? ignoreId = null);
        Task<bool> FileLabelExists(string fileLabel);

        // Retention Records
        Task<bool> RetentionRecordExistsAsync(Guid id);
        Task<RetentionRecordDetailDto> GetRetentionRecordAsync(Guid id);
        Task<Guid> CreateRetentionRecordAsync(Guid facilityId, RetentionRecordCreateDto create);
        Task UpdateRetentionRecordAsync(Guid id, RetentionRecordEditDto edit);
        Task<FacilityBasicDto> GetFacilityForRetentionRecord(Guid recordId);
    }

    //public class FacilityItem : IEnumerable
    //{
    //    public int Counter { get; set; }
    //    public Guid Id { get; set; }

    //    public FacilityItem() { }

    //    public FacilityItem(int count, Guid id)
    //    {
    //        Counter = count;
    //        Id = id;
    //    }

    //    public IEnumerator GetEnumerator()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}