using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IHsrpFacilityPropertiesRepository : IDisposable
    {
        Task<bool> HsrpFacilityPropertiesExistsAsync(Guid facilityId);

        Task<HsrpFacilityPropertiesDetailDto> GetHsrpFacilityPropertiesByFacilityIdAsync(Guid facilityId);

        Task<HsrpFacilityPropertiesEditDto> GetHsrpFacilityPropertiesByIdAsync(Guid? id);

        Task<HsrpFacilityPropertiesEditDto> GetHsrpFacilityPropertiesEditByFacilityIdAsync(Guid? id);

        Task<Guid> CreateHsrpFacilityPropertiesAsync(HsrpFacilityPropertiesCreateDto hsrpFacilityProperties);

        Task UpdateHsrpFacilityPropertiesAsync(Guid facilityId, HsrpFacilityPropertiesEditDto hsrpFacilityPropertiesUpdates);
    }
}
