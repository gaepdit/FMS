using System;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactTitleSummaryDto
    {
        public ContactTitleSummaryDto() { }

        public ContactTitleSummaryDto(ContactTitle contactTitle)
        {
            Id = contactTitle.Id;
            Active = contactTitle.Active;
            Name = contactTitle.Name;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Title")]
        public string Name { get; set; }
    }
}
