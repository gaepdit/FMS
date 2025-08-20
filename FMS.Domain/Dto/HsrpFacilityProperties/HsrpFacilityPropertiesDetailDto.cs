using FMS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesDetailDto
    {
        public HsrpFacilityPropertiesDetailDto(HsrpFacilityProperties hsrpFacilityProperties)
        {
            Id = hsrpFacilityProperties.Id;
            FacilityId = hsrpFacilityProperties.FacilityId;
            DateListed = hsrpFacilityProperties.DateListed;
            OrganizationalUnit = hsrpFacilityProperties.OrganizationalUnit;
            ComplianceOfficer = hsrpFacilityProperties.ComplianceOfficer;
            VRPDate = hsrpFacilityProperties.VRPDate;
            BrownfieldDate = hsrpFacilityProperties.BrownfieldDate;
        }
        [Required]
        public Guid Id { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public Guid FacilityId { get; set; }

        [Display(Name = "Date Listed")]
        public DateOnly DateListed { get; set; }

        [Display(Name = "Additional Org Unit")]
        public OrganizationalUnit OrganizationalUnit { get; set; }

        [Display(Name = "Geologist")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "VRP Date")]
        public DateOnly? VRPDate { get; set; }

        [Display(Name = "Brownfield Date")]
        public DateOnly? BrownfieldDate { get; set; }

        [Display(Name = "Date De-listed")]
        public DateOnly? DateDeListed { get; set; }
    }
}
