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
            Code = facilityType.Code;
            Name = facilityType.Name;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        // Existing numeric code
        public int Code { get; set; }

        [Display(Name = "Facility Type")]
        public string Name { get; set; }
    }
}
