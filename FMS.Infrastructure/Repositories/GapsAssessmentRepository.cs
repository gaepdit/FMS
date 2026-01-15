using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class GapsAssessmentRepository : IGapsAssessmentRepository
    {
        private readonly FmsDbContext _context;
        public GapsAssessmentRepository(FmsDbContext context) => _context = context;

        public Task<bool> GapsAssessmentExistsAsync(Guid id) =>
            _context.GapsAssessments.AnyAsync(e => e.Id == id);

        public Task<bool> GapsAssessmentNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.GapsAssessments.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<GapsAssessmentEditDto> GetGapsAssessmentByIdAsync(Guid id) =>
            _context.GapsAssessments.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new GapsAssessmentEditDto(e))
                .SingleOrDefaultAsync();

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

        public Task UpdateGapsAssessmentAsync(Guid id, GapsAssessmentEditDto gapsAssessmentUpdates)
        {
            Prevent.NullOrEmpty(gapsAssessmentUpdates.Name, nameof(gapsAssessmentUpdates.Name));
            return UpdateGapsAssessmentInternalAsync(id, gapsAssessmentUpdates);
        }

        public async Task<Guid> UpdateGapsAssessmentInternalAsync(Guid id, GapsAssessmentEditDto gapsAssessmentUpdates)
        {
            var gapsAssessment = await _context.GapsAssessments.FindAsync(id) ?? throw new ArgumentException("Abandon Sites ID not found.", nameof(id));

            if (await GapsAssessmentNameExistsAsync(gapsAssessmentUpdates.Name, id))
            {
                throw new ArgumentException($"GAPS Assessment Name '{gapsAssessmentUpdates.Name}' already exist.");
            }

            gapsAssessment.Name = gapsAssessmentUpdates.Name;
            gapsAssessment.Description = gapsAssessmentUpdates.Description;

            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return gapsAssessment.Id;
        }

        public async Task UpdateGapsAssessmentStatusAsync(Guid id, bool active)
        {
            var gapsAssessment = await _context.GapsAssessments
                .SingleOrDefaultAsync(e => e.Id == id);
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
