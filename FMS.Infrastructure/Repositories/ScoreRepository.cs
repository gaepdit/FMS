using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly FmsDbContext _context;
        public ScoreRepository(FmsDbContext context) => _context = context;

        public Task<bool> ScoreExistsAsync(Guid id) =>
            _context.Scores.AnyAsync(e => e.Id == id);

        public Task<Score> GetScoreByIdAsync(Guid id) =>
            _context.Scores.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

        public Task<IEnumerable<Score>> GetScoreByFacilityIdAsync(Guid facilityId) =>
            _context.Scores.AsNoTracking()
                .Where(e => e.FacilityId == facilityId)
                .OrderByDescending(e => e.ScoredDate)
                .ToListAsync()
                .ContinueWith(task => task.Result.AsEnumerable());

        public Task<Guid> CreateScoreAsync(ScoreCreateDto score)
            {
            Prevent.Null(score, nameof(score));
            Prevent.NullOrEmpty(score.FacilityId, nameof(score.FacilityId));

            return CreateScoreInternalAsync(score);
        }

        private async Task<Guid> CreateScoreInternalAsync(ScoreCreateDto score)
        {
            var newScore = new Score(score);

            _context.Scores.Add(newScore);
            await _context.SaveChangesAsync();
            return newScore.Id;
        }

        public async Task<Score> UpdateScoreAsync(ScoreEditDto score)
        {
            Prevent.Null(score, nameof(score));
            Prevent.NullOrEmpty(score.Id, nameof(score.Id));

            if (!await ScoreExistsAsync(score.Id))
            {
                throw new ArgumentException($"Score: {score.Id} does not exist.");
            }

            var existingScore = await GetScoreByIdAsync(score.Id);

            _context.Scores.Update(existingScore);
            await _context.SaveChangesAsync();
            return existingScore;
        }

        public async Task<bool> UpdateScoreStatusAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));

            if (!await ScoreExistsAsync(id))
            {
                throw new ArgumentException($"Score: {id} does not exist.");
            }

            var score = await GetScoreByIdAsync(id);

            if (score == null)
            {
                throw new ArgumentException($"Score: {id} does not exist.");
            }

            score.Active = active;

            _context.Scores.Update(score);
            await _context.SaveChangesAsync();
            return true;
        }


        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ScoreRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
