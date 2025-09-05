using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ISubstanceRepository : IDisposable
    {
        Task<bool> SubstanceExistsAsync(Guid id);

        Task<SubstanceEditDto> GetSubstanceByIdAsync(Guid id);

        Task<IReadOnlyList<SubstanceSummaryDto>> GetReadOnlySubstanceByFacilityIdAsync(Guid facilityId);

        Task<IList<SubstanceEditDto>> GetSubstanceByFacilityIdAsync(Guid facilityId);

        Task<Guid> CreateSubstanceAsync(SubstanceCreateDto substance);

        Task UpdateSubstanceAsync(Guid id, SubstanceEditDto substanceUpdates);

        Task UpdateSubstanceStatusAsync(Guid id, bool active);
    }
}
