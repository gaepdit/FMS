using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class GapsAssessmentRepository : IGapsAssessmentRepository
    {
        private readonly FmsDbContext _context;
        public GapsAssessmentRepository(FmsDbContext context) => _context = context;

        public Task<bool> GapsAssessmentExistsAsync(Guid id) =>
            _context.GapsAssessments.AnyAsync(e => e.Id == id);

        public Task<GapsAssessment> GetGapsAssessmentByIdAsync(Guid id) =>
            _context.GapsAssessments.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

        public async Task<IReadOnlyList<GapsAssessmentSummaryDto>> GetGapsAssessmentListAsync(bool ActiveOnly = false) => await _context.GapsAssessments.AsNoTracking()
                .OrderByDescending(e => e.Name)
                .Where(e => e.Active || e.Active == ActiveOnly)
                .Select(e => new GapsAssessmentSummaryDto(e))
                .ToListAsync();

        public async Task<Guid> CreateGapsAssessmentAsync(GapsAssessmentCreateDto gapsAssessment)
        {
            if (gapsAssessment == null)
            {
                throw new ArgumentNullException(nameof(gapsAssessment));
            }
            var newGapsAssessment = new GapsAssessment(gapsAssessment);
            await _context.GapsAssessments.AddAsync(newGapsAssessment);
            await _context.SaveChangesAsync();
            return newGapsAssessment.Id;
        }

        public async Task UpdateGapsAssessmentAsync(GapsAssessmentCreateDto gapsAssessment)
        {
            if (gapsAssessment == null)
            {
                throw new ArgumentNullException(nameof(gapsAssessment));
            }
            var existingGapsAssessment = await _context.GapsAssessments
                .SingleOrDefaultAsync(e => e.Id == gapsAssessment.Id);
            if (existingGapsAssessment == null)
            {
                throw new KeyNotFoundException($"Gaps Assessment with ID {gapsAssessment.Id} not found.");
            }
            
            existingGapsAssessment.Name = gapsAssessment.Name;
            existingGapsAssessment.Description = gapsAssessment.Description;
            existingGapsAssessment.Active = gapsAssessment.Active;

            _context.GapsAssessments.Update(existingGapsAssessment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGapsAssessmentStatusAsync(Guid id)
        {
            var gapsAssessment = await _context.GapsAssessments.FindAsync(id);
            if (gapsAssessment == null)
            {
                throw new KeyNotFoundException($"Gaps Assessment with ID {id} not found.");
            }

            gapsAssessment.Active = !gapsAssessment.Active;

            _context.GapsAssessments.Update(gapsAssessment);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support

        private bool _disposedValue;

        private void Dispose(bool disposing)
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
        ~GapsAssessmentRepository()
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
