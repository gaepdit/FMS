using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IOnsiteScoreRepository : IDisposable
    {
        Task<bool> OnsiteScoreExistsAsync(Guid id);

        Task<OnsiteScoreEditDto> GetOnsiteScoreByScoreIdAsync(Guid scoreId);

        Task<Guid> CreateOnsiteScoreAsync(OnSiteScoreCreateDto onsiteScore);

        Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onsiteScore);

        Task UpdateOnsiteScoreStatusAsync(Guid id, bool active);
    }
}
