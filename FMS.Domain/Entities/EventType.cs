using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class EventType : BaseActiveModel, INamedModel
    {
        public EventType() { }

        public EventType(EventTypeCreateDto eventType)
        {
            Name = eventType.Name;
        }

        public EventType(EventTypeEditDto eventType)
        {
            Id = eventType.Id;
            Name = eventType.Name;
            Active = eventType.Active;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
