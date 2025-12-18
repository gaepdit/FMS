using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GroundwaterStatusSummaryDto
    {
        public GroundwaterStatusSummaryDto(GroundwaterStatus groundwaterStatus)
        {
            Id = groundwaterStatus.Id;
            Active = groundwaterStatus.Active;
            Name = groundwaterStatus.Name;
            Description = groundwaterStatus.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Groundwater Status")]
        public string Name { get; set; }
        
        [Display(Name = "Description")]
        public string Description { get; set; }

        public string DisplayName => $"{Name} ({Description})";
    }
}
