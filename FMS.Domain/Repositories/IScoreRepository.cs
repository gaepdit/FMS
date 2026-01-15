using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IScoreRepository : IDisposable
    {
        Task<bool> ScoreExistsAsync(Guid id);

        Task<ScoreEditDto> GetScoreByIdAsync(Guid id);

        Task<ScoreEditDto> GetScoreEditByFacilityIdAsync(Guid facilityId);

        Task<IEnumerable<Score>> GetScoreByFacilityIdAsync(Guid facilityId);

        Task<Guid> CreateScoreAsync(ScoreCreateDto score);

        Task<Score> UpdateScoreAsync(Guid facilityId, ScoreEditDto scoreUpdates);

        Task<bool> UpdateScoreStatusAsync(Guid id, bool active);
    }
}
