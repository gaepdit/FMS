using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IFacilityStatusRepository : IDisposable
    {
        Task<bool> FacilityStatusExistsAsync(Guid id);
        Task<bool> FacilityStatusStatusExistsAsync(string status, Guid? ignoreId = null);
        Task<FacilityStatusEditDto> GetFacilityStatusAsync(Guid id);
        Task<string> GetFacilityStatusNameAsync(Guid? id);
        Task<IReadOnlyList<FacilityStatusSummaryDto>> GetFacilityStatusListAsync();
        Task<Guid> CreateFacilityStatusAsync(FacilityStatusCreateDto facilityStatus);
        Task UpdateFacilityStatusAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates);
        Task UpdateFacilityStatusStatusAsync(Guid id, bool active);
    }
}