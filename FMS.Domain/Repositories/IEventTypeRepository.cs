using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IEventTypeRepository : IDisposable
    {
        Task<bool> EventTypeExistsAsync(Guid id);

        Task<EventTypeEditDto> GetEventTypeByIdAsync(Guid id);

        Task<IReadOnlyList<EventTypeSummaryDto>> GetEventTypeListAsync();

        Task<Guid> CreateEventTypeAsync(EventTypeCreateDto eventType);

        Task UpdateEventTypeAsync(Guid id, EventTypeEditDto eventTypeUpdates);

        Task UpdateEventTypeStatusAsync(Guid id, bool active);
    }
}
