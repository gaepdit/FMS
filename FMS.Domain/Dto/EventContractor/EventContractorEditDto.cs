using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventContractorEditDto
    {
        public EventContractorEditDto() { }

        public EventContractorEditDto(EventContractor contractor)
        {
            Active = contractor.Active;
            Name = contractor.Name;
            Description = contractor.Description;
        }

        public bool Active { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
