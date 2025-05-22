using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    internal class ActionTakenRepository : IActionTakenRepository
    {
        public readonly FmsDbContext _context;
        public ActionTakenRepository(FmsDbContext context) => _context = context;
        public Task<bool> ActionTakenExistAsync(Guid id) =>
            _context.ActionTaken.AnyAsync(e => e.Id == id);
        public Task<bool> ActionTakenNameExistAsync(string status, Guid? ignoreId = null) =>
            _context.ActionTaken.AnyAsync(e =>
                e.Name == status && (!ignoreId.HasValue || e.Id != ignoreId.Value));

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

        public Task UpdateActionTakenAsync(Guid id, ActionTakenEditDto actionTaken)
        {
            Prevent.NullOrEmpty(actionTaken.Name, nameof(actionTaken.Name));
            return UpdateActionTakenAsync(id, actionTaken);
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
    }
}
