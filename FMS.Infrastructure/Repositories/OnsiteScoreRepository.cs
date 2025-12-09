using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class OnsiteScoreRepository : IOnsiteScoreRepository
    {
        private readonly FmsDbContext _context;
        private readonly ISubstanceRepository _substanceRepository;
        public OnsiteScoreRepository(FmsDbContext context, ISubstanceRepository substanceRepository)
        {
            _context = context;
            _substanceRepository = substanceRepository;
        }


        public Task<bool> OnsiteScoreExistsAsync(Guid id) =>
            _context.OnsiteScores.AnyAsync(e => e.Id == id);

        public async Task<bool> SubstanceExistsInOnsiteScoreAsync(Guid substanceId, Guid facilityId) =>
            await _context.OnsiteScores.AnyAsync(e => e.SubstanceId == substanceId && e.FacilityId == facilityId);

        public async Task<OnsiteScoreEditDto> GetOnsiteScoreByFacilityIdAsync(Guid facilityId)
        {
            var onsiteScore = await _context.OnsiteScores.AsNoTracking()
                .Include(e => e.Substance)
                .Include(e => e.Substance.Chemical)
                .Where(e => e.FacilityId == facilityId)
                .SingleOrDefaultAsync();

            if (onsiteScore == null) return null;

            onsiteScore.Substance = await _substanceRepository.GetSubstanceForSoilByFacilityIdAsync(facilityId);
            onsiteScore.SubstanceId = onsiteScore.Substance?.Id;

            return onsiteScore == null ? null : new OnsiteScoreEditDto(onsiteScore);
        }

        public Task<Guid> CreateOnsiteScoreAsync(OnsiteScoreCreateDto onsiteScore)
        {
            Prevent.Null(onsiteScore, nameof(onsiteScore));
            Prevent.NullOrEmpty(onsiteScore.FacilityId, nameof(onsiteScore.FacilityId));

            return CreateOnsiteScoreInternalAsync(onsiteScore);
        }

        public async Task<Guid> CreateOnsiteScoreInternalAsync(OnsiteScoreCreateDto onsiteScore)
        {
            var newOnsiteScore = new OnsiteScore
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = onsiteScore.FacilityId,
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
                SubstanceId = null,
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
            Prevent.NullOrEmpty(onsiteScore.FacilityId, nameof(onsiteScore.FacilityId));

            return UpdateOnsiteScoreInternalAsync(onsiteScore);
        }

        private async Task<bool> UpdateOnsiteScoreInternalAsync(OnsiteScoreEditDto onsiteScore)
        {
           
            var existingOnsiteScore = await _context.OnsiteScores
                .SingleOrDefaultAsync(e => e.FacilityId == onsiteScore.FacilityId);

            if (existingOnsiteScore == null)
            {
                throw new InvalidOperationException($"Onsite Score with ID {onsiteScore.Id} does not exist.");
            }

            existingOnsiteScore.Active = onsiteScore.Active;
            existingOnsiteScore.Id = onsiteScore.Id;
            existingOnsiteScore.FacilityId = onsiteScore.FacilityId;
            existingOnsiteScore.OnsiteScoreValue = onsiteScore.OnsiteScoreValue;
            existingOnsiteScore.A = onsiteScore.A;
            existingOnsiteScore.B = onsiteScore.B;
            existingOnsiteScore.C = onsiteScore.C;
            existingOnsiteScore.Description = onsiteScore.Description;
            existingOnsiteScore.ChemName1D = onsiteScore.ChemName1D;
            existingOnsiteScore.Substance = onsiteScore.Substance;
            existingOnsiteScore.SubstanceId = onsiteScore.SubstanceId;
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
