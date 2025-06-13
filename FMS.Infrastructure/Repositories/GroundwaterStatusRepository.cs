using FMS.Domain.Dto;
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
    public class GroundwaterStatusRepository : IGroundwaterStatusRepository
    {
        public readonly FmsDbContext _context;
        public GroundwaterStatusRepository(FmsDbContext context) => _context = context;

        // Implement interface methods
        public Task<bool> GroundwaterStatusExistsAsync(Guid id) =>
            _context.GroundwaterStatuses.AnyAsync(e => e.Id == id);

        public Task<bool> GroundwaterStatusNameExistsAsync(string name, Guid? ignoreId = null) => _context.BudgetCodes.AnyAsync(e => e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

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
           .OrderBy(e => e.Name)
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
            throw new NotImplementedException();
        }

        

        public Task<bool> GroundwaterStatusDescriptionExistsAsync(string description, Guid? ignoreId = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGroundwaterStatusAsync(Guid id, GroundwaterStatusEditDto groundwaterStatusUpdates)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGroundwaterStatusStatusAsync(Guid id, bool active)
        {
            throw new NotImplementedException();
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
