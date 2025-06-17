using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SourceStatusSummaryDto
    {
        public SourceStatusSummaryDto() { }

        public SourceStatusSummaryDto(SourceStatus sourceStatus)
        {
            Id = sourceStatus.Id;
            Name = sourceStatus.Name;
            Description = sourceStatus.Description;
            Active = sourceStatus.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Source Status")]
        [Required(ErrorMessage = "Source Status Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
