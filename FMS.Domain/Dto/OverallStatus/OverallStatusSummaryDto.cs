using System;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OverallStatusSummaryDto
    {
        public OverallStatusSummaryDto() { }

        public OverallStatusSummaryDto(OverallStatus overallStatus)
        {
            Id = overallStatus.Id;
            Active = overallStatus.Active;
            Name = overallStatus.Name;
            Description = overallStatus.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Overall Status")]
        public string Name { get; set; }

        public string Description { get; set; }

        public string DisplayName => $"{Name} ({Description})";
    }
}
