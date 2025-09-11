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
            var newEvent = await _context.Events.AsNoTracking()
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Include(e => e.ComplianceOfficer)
                .SingleOrDefaultAsync(e => e.Id == id);

            var eventSummary = newEvent == null ? null : new EventSummaryDto(newEvent);

            return eventSummary == null ? null : new EventEditDto(eventSummary);
        }

        public async Task<IEnumerable<EventSummaryDto>> GetEventsByFacilityIdAsync(Guid facilityId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));

            var events = await _context.Events
                   .AsNoTracking()
                   .Include(e => e.EventType)
                   .Include(e => e.ActionTaken)
                   .Include(e => e.ComplianceOfficer)
                   .Where(e => e.FacilityId == facilityId)
                   .ToListAsync();

            events = events
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.StartDate)
                .GroupBy(e => e.ParentId?.ToString() ?? string.Empty)
                .SelectMany(g => g)
                .ToList();

            return events.Select(e => new EventSummaryDto(e)).ToList();
        }

        public async Task<IEnumerable<EventSummaryDto>> GetEventsByFacilityIdAndParentIdAsync(Guid facilityId, Guid parentId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));

            return await _context.Events.AsNoTracking()
                .Include(e => e.EventType)
                .Include(e => e.ActionTaken)
                .Include(e => e.ComplianceOfficer)
                .OrderByDescending(e => e.Active)
                .Where(e => e.FacilityId == facilityId && e.ParentId == parentId)
                .Select(e => new EventSummaryDto(e))
                .ToListAsync();
        }

        public Task<Guid> CreateEventAsync(EventCreateDto eventDto)
        {
            Prevent.Null(eventDto, nameof(eventDto));
            Prevent.NullOrEmpty(eventDto.EventTypeId, nameof(eventDto.EventTypeId));
            Prevent.NullOrEmpty(eventDto.FacilityId, nameof(eventDto.FacilityId));

            return AddEventInternalAsync(eventDto);
        }

        private async Task<Guid> AddEventInternalAsync(EventCreateDto eventDto)
        {
            var newEvent = new Event
            {
                Id = Guid.NewGuid(),
                FacilityId = eventDto.FacilityId,
                ParentId = eventDto.ParentId,
                EventTypeId = eventDto.EventTypeId,
                ActionTakenId = eventDto.ActionTakenId,
                StartDate = eventDto.StartDate,
                DueDate = eventDto.DueDate,
                CompletionDate = eventDto.CompletionDate,
                ComplianceOfficerId = eventDto.ComplianceOfficerId,
                EventAmount = eventDto.EventAmount,
                EntityNameOrNumber = eventDto.EntityNameOrNumber,
                Comment = eventDto.Comment
            };

            _context.Events.Add(newEvent);
            await _context.SaveChangesAsync();
            return newEvent.Id;
        }

        public Task UpdateEventAsync(EventEditDto eventDto)
        {
            Prevent.Null(eventDto, nameof(eventDto));
            Prevent.NullOrEmpty(eventDto.Id, nameof(eventDto.Id));

            return UpdateEventInternalAsync(eventDto);
        }

        private async Task UpdateEventInternalAsync(EventEditDto eventDto)
        {
            
            var existingEvent = await _context.Events.FindAsync(eventDto.Id);
            if (existingEvent == null) throw new InvalidOperationException("Event not found");

            existingEvent.FacilityId = eventDto.FacilityId;
            existingEvent.ParentId = eventDto.ParentId;
            existingEvent.EventTypeId = eventDto.EventTypeId;
            existingEvent.ActionTakenId = eventDto.ActionTakenId;
            existingEvent.StartDate = eventDto.StartDate;
            existingEvent.DueDate = eventDto.DueDate;
            existingEvent.CompletionDate = eventDto.CompletionDate;
            existingEvent.ComplianceOfficerId = eventDto.ComplianceOfficerId;
            existingEvent.EventAmount = eventDto.EventAmount;
            existingEvent.EntityNameOrNumber = eventDto.EntityNameOrNumber;
            existingEvent.Comment = eventDto.Comment;

            _context.Events.Update(existingEvent);
            await _context.SaveChangesAsync();
        }

        public Task UpdateEventStatusAsync(Guid id, bool active)
        {
            var eventToUpdate = _context.Events.Find(id);
            if (eventToUpdate == null)
            {
                throw new InvalidOperationException($"Event with ID {id} does not exist.");
            }
            eventToUpdate.Active = active;

            _context.Events.Update(eventToUpdate);
            return _context.SaveChangesAsync();
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
