using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    internal class ActionTakenRepository : IActionTakenRepository
    {
        public readonly FmsDbContext _context;
        public FacilityStatusRepository(FmsDbContext context) => _context = context;
        public Task<bool> ActionTakenExistAsync(Guid id) =>
            _context.FacilityStatuses.AnyAsync(e => e.Id == id);
        public Task<bool> ActionTakenNameExistAsync(string status, Guid? ignoreId = null) =>
            _context.FacilityStatuses.AnyAsync(e =>
                e.Status == status && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<ActionTakenEditDto> GetActionTakenAsync(Guid id)
        {
            var actionTaken = await _context.
        };

        public Task<IReadOnlyList<ActionTakenSummaryDto>> GetActionTakenListAsync;

        public Task<Guid> CreateActionTakenAsync;

        public Task UpdateActionTakenAsync;

        public Task UpdateActionTakenStatusAsync;
    }
}
