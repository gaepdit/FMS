using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;

namespace FMS
{
    public interface IAllowedActionTakenHelper
    {
        // Define methods that will be implemented in the AllowedActionTakenHelper class
        Task<IList<AllowedActionTakenSpec>> GetAllowedActionTakenListByEventIdAsync(Guid eventTypeId);
    }

    public class AllowedActionTakenHelper : IAllowedActionTakenHelper
    {
        private readonly IAllowedActionTakenRepository _repository;
        private readonly IActionTakenRepository _actionTakenRepository;
        private readonly IEventTypeRepository _eventTypeRepository;
        public AllowedActionTakenHelper(
            IAllowedActionTakenRepository repository,
            IActionTakenRepository actionTakenRepository,
            IEventTypeRepository eventTypeRepository)
        {
            _repository = repository;
            _actionTakenRepository = actionTakenRepository;
            _eventTypeRepository = eventTypeRepository;
        }

        public async Task<IList<AllowedActionTakenSpec>> GetAllowedActionTakenListByEventIdAsync(Guid eventTypeId)
        {
            if (eventTypeId == Guid.Empty)
            {
                throw new ArgumentException("Event ID cannot be empty.", nameof(eventTypeId));
            }

            EventTypeEditDto currentEventTypeEditDto = await _eventTypeRepository.GetEventTypeByIdAsync(eventTypeId);

            if (currentEventTypeEditDto == null)
            {   
                throw new ArgumentException($"Event Type with ID {eventTypeId} does not exist.", nameof(eventTypeId));
            }

            IReadOnlyList<ActionTakenSummaryDto> actionTakenList = await _actionTakenRepository.GetActionTakenListAsync();

            if (actionTakenList == null || actionTakenList.Count == 0)
            {
                throw new InvalidOperationException("No action taken records found.");
            }
            
            IList<AllowedActionTakenSpec> allowedActionsTaken = await _repository.GetAllowedActionTakenListAsync(eventTypeId);

            if (allowedActionsTaken == null)
            {
                throw new InvalidOperationException($"No allowed actions taken found for event type with ID {eventTypeId}.");
            }

            EventType newEventType = new(currentEventTypeEditDto);

            foreach (var actionTaken in actionTakenList)
            {
                var newActionTaken = new ActionTaken(actionTaken);

                if (!allowedActionsTaken.Any(e => e.EventTypeId == newEventType.Id && e.ActionTakenId == newActionTaken.Id))
                {
                    var newAAT = new AllowedActionTaken
                    {
                        Id = Guid.NewGuid(),
                        Active = false,
                        ActionTaken = newActionTaken,
                        EventType = newEventType
                    };
                    allowedActionsTaken.Add(new AllowedActionTakenSpec(newAAT));
                }
            }

            return allowedActionsTaken
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.ActionTakenName)
                .ThenBy(e => e.EventTypeActive)
                .ThenBy(e => e.ActionTakenActive)
                .ToList();
        }
    }
}
