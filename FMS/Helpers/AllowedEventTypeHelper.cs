using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;

namespace FMS
{
    public interface IAllowedEventTypeHelper
    {
        // Define methods that will be implemented in the AllowedEventTypeHelper class
        Task<IList<AllowedEventTypeSpec>> GetAllowedEventTypeListByFacilityTypeIdAsync(Guid facilityTypeId);
    }

    public class AllowedEventTypeHelper : IAllowedEventTypeHelper
    {
        private readonly IAllowedEventTypeRepository _repository;
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IFacilityTypeRepository _facilityTypeRepository;
        public AllowedEventTypeHelper(
            IAllowedEventTypeRepository repository,
            IEventTypeRepository eventTypeRepository,
            IFacilityTypeRepository facilityTypeRepository)
        {
            _repository = repository;
            _eventTypeRepository = eventTypeRepository;
            _facilityTypeRepository = facilityTypeRepository;
        }

        public async Task<IList<AllowedEventTypeSpec>> GetAllowedEventTypeListByFacilityTypeIdAsync(Guid facilityTypeId)
        {
            if (facilityTypeId == Guid.Empty)
            {
                throw new ArgumentException("Facility Type ID cannot be empty.", nameof(facilityTypeId));
            }

            FacilityTypeEditDto currentFacilityTypeEditDto = await _facilityTypeRepository.GetFacilityTypeAsync(facilityTypeId);

            if (currentFacilityTypeEditDto == null)
            {
                throw new ArgumentException($"Facility Type with ID {facilityTypeId} does not exist.", nameof(facilityTypeId));
            }

            IReadOnlyList<EventTypeSummaryDto> eventTypeList = await _eventTypeRepository.GetEventTypeListAsync();

            if (eventTypeList == null || eventTypeList.Count == 0)
            {
                throw new InvalidOperationException("No event type records found.");
            }

            IList<AllowedEventTypeSpec> allowedEventTypes = await _repository.GetAllowedEventTypeListAsync(facilityTypeId);

            if (allowedEventTypes == null)
            {
                throw new InvalidOperationException($"No allowed event types found for facility type with ID {facilityTypeId}.");
            }

            FacilityType newFacilityType = new(currentFacilityTypeEditDto);

            foreach (var eventType in eventTypeList)
            {
                var newEventType = new EventType(eventType);

                if (!allowedEventTypes.Any(e => e.FacilityTypeId == newFacilityType.Id && e.EventTypeId == newEventType.Id))
                {
                    var newAET = new AllowedEventType
                    {
                        Id = Guid.NewGuid(),
                        Active = false,
                        EventType = newEventType,
                        FacilityType = newFacilityType
                    };
                    allowedEventTypes.Add(new AllowedEventTypeSpec(newAET));
                }
            }

            return allowedEventTypes
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.EventTypeName)
                .ThenBy(e => e.FacilityTypeActive)
                .ThenBy(e => e.EventTypeActive)
                .ToList();
        }
    }
}

