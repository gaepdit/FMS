using System;
using FMS.Domain.Entities;

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

        public string Name { get; set; }

        public string Description { get; set; }

        //public string DisplayName => $"{Name} ({Description})";
    }
}
