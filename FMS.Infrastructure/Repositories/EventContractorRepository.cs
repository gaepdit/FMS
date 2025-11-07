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
    public class EventContractorRepository : IEventContractorRepository
    {
        public readonly FmsDbContext _context;
        public EventContractorRepository(FmsDbContext context) => _context = context;

        public async Task<bool> EventContractorExistsAsync(Guid id) =>
            await _context.EventContractors.AnyAsync(e => e.Id == id);
        public async Task<EventContractor> GetEventContractorByIdAsync(Guid id) =>
            await _context.EventContractors.FindAsync(id);

        public async Task<IReadOnlyList<EventContractorSummaryDto>> GetAllEventContractorsAsync(bool activeOnly = true) => await _context.EventContractors.AsNoTracking()
            .OrderByDescending(e => e.Active)
           .ThenBy(e => e.Name)
           .Where(e => e.Active || e.Active == activeOnly)
           .Select(e => new EventContractorSummaryDto(e))
           .ToListAsync();

        public async Task CreateEventContractorAsync(EventContractorCreateDto contractor) =>
            await _context.EventContractors.AddAsync(new EventContractor(contractor));

        public async Task UpdateEventContractorAsync(EventContractorEditDto contractor) =>
            _context.EventContractors.Update(new EventContractor(contractor));
    }
}
