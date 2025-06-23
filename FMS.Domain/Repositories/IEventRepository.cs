using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IEventRepository : IDisposable
    {
        Task<bool> EventExistsAsync(Guid id);

        Task<EventEditDto> GetEventByIdAsync(Guid id);

        Task<IEnumerable<Event>> GetEventsByFacilityIdAsync(Guid facilityId);

        Task<IEnumerable<Event>> GetEventsByFacilityIdAndStatusAsync(Guid facilityId, string status);

        Task<IEnumerable<Event>> GetEventsByFacilityIdAndParentIdAsync(Guid facilityId, Guid parentId);

        Task AddEventAsync(EventCreateDto eventDto);

        Task UpdateEventAsync(EventEditDto eventDto);

        Task UpdateEventStatusAsync(Guid id, bool active);
    }
}
