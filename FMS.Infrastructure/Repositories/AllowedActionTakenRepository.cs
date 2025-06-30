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
    public class AllowedActionTakenRepository
    {
        public readonly FmsDbContext _context;
        public AllowedActionTakenRepository(FmsDbContext context) => _context = context;


        public Task<bool> AllowedActionTakenExistsAsync(Guid id) =>
            _context.AllowedActionsTaken.AnyAsync(e => e.Id == id);

        public async Task<AllowedActionTakenEditDto> GetAllowedActionTakenAsync(Guid id)
        {
            Prevent.Null(id, nameof(id));

            return await _context.AllowedActionsTaken.AsNoTracking()
                .Where(e => e.Id == id)
                .Select(e => new AllowedActionTakenEditDto(e))
                .SingleOrDefaultAsync();
        }

        public async Task<IReadOnlyList<AllowedActionTakenSummaryDto>> GetAllowedActionTakenListAsync()
        {
            return await _context.AllowedActionsTaken.AsNoTracking()
                .OrderBy(e => e.EventType.Name)
                .ThenBy(e => e.ActionTaken.Name)
                .GroupBy(e => new { e.EventTypeId })
                .Select(e => new AllowedActionTakenSummaryDto((AllowedActionTaken)e))
                .ToListAsync();
        }

        public Task<Guid> CreateAllowedActionTakenAsync(AllowedActionTakenCreateDto allowedActionTaken)
        {
            Prevent.Null(allowedActionTaken, nameof(allowedActionTaken));
            Prevent.NullOrEmpty(allowedActionTaken.EventTypeId, nameof(allowedActionTaken.EventTypeId));
            Prevent.NullOrEmpty(allowedActionTaken.ActionTakenId, nameof(allowedActionTaken.ActionTakenId));

            return CreateAllowedActionTakenInternalAsync(allowedActionTaken);
        }

        private async Task<Guid> CreateAllowedActionTakenInternalAsync(AllowedActionTakenCreateDto allowedActionTaken)
        {
            if (await _context.AllowedActionsTaken.AnyAsync(e =>
                e.EventTypeId == allowedActionTaken.EventTypeId &&
                e.ActionTakenId == allowedActionTaken.ActionTakenId))
            {
                throw new ArgumentException($"Allowed Action Taken for Event Type {allowedActionTaken.EventTypeId} and Action Taken {allowedActionTaken.ActionTakenId} already exists.");
            }

            var newAllowedActionTaken = new AllowedActionTaken(allowedActionTaken);

            await _context.AllowedActionsTaken.AddAsync(newAllowedActionTaken);
            await _context.SaveChangesAsync();
            return newAllowedActionTaken.Id;
        }

        public Task UpdateAllowedActionTakenAsync(Guid id, AllowedActionTakenEditDto allowedActionTakenUpdates)
        {
            Prevent.Null(id, nameof(id));
            Prevent.Null(allowedActionTakenUpdates, nameof(allowedActionTakenUpdates));
            Prevent.NullOrEmpty(allowedActionTakenUpdates.EventTypeId, nameof(allowedActionTakenUpdates.EventTypeId));
            Prevent.NullOrEmpty(allowedActionTakenUpdates.ActionTakenId, nameof(allowedActionTakenUpdates.ActionTakenId));

            return UpdateAllowedActionTakenInternalAsync(id, allowedActionTakenUpdates);
        }

        private async Task UpdateAllowedActionTakenInternalAsync(Guid id, AllowedActionTakenEditDto allowedActionTakenUpdates)
        {
            if (await _context.AllowedActionsTaken.AnyAsync(e =>
                e.EventTypeId == allowedActionTakenUpdates.EventTypeId &&
                e.ActionTakenId == allowedActionTakenUpdates.ActionTakenId &&
                e.Id != id))
            {
                throw new ArgumentException($"Allowed Action Taken for Event Type {allowedActionTakenUpdates.EventTypeId} and Action Taken {allowedActionTakenUpdates.ActionTakenId} already exists.");
            }

            var existingAllowedActionTaken = await _context.AllowedActionsTaken.FindAsync(id);

            if (existingAllowedActionTaken == null)
            {
                throw new ArgumentException($"Allowed Action Taken with Id {id} does not exist.");
            }

            _context.AllowedActionsTaken.Update(existingAllowedActionTaken);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAllowedActionTakenStatusAsync(Guid id, bool active)
        {
            Prevent.Null(id, nameof(id));

            var existingAllowedActionTaken = await _context.AllowedActionsTaken.FindAsync(id);

            if (existingAllowedActionTaken == null)
            {
                throw new ArgumentException($"Allowed Action Taken with Id {id} does not exist.");
            }

            existingAllowedActionTaken.Active = active;

            _context.AllowedActionsTaken.Update(existingAllowedActionTaken);
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
        ~AllowedActionTakenRepository()
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
