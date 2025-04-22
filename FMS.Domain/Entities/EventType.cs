using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class EventType : BaseActiveModel
    {
        public EventType() { }

        public EventType(EventTypeCreateDto eventType)
        {
            Name = eventType.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
