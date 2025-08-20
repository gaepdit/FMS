using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IHsrpFacilityPropertiesRepository : IDisposable
    {
        Task<bool> HsrpFacilityPropertiesExistsAsync(Guid facilityId);

        Task<HsrpFacilityPropertiesDetailDto> GetHsrpFacilityPropertiesByFacilityIdAsync(Guid facilityId);

        Task<HsrpFacilityPropertiesEditDto> GetHsrpFacilityPropertiesEditAsync(Guid id);

        Task<Guid> CreateHsrpFacilityPropertiesAsync(HsrpFacilityPropertiesCreateDto hsrpFacilityProperties);

        Task UpdateHsrpFacilityPropertiesAsync(Guid facilityId, HsrpFacilityPropertiesEditDto hsrpFacilityPropertiesUpdates);
    }
}
