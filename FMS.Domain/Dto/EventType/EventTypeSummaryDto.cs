using System;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventTypeSummaryDto
    {
        public EventTypeSummaryDto(EventType eventType)
        {
            Id = eventType.Id;
            Name = eventType.Name;
            Active = eventType.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Event Type")]
        public string Name { get; set; }
    }
}
