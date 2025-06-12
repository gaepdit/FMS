using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IGroundwaterScoreRepository : IDisposable
    {
        Task<bool> GroundwaterScoreExistsAsync(Guid id);

        Task<GroundwaterScoreEditDto> GetGroundwaterScoreByScoreIdAsync(Guid id);

        Task<bool> CreateGroundwaterScoreAsync(GroundwaterScoreCreateDto groundwaterScore);

        Task<bool> UpdateGroundwaterScoreAsync(GroundwaterScoreEditDto groundwaterScore);

        Task<bool> DeleteGroundwaterScoreAsync(Guid id);
    }
}
