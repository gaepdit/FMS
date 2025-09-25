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

        Task<EventSummaryDto> GetEventSummaryByIdAsync(Guid id);

        Task<IEnumerable<EventSummaryDto>> GetEventsByFacilityIdAsync(Guid facilityId);

        Task<IEnumerable<EventSummaryDto>> GetEventsByFacilityIdAndParentIdAsync(Guid facilityId, Guid parentId);

        Task<IReadOnlyList<ActionTakenSummaryDto>> GetActionTakenListByEventType(EventTypeSummaryDto eventType);

        Task<Guid> CreateEventAsync(EventCreateDto eventDto);

        Task UpdateEventAsync(EventEditDto eventDto);

        Task UpdateEventStatusAsync(Guid id, bool active);

        Task DeleteEventByIdAsync(Guid id);
    }
}
