using System;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class GroundwaterScoreRepository : IGroundwaterScoreRepository
    {
        private readonly FmsDbContext _context;
        public GroundwaterScoreRepository(FmsDbContext context) => _context = context;

        public Task<bool> GroundwaterScoreExistsAsync(Guid id) =>
            _context.GroundwaterScores.AnyAsync(e => e.Id == id);

        public async Task<GroundwaterScoreEditDto> GetGroundwaterScoreByFacilityIdAsync(Guid facilityId)
        {
            var groundwaterScore = await _context.GroundwaterScores.AsNoTracking()
                .Include(e => e.Substance)
                .Include(e => e.Substance.Chemical)
                .Where(e => e.FacilityId == facilityId && e.Substance.ChemicalId == e.Substance.Chemical.Id && e.SubstanceId == e.Substance.Id && e.Substance.Groundwater && e.Substance.UseForScoring)
                .SingleOrDefaultAsync();

            return groundwaterScore == null ? null : new GroundwaterScoreEditDto(groundwaterScore);
        }

        public Task<Guid> CreateGroundwaterScoreAsync(GroundwaterScoreCreateDto groundwaterScore)
        {
            Prevent.Null(groundwaterScore, nameof(groundwaterScore));
            Prevent.NullOrEmpty(groundwaterScore.FacilityId, nameof(groundwaterScore.FacilityId));

            return CreateGroundwaterScoreInternalAsync(groundwaterScore);
        }

        private async Task<Guid> CreateGroundwaterScoreInternalAsync(GroundwaterScoreCreateDto groundwaterScore)
        {
            var newGroundwaterScore = new GroundwaterScore
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = groundwaterScore.FacilityId,
                GWScore = groundwaterScore.GWScore,
                A = groundwaterScore.A,
                B1 = groundwaterScore.B1,
                B2 = groundwaterScore.B2,
                C = groundwaterScore.C,
                Description = groundwaterScore.Description,
                ChemName = groundwaterScore.ChemName,
                Other = groundwaterScore.Other,
                D2 = groundwaterScore.D2,
                D3 = groundwaterScore.D3,
                SubstanceId = null,
                CASNO = groundwaterScore.CASNO,
                E1 = groundwaterScore.E1,
                E2 = groundwaterScore.E2
            };

            _context.GroundwaterScores.Add(newGroundwaterScore);
            await _context.SaveChangesAsync();
            return newGroundwaterScore.Id;
        }

        public Task UpdateGroundwaterScoreAsync(GroundwaterScoreEditDto groundwaterScore)
        {
            Prevent.Null(groundwaterScore, nameof(groundwaterScore));
            Prevent.NullOrEmpty(groundwaterScore.Id, nameof(groundwaterScore.Id));

            return UpdateGroundwaterScoreInternalAsync(groundwaterScore);
        }

        private async Task UpdateGroundwaterScoreInternalAsync(GroundwaterScoreEditDto groundwaterScore)
        {
            var existingGroundwaterScore = await _context.GroundwaterScores.FindAsync(groundwaterScore.Id);

            if (existingGroundwaterScore == null)
            {
                throw new InvalidOperationException($"Groundwater Score with ID {groundwaterScore.Id} does not exist.");
            }

            existingGroundwaterScore.Active = groundwaterScore.Active;
            existingGroundwaterScore.FacilityId = groundwaterScore.FacilityId;
            existingGroundwaterScore.GWScore = groundwaterScore.GWScore;
            existingGroundwaterScore.A = groundwaterScore.A;
            existingGroundwaterScore.B1 = groundwaterScore.B1;
            existingGroundwaterScore.B2 = groundwaterScore.B2;
            existingGroundwaterScore.C = groundwaterScore.C;
            existingGroundwaterScore.Description = groundwaterScore.Description;
            existingGroundwaterScore.ChemName = groundwaterScore.ChemName;
            existingGroundwaterScore.Other = groundwaterScore.Other;
            existingGroundwaterScore.D2 = groundwaterScore.D2;
            existingGroundwaterScore.D3 = groundwaterScore.D3;
            existingGroundwaterScore.SubstanceId = groundwaterScore.SubstanceId;
            existingGroundwaterScore.CASNO = groundwaterScore.CASNO;
            existingGroundwaterScore.E1 = groundwaterScore.E1;
            existingGroundwaterScore.E2 = groundwaterScore.E2;

            _context.GroundwaterScores.Update(existingGroundwaterScore);
            await _context.SaveChangesAsync();
        }

        public Task UpdateGroundwaterScoreStatusAsync(Guid id, bool active)
        {
            var groundwaterScore = _context.GroundwaterScores.Find(id);

            if (groundwaterScore == null)
            {
                throw new InvalidOperationException($"Groundwater Score with ID {id} does not exist.");
            }
            groundwaterScore.Active = active;

            _context.GroundwaterScores.Update(groundwaterScore);
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
        ~GroundwaterScoreRepository()
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
