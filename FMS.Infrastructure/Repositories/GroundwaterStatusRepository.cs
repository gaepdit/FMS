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
    public class GroundwaterStatusRepository : IGroundwaterStatusRepository
    {
        public readonly FmsDbContext _context;
        public GroundwaterStatusRepository(FmsDbContext context) => _context = context;

        // Implement interface methods
        public Task<bool> GroundwaterStatusExistsAsync(Guid id) =>
            _context.GroundwaterStatuses.AnyAsync(e => e.Id == id);

        public Task<bool> GroundwaterStatusNameExistsAsync(string name, Guid? ignoreId = null) => _context.GroundwaterStatuses.AnyAsync(e => e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> GroundwaterStatusDescriptionExistsAsync(string description, Guid? ignoreId = null) => _context.GroundwaterStatuses.AnyAsync(e => e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<GroundwaterStatusEditDto> GetGroundwaterStatusAsync(Guid id)
        {
            var groundwaterStatus = await _context.GroundwaterStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (groundwaterStatus == null)
            {
                return null;
            }

            return new GroundwaterStatusEditDto(groundwaterStatus);
        }

        public async Task<IReadOnlyList<GroundwaterStatusSummaryDto>> GetGroundwaterStatusListAsync() => await _context.GroundwaterStatuses.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new GroundwaterStatusSummaryDto(e))
            .ToListAsync();

        public async Task<string> GetGroundwaterStatusNameAsync(Guid? id)
        {
            var groundwaterStatus = await _context.GroundwaterStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return groundwaterStatus?.Name;
        }

        public Task<Guid> CreateGroundwaterStatusAsync(GroundwaterStatusCreateDto groundwaterStatus)
        {
            Prevent.Null(groundwaterStatus, nameof(groundwaterStatus));
            Prevent.NullOrEmpty(groundwaterStatus.Name, nameof(groundwaterStatus.Name));
            Prevent.NullOrEmpty(groundwaterStatus.Description, nameof(groundwaterStatus.Description));

            return CreateGroundwaterStatusInternalAsync(groundwaterStatus);
        }

        private async Task<Guid> CreateGroundwaterStatusInternalAsync(GroundwaterStatusCreateDto groundwaterStatus)
        {
            if (await GroundwaterStatusNameExistsAsync(groundwaterStatus.Name))
            {
                throw new ArgumentException($"Groundwater Status Name {groundwaterStatus.Name} already exists.");
            }

            if (await GroundwaterStatusDescriptionExistsAsync(groundwaterStatus.Description))
            {
                throw new ArgumentException($"Groundwater Status Description {groundwaterStatus.Description} already exists.");
            }

            var newGroundwaterStatus = new GroundwaterStatus(groundwaterStatus);

            await _context.GroundwaterStatuses.AddAsync(newGroundwaterStatus);
            await _context.SaveChangesAsync();

            return newGroundwaterStatus.Id;
        }

        public async Task UpdateGroundwaterStatusAsync(Guid id, GroundwaterStatusEditDto groundwaterStatusUpdates)
        {
            Prevent.NullOrEmpty(groundwaterStatusUpdates.Name, nameof(groundwaterStatusUpdates.Name));

            await UpdateGroundwaterStatusInternalAsync(id, groundwaterStatusUpdates);
        }

        private async Task<Guid> UpdateGroundwaterStatusInternalAsync(Guid id, GroundwaterStatusEditDto groundwaterStatusUpdates)
        {
            var groundwaterStatus = await _context.GroundwaterStatuses.FindAsync(id) ?? throw new ArgumentException("Groundwater Status ID not found.", nameof(id));

            groundwaterStatus.Name = groundwaterStatusUpdates.Name;
            groundwaterStatus.Description = groundwaterStatusUpdates.Description;

            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return groundwaterStatus.Id;
        }

        public async Task UpdateGroundwaterStatusStatusAsync(Guid id, bool active)
        {
            var groundwaterStatus = await _context.GroundwaterStatuses.FindAsync(id)
                ?? throw new ArgumentException("Groundwater Status ID not found");

            groundwaterStatus.Active = active;

            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~GroundwaterStatusRepository()
        {
            Dispose(false);
        }

        // Implement IDisposable pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here if necessary
                }

                // Dispose unmanaged resources here if necessary
                _disposed = true;
            }
        }

        #endregion
    }
}
