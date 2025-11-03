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
    public class AbandonedInactiveRepository : IAbandonedInactiveRepository
    {
        public readonly FmsDbContext _context;
        public AbandonedInactiveRepository(FmsDbContext context) => _context = context;

        public Task<bool> AbandonedInactiveExistsAsync(Guid id) =>
            _context.AbandonedInactives.AnyAsync(e => e.Id == id);

        public Task<bool> AbandonedInactiveNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.AbandonedInactives.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> AbandonedInactiveDescriptionExistsAsync(string description, Guid? ignoreId = null) =>
            _context.AbandonedInactives.AnyAsync(e =>
                e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<IReadOnlyList<AbandonedInactiveSummaryDto>> GetAbandonedInactiveListAsync(bool activeOnly = false) => await _context.AbandonedInactives.AsNoTracking()
              .OrderByDescending(e => e.Active)
              .ThenBy(e => e.Name)
              .Where(e => e.Active || e.Active == activeOnly)
              .Select(e => new AbandonedInactiveSummaryDto(e))
              .ToListAsync();

        public async Task<AbandonedInactiveEditDto> GetAbandonedInactiveAsync(Guid id)
            {
            var abandonedInactive = await _context.AbandonedInactives.AsNoTracking().
                SingleOrDefaultAsync(e => e.Id == id);
            if (abandonedInactive == null)
            {
                return null;
            }
            return new AbandonedInactiveEditDto(abandonedInactive);
        }

        public Task<Guid> CreateAbandonedInactiveAsync(AbandonedInactiveCreateDto abandonedInactive)
        {
            Prevent.Null(abandonedInactive, nameof(abandonedInactive));
            Prevent.NullOrEmpty(abandonedInactive.Name, nameof(abandonedInactive.Name));
            return CreateAbandonedInactiveInternalAsync(abandonedInactive);
        }
        private async Task<Guid> CreateAbandonedInactiveInternalAsync(AbandonedInactiveCreateDto abandonedInactive)
        {
            if (await AbandonedInactiveNameExistsAsync(abandonedInactive.Name))
            {
                throw new ArgumentException($"Abandoned/Inactive {abandonedInactive.Name} already exist.");
            }
            var newAI = new AbandonedInactive(abandonedInactive);
            await _context.AbandonedInactives.AddAsync(newAI);
            await _context.SaveChangesAsync();
            return newAI.Id;
        }

        public async Task UpdateAbandonedInactiveAsync(Guid id, AbandonedInactiveEditDto abandonedInactive)
        {
            Prevent.Null(abandonedInactive, nameof(abandonedInactive));
            Prevent.NullOrEmpty(abandonedInactive.Name, nameof(abandonedInactive.Name));
            var existingAI = await _context.AbandonedInactives.
                SingleOrDefaultAsync(e => e.Id == id);
            if (existingAI == null)
            {
                throw new KeyNotFoundException($"Abandoned/Inactive with Id {id} not found.");
            }
            if (await AbandonedInactiveNameExistsAsync(abandonedInactive.Name, id))
            {
                throw new ArgumentException($"Abandoned/Inactive {abandonedInactive.Name} already exist.");
            }
            // Update properties 
            existingAI.Name = abandonedInactive.Name;
            existingAI.Description = abandonedInactive.Description;
            existingAI.Active = abandonedInactive.Active;

            await _context.SaveChangesAsync();
        }
        public async Task UpdateAbandonedInactiveStatusAsync(Guid id, bool active)
        {
            var existingAI = await _context.AbandonedInactives.
                SingleOrDefaultAsync(e => e.Id == id);
            if (existingAI == null)
            {
                throw new KeyNotFoundException($"Abandoned/Inactive with Id {id} not found.");
            }
            existingAI.Active = active;
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
        ~AbandonedInactiveRepository()
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
