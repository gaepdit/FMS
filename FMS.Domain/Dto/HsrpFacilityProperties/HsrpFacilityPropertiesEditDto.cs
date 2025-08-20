using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesEditDto
    {
        public HsrpFacilityPropertiesEditDto() { }

        public HsrpFacilityPropertiesEditDto(HsrpFacilityProperties hsrpFacilityProperties)
        {
            if (hsrpFacilityProperties == null)
            {
                throw new ArgumentNullException(nameof(hsrpFacilityProperties));
            }
            Id = hsrpFacilityProperties.Id;
            FacilityId = hsrpFacilityProperties.FacilityId;
            DateListed = hsrpFacilityProperties.DateListed;
            AdditionalOrgUnit = hsrpFacilityProperties.AdditionalOrgUnit;
            ComplianceOfficerId = hsrpFacilityProperties.ComplianceOfficer?.Id;
            VRPDate = hsrpFacilityProperties.VRPDate;
            BrownfieldDate = hsrpFacilityProperties.BrownfieldDate;
            DateDeListed = hsrpFacilityProperties.DateDeListed;
        }

        public HsrpFacilityPropertiesEditDto(Guid facilityId, string facilityNumber, HsrpFacilityPropertiesDetailDto Hfpcd)
        {
            FacilityId = facilityId;
            FacilityNumber = facilityNumber;
            DateListed = Hfpcd.DateListed;
            AdditionalOrgUnit = Hfpcd.AdditionalOrgUnit;
            ComplianceOfficerId = Hfpcd.ComplianceOfficer?.Id;
            VRPDate = Hfpcd.VRPDate;
            BrownfieldDate = Hfpcd.BrownfieldDate;
            DateDeListed = Hfpcd.DateDeListed;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        public string FacilityNumber { get; set; }

        [Required]
        [Display(Name = "Date Listed")]
        public DateOnly DateListed { get; set; }

        [Display(Name = "Additional Org Unit")]
        public string AdditionalOrgUnit { get; set; }

        [Display(Name = "Geologist")]
        public Guid? ComplianceOfficerId { get; set; }

        [Display(Name = "VRP Date")]
        public DateOnly? VRPDate { get; set; }

        [Display(Name = "Brownfield Date")]
        public DateOnly? BrownfieldDate { get; set; }

        [Display(Name = "Date De-listed")]
        public DateOnly? DateDeListed { get; set; }
    }
}
