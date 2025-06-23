using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly FmsDbContext _context;
        public EventRepository(FmsDbContext context) => _context = context;

        public Task<bool> EventExistsAsync(Guid id) =>
            _context.Events.AnyAsync(e => e.Id == id);

        public async Task<EventEditDto> GetEventByIdAsync(Guid id)
        {
            var Event = await _context.Events.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return Event == null ? null : new EventEditDto(Event);
        }

        public Task<IEnumerable<Event>> GetEventsByFacilityIdAsync(Guid facilityId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetEventsByFacilityIdAndStatusAsync(Guid facilityId, string status)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Event>> GetEventsByFacilityIdAndParentIdAsync(Guid facilityId, Guid parentId)
        {
            throw new NotImplementedException();
        }

        public Task AddEventAsync(EventCreateDto eventDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEventAsync(EventEditDto eventDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateEventStatusAsync(Guid id, bool active)
        {
            throw new NotImplementedException();
        }


        #region IDisposable Support

        private bool _disposedValue; // Corrected: 'private' modifier now precedes the member type

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
        ~EventRepository()
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
