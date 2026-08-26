using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IAllowedEventTypeRepository
    {
        Task<bool> AllowedEventTypeExistsAsync(Guid id);

        Task<bool> AllowedEventTypeExistsAsync(Guid FacilityTypeId, Guid eventTypeId);

        Task<AllowedEventTypeSpec> GetAllowedEventTypeByAETIdAsync(Guid? id);

        Task<AllowedEventTypeSpec> GetAllowedEventTypeByFacilityTypeAndEventTypeAsync(Guid FacilityTypeId, Guid eventTypeId);

        Task<IList<AllowedEventTypeSpec>> GetAllowedEventTypeListAsync(Guid facilityTypeId);

        Task<Guid> CreateAllowedEventTypeAsync(AllowedEventTypeSpec allowedEventType);

        Task<Guid> UpdateAllowedEventTypeAsync(AllowedEventTypeSpec allowedEventType);

        Task<Guid> DeleteAllowedEventTypeAsync(Guid? id);
    }
}
