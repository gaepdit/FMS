using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
