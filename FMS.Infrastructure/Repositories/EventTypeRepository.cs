using FMS.Domain.Dto;
using FMS.Domain.Entities;
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
    public class EventTypeRepository : IEventTypeRepository
    {
        public readonly FmsDbContext _context;
        public EventTypeRepository(FmsDbContext context) => _context = context;


        // Implement interface methods
        public Task<bool> EventTypeExistsAsync(Guid id) =>
            _context.EventTypes.AnyAsync(e => e.Id == id);

        public Task<EventTypeEditDto> GetEventTypeByIdAsync(Guid id)
        {
            // Implementation logic here
            return Task.FromResult<EventTypeEditDto>(null);
        }

        public Task<IReadOnlyList<EventTypeSummaryDto>> GetEventTypeListsAsync()
        {
            // Implementation logic here
            return Task.FromResult<IReadOnlyList<EventTypeSummaryDto>>(new List<EventTypeSummaryDto>());
        }

        public Task<Guid> CreateEventTypeAsync(EventTypeCreateDto eventType)
        {
            // Implementation logic here
            return Task.FromResult(Guid.NewGuid());
        }

        public Task UpdateEventTypeAsync(Guid id, EventTypeEditDto eventTypeUpdates)
        {
            // Implementation logic here
            return Task.CompletedTask;
        }

        public Task UpdateEventTypeStatusAsync(Guid id, bool active)
        {
            // Implementation logic here
            return Task.CompletedTask;
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
