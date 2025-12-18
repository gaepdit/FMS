using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesEditDto
    {
        public HsrpFacilityPropertiesEditDto() { }

        public HsrpFacilityPropertiesEditDto(Guid facilityId)
        {
            Id = Guid.NewGuid();
            FacilityId = facilityId;
        }

        public HsrpFacilityPropertiesEditDto(HsrpFacilityProperties hsrpFacilityProperties)
        {
            if (hsrpFacilityProperties == null)
            {
                throw new ArgumentNullException(nameof(hsrpFacilityProperties));
            }
            Id = hsrpFacilityProperties.Id;
            FacilityId = hsrpFacilityProperties.FacilityId;
            DateListed = hsrpFacilityProperties.DateListed;
            OrganizationalUnitId = hsrpFacilityProperties.OrganizationalUnit?.Id;
            ComplianceOfficerId = hsrpFacilityProperties.ComplianceOfficer?.Id;
            VRPDate = hsrpFacilityProperties.VRPDate;
            BrownfieldDate = hsrpFacilityProperties.BrownfieldDate;
            DateDeListed = hsrpFacilityProperties.DateDeListed;
            VRPTerminated = hsrpFacilityProperties.VRPTerminated;
            BrownfieldTerminated = hsrpFacilityProperties.BrownfieldTerminated;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        [Required]
        [Display(Name = "Date Listed")]
        public DateOnly? DateListed { get; set; }

        [Display(Name = "Additional Org Unit")]
        public Guid? OrganizationalUnitId { get; set; }

        [Display(Name = "Geologist")]
        public Guid? ComplianceOfficerId { get; set; }

        [Display(Name = "VRP Date")]
        public DateOnly? VRPDate { get; set; }

        [Display(Name = "Brownfield Date")]
        public DateOnly? BrownfieldDate { get; set; }

        [Display(Name = "Date De-listed")]
        public DateOnly? DateDeListed { get; set; }

        [Display(Name = "VRP Terminated")]
        public bool VRPTerminated { get; set; }

        [Display(Name = "Brownfield Terminated")]
        public bool BrownfieldTerminated { get; set; }
        }
}
