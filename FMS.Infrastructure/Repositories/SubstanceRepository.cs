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
    public class SubstanceRepository : ISubstanceRepository
    {
        private readonly FmsDbContext _context;
        public SubstanceRepository(FmsDbContext context) => _context = context;

        public Task<bool> SubstanceExistsAsync(Guid id) =>
            _context.Substances.AnyAsync(e => e.Id == id);

        public async Task<SubstanceEditDto> GetSubstanceByIdAsync(Guid id) =>
            await _context.Substances.AsNoTracking()
            .Include(e => e.Chemical)
            .Where(e => e.Id == id)
            .Select(e => new SubstanceEditDto(e))
            .SingleOrDefaultAsync();

        public async Task<SubstanceSummaryDto> GetSubstanceSummaryByIdAsync(Guid id) =>
            await _context.Substances.AsNoTracking()
            .Include(e => e.Chemical)
            .Where(e => e.Id == id)
            .Select(e => new SubstanceSummaryDto(e))
            .SingleOrDefaultAsync();

        public async Task<IReadOnlyList<SubstanceSummaryDto>> GetReadOnlySubstanceByFacilityIdAsync(Guid facilityId) =>
           await _context.Substances.AsNoTracking()
            .Include(e => e.Chemical)
            .Where(e => e.FacilityId == facilityId)
            .Select(e => new SubstanceSummaryDto(e))
            .OrderByDescending(e => e.UseForScoring)
            .ToListAsync();

        public async Task<IList<SubstanceEditDto>> GetSubstanceByFacilityIdAsync(Guid facilityId) => await _context.Substances.AsNoTracking()
            .Include(e => e.Chemical)
            .Where(e => e.FacilityId == facilityId)
            .Select(e => new SubstanceEditDto(e))
            .OrderByDescending(e => e.UseForScoring)
            .ToListAsync();

        public async Task<Guid> CreateSubstanceAsync(SubstanceCreateDto substance)
        {
            Prevent.Null(substance, nameof(substance));
            Prevent.NullOrEmpty(substance.FacilityId, nameof(substance.FacilityId));

            var newSubstance = new Substance(substance);

            if (substance.FacilityId == Guid.Empty)
            {
                throw new ArgumentException("FacilityId cannot be empty.", nameof(substance));
            }

            _context.Substances.Add(newSubstance);
            await _context.SaveChangesAsync();
            return newSubstance.Id;
        }

        public async Task UpdateSubstanceAsync(Guid id, SubstanceEditDto substanceUpdates)
        {
            Prevent.Null(substanceUpdates, nameof(substanceUpdates));
            Prevent.NullOrEmpty(id, nameof(id));

            var existingSubstance = await _context.Substances.SingleOrDefaultAsync(e => e.Id == id);

            if (existingSubstance == null)
            {
                throw new KeyNotFoundException($"Substance with Id {id} not found.");
            }

            existingSubstance.Id = substanceUpdates.Id;
            existingSubstance.Active = substanceUpdates.Active;
            existingSubstance.FacilityId = substanceUpdates.FacilityId;
            existingSubstance.ChemicalId = substanceUpdates.ChemicalId;
            existingSubstance.Groundwater = substanceUpdates.Groundwater;
            existingSubstance.Soil = substanceUpdates.Soil;
            existingSubstance.UseForScoring = substanceUpdates.UseForScoring;

            _context.Substances.Update(existingSubstance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubstanceStatusAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));
            var existingSubstance = await _context.Substances.SingleOrDefaultAsync(e => e.Id == id);
            if (existingSubstance == null)
            {
                throw new KeyNotFoundException($"Substance with Id {id} not found.");
            }
            existingSubstance.Active = active;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubstanceAsync(Guid id)
        {
            Prevent.NullOrEmpty(id, nameof(id));
            var existingSubstance = await _context.Substances.SingleOrDefaultAsync(e => e.Id == id);
            if (existingSubstance == null)
            {
                throw new KeyNotFoundException($"Substance with Id {id} not found.");
            }
            _context.Substances.Remove(existingSubstance);
            await _context.SaveChangesAsync();
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
        ~SubstanceRepository()
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
