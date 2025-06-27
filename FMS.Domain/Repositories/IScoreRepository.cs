using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IScoreRepository : IDisposable
    {
        Task<bool> ScoreExistsAsync(Guid id);

        Task<Score> GetScoreByIdAsync(Guid id);

        Task<IEnumerable<Score>> GetScoreByFacilityIdAsync(Guid facilityId);

        Task<Score> CreateScoreAsync(ScoreCreateDto score);

        Task<Score> UpdateScoreAsync(ScoreEditDto score);

        Task<bool> UpdateScoreStatusAsync(Guid id);
    }
}
