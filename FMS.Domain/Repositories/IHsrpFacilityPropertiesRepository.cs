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
        Task<bool> HsrpFacilityPropertiesExistsAsync(Guid id);

        Task<HsrpFacilityPropertiesDetailDto> GetHsrpFacilityPropertiesByFacilityIdAsync(Guid facilityId);

        Task<Guid> CreateHsrpFacilityPropertiesAsync(HsrpFacilityPropertiesCreateDto location);

        Task UpdateHsrpFacilityPropertiesAsync(Guid Id, HsrpFacilityPropertiesEditDto hsrpFacilityPropertiesUpdates);
    }
}
