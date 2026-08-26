using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class AllowedEventType : BaseActiveModel
    {
        public AllowedEventType() { }

        public AllowedEventType(AllowedEventType allowedEventType)
        {
            Id = Guid.NewGuid();
            FacilityTypeId = allowedEventType.FacilityTypeId;
            FacilityType = allowedEventType.FacilityType;
            EventTypeId = allowedEventType.EventTypeId;
            EventType = allowedEventType.EventType;
            Active = allowedEventType.Active;
        }

        public Guid FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }

        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; }
    }
}
