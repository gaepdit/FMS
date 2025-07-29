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
    public class EventTypeRepository : IEventTypeRepository
    {
        public readonly FmsDbContext _context;
        public EventTypeRepository(FmsDbContext context) => _context = context;


        // Implement interface methods
        public Task<bool> EventTypeExistsAsync(Guid id) =>
            _context.EventTypes.AnyAsync(e => e.Id == id);

        public Task<bool> EventTypeNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.EventTypes.AnyAsync(e => 
            e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<EventTypeEditDto> GetEventTypeByIdAsync(Guid id)
        {
            EventType eventType = await _context.EventTypes.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (eventType == null)
            {
                return null;
            }
            return new EventTypeEditDto(eventType);
        }

        public async Task<string> GetEventTypeNameAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            string eventType = await _context.EventTypes.AsNoTracking()
                .Where(e => e.Id == id.Value)
                .Select(e => e.Name)
                .SingleOrDefaultAsync();
            return eventType;
        }

        public async Task<IReadOnlyList<EventTypeSummaryDto>> GetEventTypeListAsync() => 
            await _context.EventTypes.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new EventTypeSummaryDto(e))
            .ToListAsync();

        public async Task<Guid> CreateEventTypeAsync(EventTypeCreateDto eventType)
        {
            Prevent.Null(eventType, nameof(eventType));
            Prevent.NullOrEmpty(eventType.Name, nameof(eventType.Name));

            return await CreateEventTypeInternalAsync(eventType);
        }

        private async Task<Guid> CreateEventTypeInternalAsync(EventTypeCreateDto eventType)
        {
            var newEventType = new EventType(eventType);

            await _context.EventTypes.AddAsync(newEventType);
            await _context.SaveChangesAsync();

            return newEventType.Id;
        }

        public Task UpdateEventTypeAsync(Guid id, EventTypeEditDto eventTypeUpdates)
        {
            Prevent.NullOrEmpty(eventTypeUpdates.Name, nameof(eventTypeUpdates.Name));

            return UpdateEventTypeInternalAsync(id, eventTypeUpdates);
        }

        private async Task<Guid> UpdateEventTypeInternalAsync(Guid id, EventTypeEditDto eventTypeUpdates)
        {
            var eventType = await _context.EventTypes.FindAsync(id) ?? throw new ArgumentException("Event Type ID not found.", nameof(id));

            // Check if the name already exists for another Event Type
            if (await EventTypeNameExistsAsync(eventTypeUpdates.Name, id))
            {
                throw new ArgumentException($"Event Type Name : '{eventTypeUpdates.Name}' already exists.");
            }

            eventType.Name = eventTypeUpdates.Name;

            _context.EventTypes.Update(eventType);
            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return eventType.Id;
        }

        public async Task UpdateEventTypeStatusAsync(Guid id, bool active)
        {
            var eventType = await _context.EventTypes.FindAsync(id)
                ?? throw new ArgumentException("Event Type ID not found");

            eventType.Active = active;

            _context.EventTypes.Update(eventType);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~EventTypeRepository()
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
