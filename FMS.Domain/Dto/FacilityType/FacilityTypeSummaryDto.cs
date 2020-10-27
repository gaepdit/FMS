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
        }

        public Guid Id { get; }

        public bool Active { get; }

        [Display(Name = "Facility Type")]
        public string Name { get; }
    }
}
