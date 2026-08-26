using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class AllowedEventTypeSpec
    {
        public AllowedEventTypeSpec() { }

        public AllowedEventTypeSpec(AllowedEventType allowedEventType)
        {
            Id = allowedEventType.Id;
            Active = allowedEventType.Active;
            FacilityTypeId = allowedEventType.FacilityType.Id;
            FacilityTypeName = allowedEventType.FacilityType.Name;
            FacilityTypeActive = allowedEventType.FacilityType.Active;
            EventTypeId = allowedEventType.EventType.Id;
            EventTypeName = allowedEventType.EventType.Name;
            EventTypeActive = allowedEventType.EventType.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityTypeId { get; set; }

        public string FacilityTypeName { get; set; }

        public bool FacilityTypeActive { get; set; } = false;

        public Guid EventTypeId { get; set; }

        public string EventTypeName { get; set; }

        public bool EventTypeActive { get; set; } = false;
    }
}
