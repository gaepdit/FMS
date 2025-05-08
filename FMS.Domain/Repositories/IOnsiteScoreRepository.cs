using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IOnsiteScoreRepository
    {
        Task<bool> OnsiteScoreExistsAsync(Guid id);
        Task<OnsiteScoreEditDto> GetOnsiteScoreByIdAsync(Guid id);
        Task<bool> AddOnsiteScoreAsync(OnSiteScoreCreateDto onSiteScore);
        Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onSiteScore);
        Task<bool> DeleteOnsiteScoreAsync(Guid id);
    }
}
