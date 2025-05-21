using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    internal class ActionTakenRepository : IActionTakenRepository
    {
        public readonly FmsDbContext _context;
        public Task<bool> ActionTakenExistAsync(Guid id) =>
            _context.FacilityStatuses.AnyAsync(e => e.Id == id);
        public Task<bool> ActionTakenNameExistAsync(string status, Guid? ignoreId = null) =>
            _context.FacilityStatuses.AnyAsync(e =>
                e.Status == status && (!ignoreId.HasValue || e.Id != ignoreId.Value));
    }
}
