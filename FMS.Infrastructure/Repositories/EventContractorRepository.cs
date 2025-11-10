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
    public class EventContractorRepository :IEventContractorRepository
    {
        public readonly FmsDbContext _context;
        public EventContractorRepository(FmsDbContext context) => _context = context;

        public async Task<bool> EventContractorExistsAsync(Guid id) =>
            await _context.EventContractors.AnyAsync(e => e.Id == id);
        public async Task<EventContractorEditDto> GetEventContractorByIdAsync(Guid id)
        {
            var eventContractor = await _context.EventContractors.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
            if (eventContractor == null)
            {
                return null;
            }
            return new EventContractorEditDto(eventContractor);
        }
           
        public async Task<IReadOnlyList<EventContractorSummaryDto>> 
            GetEventContractorListAsync(bool activeOnly = true) => 
            await _context.EventContractors.AsNoTracking()
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

        public async Task UpdateEventContractorStatusAsync(Guid id)
        {
            var existingContractor = await _context.EventContractors.FindAsync(id);
            if (existingContractor != null)
            {
                var status = existingContractor.Active;
                existingContractor.Active = !status;

                _context.EventContractors.Update(existingContractor);
                await _context.SaveChangesAsync();
            }
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
        ~EventContractorRepository()
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
