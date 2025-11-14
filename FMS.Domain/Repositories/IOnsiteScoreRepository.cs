using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IOnsiteScoreRepository : IDisposable
    {
        Task<bool> OnsiteScoreExistsAsync(Guid id);

        Task<bool> SubstanceExistsInOnsiteScoreAsync(Guid substanceId, Guid facilityId);

        Task<OnsiteScoreEditDto> GetOnsiteScoreByFacilityIdAsync(Guid facilityId);

        Task<Guid> CreateOnsiteScoreAsync(OnsiteScoreCreateDto onsiteScore);

        Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onsiteScore);

        Task UpdateOnsiteScoreStatusAsync(Guid id, bool active);
    }
}
