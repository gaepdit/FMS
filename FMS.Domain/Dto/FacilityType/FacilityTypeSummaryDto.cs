using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityTypeSummaryDto
    {
        public FacilityTypeSummaryDto(FacilityType facilityType)
        {
            Id = facilityType.Id;
            Active = facilityType.Active;
            Name = facilityType.Name;
            Description = facilityType.Description;
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Code")]
        public string Name { get; }

        public string Description { get; }
    }
}