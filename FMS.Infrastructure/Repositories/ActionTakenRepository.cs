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
    internal class ActionTakenRepository : IActionTakenRepository
    {
        public readonly FmsDbContext _context;
        public ActionTakenRepository(FmsDbContext context) => _context = context;
        public Task<bool> ActionTakenExistsAsync(Guid id) =>
            _context.ActionTaken.AnyAsync(e => e.Id == id);
        public Task<bool> ActionTakenNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.ActionTaken.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<ActionTakenEditDto> GetActionTakenAsync(Guid id)
        {
            var actionTaken = await _context.ActionTaken.AsNoTracking().
                SingleOrDefaultAsync(e => e.Id == id);
            if (actionTaken == null)
            {
                return null;
            }
            return new ActionTakenEditDto(actionTaken);
        }

        public async Task<IReadOnlyList<ActionTakenSummaryDto>> GetActionTakenListAsync() =>
            await _context.ActionTaken.AsNoTracking()
            .OrderBy(e => e.Name)
            .Select(e => new ActionTakenSummaryDto(e));
            .ToListAsync();

        public Task<Guid> CreateActionTakenAsync(ActionTakenCreateDto actionTaken)
        {
            Prevent.Null(actionTaken, nameof(actionTaken));
            Prevent.NullOrEmpty(actionTaken.Name, nameof(actionTaken.Name));

            return CreateActionTakenInternalAsync(actionTaken);
        }
        private async Task<Guid> CreateActionTakenInternalAsync(ActionTakenCreateDto actionTaken)
        {
            if (await ActionTakenNameExistsAsync(actionTaken.Name))
            {
                throw new ArgumentException($"Action Taken {actionTaken.Name} already exist.");
            }

            var newAT = new ActionTaken(actionTaken);
            await _context.ActionTaken.AddAsync(newAT);
            await _context.SaveChangesAsync();

            return newAT.Id;
        }
        public Task UpdateActionTakenAsync(Guid id, ActionTakenEditDto actionTaken)
        {
            Prevent.NullOrEmpty(actionTaken.Name, nameof(actionTaken.Name));
            return UpdateActionTakenInternalAsync(id, actionTaken);
        }
        private async Task<Guid> UpdateActionTakenInternalAsync(Guid id,
            ActionTakenEditDto actionTakenUpdates)
        {
            var actionTaken = await _context.ActionTaken.FindAsync(id);

            if (actionTaken == null)
            {
                throw new ArgumentException("Action Taken ID not found.", nameof(id));
            }

            if (await ActionTakenNameExistsAsync(actionTakenUpdates.Name, id))
            {
                throw new ArgumentException(
                    $"Action Taken Name '{actionTakenUpdates.Name}' already exist.");
            }
            actionTaken.Name = actionTakenUpdates.Name;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateActionTakenStatusAsync(Guid id, bool active)
        {
            var actionTaken = await _context.ActionTaken.FindAsync(id);
            if (actionTaken == null)
            {
                throw new ArgumentException("Action Taken ID not found");
            }
            actionTaken.Active = active;
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
        ~ActionTakenRepository()
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
