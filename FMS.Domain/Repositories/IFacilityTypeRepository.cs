using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IFacilityTypeRepository : IDisposable
    {
        Task<bool> FacilityTypeExistsAsync(Guid id);
        Task<bool> FacilityTypeNameExistsAsync(string name, Guid? ignoreId = null);
        Task<bool> FacilityTypeDescriptionExistsAsync(string description, Guid? ignoreId = null);
        Task<FacilityTypeEditDto> GetFacilityTypeAsync(Guid id);
        Task<string> GetFacilityTypeNameAsync(Guid? id);
        Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync();
        Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityType);
        Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates);
        Task UpdateFacilityTypeStatusAsync(Guid id, bool active);
    }
}