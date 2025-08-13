using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class AbandonSitesRepository : IAbandonSitesRepository
    {
        public readonly FmsDbContext _context;
        public AbandonSitesRepository(FmsDbContext context) => _context = context;

        public Task<bool> AbandonSitesExistsAsync(Guid id) =>
            _context.AbandonSites.AnyAsync(e => e.Id == id);

        public Task<bool> AbandonSitesNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.AbandonSites.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<AbandonSitesEditDto> GetAbandonSitesByIdAsync(Guid id) =>
            _context.AbandonSites.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new AbandonSitesEditDto(e))
                .SingleOrDefaultAsync();

        public async Task<IReadOnlyList<AbandonSitesSummaryDto>> GetAbandonSitesListAsync(bool ActiveOnly = false) => await _context.AbandonSites.AsNoTracking()
                .OrderByDescending(e => e.Name)
                .Where(e => e.Active || e.Active == ActiveOnly)
                .Select(e => new AbandonSitesSummaryDto(e))
                .ToListAsync();

        public async Task<Guid> CreateAbandonSitesAsync(AbandonSitesCreateDto abandonSite)
        {
            if (abandonSite == null)
            {
                throw new ArgumentNullException(nameof(abandonSite));
            }
            var newAbandonSite = new AbandonSites(abandonSite);

            await _context.AbandonSites.AddAsync(newAbandonSite);
            await _context.SaveChangesAsync();

            return newAbandonSite.Id;
        }

        public Task UpdateAbandonSitesAsync(Guid id, AbandonSitesEditDto abandonSitesUpdates)
        {
            Prevent.NullOrEmpty(abandonSitesUpdates.Name, nameof(abandonSitesUpdates.Name));
            return UpdateAbandonSitesInternalAsync(id, abandonSitesUpdates);
        }

        public async Task<Guid> UpdateAbandonSitesInternalAsync(Guid id, AbandonSitesEditDto abandonSitesUpdates)
        {
            var abandonSites = await _context.AbandonSites.FindAsync(id) ?? throw new ArgumentException("Abandon Sites ID not found.", nameof(id));

            if (await AbandonSitesNameExistsAsync(abandonSitesUpdates.Name, id))
            {
                throw new ArgumentException($"Abandon Sites Name '{abandonSitesUpdates.Name}' already exist.");
            }

            abandonSites.Name = abandonSitesUpdates.Name;
            abandonSites.Description = abandonSitesUpdates.Description;

            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return abandonSites.Id;
        }

        public async Task UpdateAbandonSitesStatusAsync(Guid id, bool active)
        {
            var existingAbandonSite = await _context.AbandonSites
                .SingleOrDefaultAsync(e => e.Id == id);
            if (existingAbandonSite == null)
            {
                throw new KeyNotFoundException($"Abandon Site with ID {id} not found.");
            }

            existingAbandonSite.Active = !existingAbandonSite.Active;

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
        ~AbandonSitesRepository()
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
