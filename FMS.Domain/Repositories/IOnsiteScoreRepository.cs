using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IOnsiteScoreRepository : IDisposable
    {
        Task<bool> OnsiteScoreExistsAsync(Guid id);

        Task<OnsiteScoreEditDto> GetOnsiteScoreByScoreIdAsync(Guid id);

        Task<bool> CreateOnsiteScoreAsync(OnSiteScoreCreateDto onSiteScore);

        Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onSiteScore);

        Task<bool> DeleteOnsiteScoreAsync(Guid id);
    }
}
