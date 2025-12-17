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

        Task<SubstanceEditDto> GetSubstanceByIdAsync(Guid? id);

        Task<Substance> GetSubstanceForGWByFacilityIdAsync(Guid? facilityId);

        Task<Substance> GetSubstanceForSoilByFacilityIdAsync(Guid? facilityId);

        Task<bool> SubstanceExistsForChemicalAsync(Guid chemicalId, Guid facilityId);

        Task<bool> SubstanceUsedForOnsiteScoreForFacilityExistsAsync(Guid facilityId);

        Task<bool> SubstanceUsedForOnsiteScoreExistsAsync(Guid id, Guid facilityId);

        Task<bool> SubstanceUsedForGroundwaterScoreForFacilityExistsAsync(Guid facilityId);

        Task<bool> SubstanceUsedForGroundwaterScoreExistsAsync(Guid id, Guid facilityId);

        Task<SubstanceSummaryDto> GetSubstanceSummaryByIdAsync(Guid id);

        Task<IEnumerable<SubstanceSummaryDto>> GetSubstanceListByFacilityIdAsync(Guid facilityId);

        //Task<IList<SubstanceEditDto>> GetSubstanceByFacilityIdAsync(Guid facilityId);

        Task<Guid> CreateSubstanceAsync(SubstanceCreateDto substance);

        Task UpdateSubstanceAsync(Guid id, SubstanceEditDto substanceUpdates);

        Task UpdateSubstanceStatusAsync(Guid id, bool active);

        Task DeleteSubstanceAsync(Guid id);
    }
}
