using System;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventTypeEditDto
    {
        public EventTypeEditDto() { }

        public EventTypeEditDto(EventType eventType)
        {
            Id = eventType.Id;
            Name = eventType.Name;
            Active = eventType.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Event Type")]
        [Required(ErrorMessage = "Event Type is required.")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
