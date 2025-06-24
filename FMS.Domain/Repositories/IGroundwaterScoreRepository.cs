using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IGroundwaterScoreRepository : IDisposable
    {
        Task<bool> GroundwaterScoreExistsAsync(Guid id);

        Task<GroundwaterScoreEditDto> GetGroundwaterScoreByScoreIdAsync(Guid scoreId);

        Task<Guid> CreateGroundwaterScoreAsync(GroundwaterScoreCreateDto groundwaterScore);

        Task UpdateGroundwaterScoreAsync(GroundwaterScoreEditDto groundwaterScore);

        Task UpdateGroundwaterScoreStatusAsync(Guid id, bool active);
    }
}
