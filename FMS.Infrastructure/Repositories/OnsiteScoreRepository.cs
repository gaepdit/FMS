using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class OnsiteScoreRepository : IOnsiteScoreRepository
    {
        private readonly FmsDbContext _context;
        public OnsiteScoreRepository(FmsDbContext context) => _context = context;


        public Task<bool> OnsiteScoreExistsAsync(Guid id) =>
            _context.OnsiteScores.AnyAsync(e => e.Id == id);

        public async Task<OnsiteScoreEditDto> GetOnsiteScoreByScoreIdAsync(Guid scoreId)
        {
            var onsiteScore = await _context.OnsiteScores.AsNoTracking()
                .SingleOrDefaultAsync(e => e.ScoreId == scoreId);
            return onsiteScore == null ? null : new OnsiteScoreEditDto(onsiteScore);
        }

        public Task<Guid> CreateOnsiteScoreAsync(OnSiteScoreCreateDto onsiteScore)
        {
            Prevent.Null(onsiteScore, nameof(onsiteScore));
            Prevent.NullOrEmpty(onsiteScore.ScoreId, nameof(onsiteScore.ScoreId));

            return CreateOnsiteScoreInternalAsync(onsiteScore);
        }

        public async Task<Guid> CreateOnsiteScoreInternalAsync(OnSiteScoreCreateDto onsiteScore)
        {
            var newOnsiteScore = new OnsiteScore
            {
                Id = Guid.NewGuid(),
                Active = true,
                ScoreId = onsiteScore.ScoreId,
                OnsiteScoreValue = onsiteScore.OnsiteScoreValue,
                A = onsiteScore.A,
                B = onsiteScore.B,
                C = onsiteScore.C,
                Description = onsiteScore.Description,
                ChemName1D = onsiteScore.ChemName1D,
                Other1D = onsiteScore.Other1D,
                D2 = onsiteScore.D2,
                D3 = onsiteScore.D3,
                CASNO = onsiteScore.CASNO,
                E1 = onsiteScore.E1,
                E2 = onsiteScore.E2
            };

            _context.OnsiteScores.Add(newOnsiteScore);
            await _context.SaveChangesAsync();
            return newOnsiteScore.Id;
        }

        public Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onsiteScore)
        {
            Prevent.Null(onsiteScore, nameof(onsiteScore));
            Prevent.NullOrEmpty(onsiteScore.ScoreId, nameof(onsiteScore.ScoreId));

            return UpdateOnsiteScoreInternalAsync(onsiteScore);
        }

        private async Task<bool> UpdateOnsiteScoreInternalAsync(OnsiteScoreEditDto onsiteScore)
        {
           
            var existingOnsiteScore = await _context.OnsiteScores
                .SingleOrDefaultAsync(e => e.ScoreId == onsiteScore.ScoreId);

            if (existingOnsiteScore == null)
            {
                throw new InvalidOperationException($"Onsite Score with ID {onsiteScore.Id} does not exist.");
            }

            existingOnsiteScore.OnsiteScoreValue = onsiteScore.OnsiteScoreValue;
            existingOnsiteScore.A = onsiteScore.A;
            existingOnsiteScore.B = onsiteScore.B;
            existingOnsiteScore.C = onsiteScore.C;
            existingOnsiteScore.Description = onsiteScore.Description;
            existingOnsiteScore.ChemName1D = onsiteScore.ChemName1D;
            existingOnsiteScore.Other1D = onsiteScore.Other1D;
            existingOnsiteScore.D2 = onsiteScore.D2;
            existingOnsiteScore.D3 = onsiteScore.D3;
            existingOnsiteScore.CASNO = onsiteScore.CASNO;
            existingOnsiteScore.E1 = onsiteScore.E1;
            existingOnsiteScore.E2 = onsiteScore.E2;

            _context.OnsiteScores.Update(existingOnsiteScore);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task UpdateOnsiteScoreStatusAsync(Guid id, bool active)
        {
            var onsiteScore = _context.OnsiteScores.Find(id);

            if (onsiteScore == null)
            {
                throw new InvalidOperationException($"OnsiteScore Score with ID {id} does not exist.");
            }

            onsiteScore.Active = active;

            _context.OnsiteScores.Update(onsiteScore);
            return _context.SaveChangesAsync();
        }


        #region IDisposable Support

        private bool _disposedValue; // Corrected: 'private' modifier now precedes the member type

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
        ~OnsiteScoreRepository()
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
