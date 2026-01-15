using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IEventTypeRepository : IDisposable
    {
        Task<bool> EventTypeExistsAsync(Guid id);

        Task<bool> EventTypeNameExistsAsync(string name, Guid? ignoreId = null);

        Task<EventTypeEditDto> GetEventTypeByIdAsync(Guid id);

        Task<string> GetEventTypeNameAsync(Guid? id);

        Task<IReadOnlyList<EventTypeSummaryDto>> GetEventTypeListAsync();

        Task<Guid> CreateEventTypeAsync(EventTypeCreateDto eventType);

        Task UpdateEventTypeAsync(Guid id, EventTypeEditDto eventTypeUpdates);

        Task UpdateEventTypeStatusAsync(Guid id, bool active);
    }
}
