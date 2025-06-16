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
    
    public class OverallStatusRepository : IOverallStatusRepository
    {
        private readonly FmsDbContext _context;
        public OverallStatusRepository(FmsDbContext context) => _context = context;

        public Task<bool> OverallStatusExistsAsync(Guid id) =>
            _context.OverallStatuses.AnyAsync(e => e.Id == id);

        public Task<bool> OverallStatusNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.OverallStatuses.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> OverallStatusDescriptionExistsAsync(string description, Guid? ignoreId = null) =>
            _context.OverallStatuses.AnyAsync(e =>
                e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<OverallStatusEditDto> GetOverallStatusAsync(Guid id)
        {
            var overallStatus = await _context.OverallStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return overallStatus == null ? null : new OverallStatusEditDto(overallStatus);
        }

        public async Task<string> GetOverallStatusNameAsync(Guid? id)
        {
            var overallStatus = await _context.OverallStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return overallStatus?.Name;
        }

        public async Task<IReadOnlyList<OverallStatusSummaryDto>> GetOverallStatusListAsync() =>
            await _context.OverallStatuses.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new OverallStatusSummaryDto(e))
                .ToListAsync();

        public Task<Guid> CreateOverallStatusAsync(OverallStatusCreateDto overallStatus)
        {
            Prevent.Null(overallStatus, nameof(overallStatus));
            Prevent.NullOrEmpty(overallStatus.Name, nameof(overallStatus.Name));

            return CreateOverallStatusInternalAsync(overallStatus);
        }

        public async Task<Guid> CreateOverallStatusInternalAsync(OverallStatusCreateDto overallStatus)
        {
            if (await OverallStatusNameExistsAsync(overallStatus.Name))
            {
                throw new ArgumentException($"Overall Ststus '{overallStatus.Name}' already exists.");
            }

            if (await OverallStatusDescriptionExistsAsync(overallStatus.Description))
            {
                throw new ArgumentException($"Overall Status description '{overallStatus.Description}' already exists.");
            }

            var newOverallStatus = new OverallStatus(overallStatus);
            newOverallStatus.TrimAll();

            await _context.OverallStatuses.AddAsync(newOverallStatus);
            await _context.SaveChangesAsync();

            return newOverallStatus.Id;
        }

        public Task UpdateOverallStatusAsync(Guid id, OverallStatusEditDto overallStatusUpdates)
        {
            Prevent.NullOrWhiteSpace(overallStatusUpdates.Name, nameof(overallStatusUpdates.Name));
            return UpdateOverallStatusInternalAsync(id, overallStatusUpdates);
        }

        private async Task UpdateOverallStatusInternalAsync(Guid id, OverallStatusEditDto overallStatusUpdates)
        {
            var overallStatus = await _context.OverallStatuses.FindAsync(id);

            if (overallStatus == null)
            {
                throw new ArgumentException("Overall Status ID not found.", nameof(id));
            }

            if (await OverallStatusNameExistsAsync(overallStatusUpdates.Name, id))
            {
                throw new ArgumentException($"Overall Status Name: '{overallStatusUpdates.Name}' already exists.");
            }

            if (await OverallStatusDescriptionExistsAsync(overallStatusUpdates.Description, id))
            {
                throw new ArgumentException(
                    $"Overall Status description: '{overallStatusUpdates.Description}' already exists.");
            }

            overallStatus.Name = overallStatusUpdates.Name;
            overallStatus.Description = overallStatusUpdates.Description;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateOverallStatusStatusAsync(Guid id, bool active)
        {
            var overallStatus = await _context.OverallStatuses.FindAsync(id);

            if (overallStatus == null)
            {
                throw new ArgumentException("Overall Status ID not found");
            }

            overallStatus.Active = active;

            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~OverallStatusRepository()
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
