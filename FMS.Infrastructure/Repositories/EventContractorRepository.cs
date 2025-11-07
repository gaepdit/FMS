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
    public class EventContractorRepository 
    {
        public readonly FmsDbContext _context;
        public EventContractorRepository(FmsDbContext context) => _context = context;

        public async Task<bool> EventContractorExistsAsync(Guid id) =>
            await _context.EventContractors.AnyAsync(e => e.Id == id);
        public async Task<EventContractor> GetEventContractorByIdAsync(Guid id) =>
            await _context.EventContractors.FindAsync(id);

        public async Task<IReadOnlyList<EventContractorSummaryDto>> GetEventContractorListAsync(bool activeOnly = true) => await _context.EventContractors.AsNoTracking()
            .OrderByDescending(e => e.Active)
           .ThenBy(e => e.Name)
           .Where(e => e.Active || e.Active == activeOnly)
           .Select(e => new EventContractorSummaryDto(e))
           .ToListAsync();

        public async Task CreateEventContractorAsync(EventContractorCreateDto contractor)
        {
            Prevent.Null(contractor, nameof(contractor));
            Prevent.NullOrEmpty(contractor.Name, nameof(contractor.Name));
            if (await _context.EventContractors.AnyAsync(e => e.Name == contractor.Name))
            {
                throw new ArgumentException($"Event Contractor {contractor.Name} already exist.");
            }
            var newContractor = new EventContractor(contractor);
            _context.EventContractors.Add(newContractor);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEventContractorAsync(EventContractorEditDto contractor)
        {
            var existingContractor = await _context.EventContractors.FindAsync(contractor.Id);
            if (existingContractor != null)
            {
                existingContractor.Name = contractor.Name;
                existingContractor.Description = contractor.Description;
                existingContractor.Active = contractor.Active;

                _context.EventContractors.Update(existingContractor);
                await _context.SaveChangesAsync();
            }
        }
    }
}
