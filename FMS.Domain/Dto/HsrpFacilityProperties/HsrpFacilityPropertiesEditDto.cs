using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesEditDto
    {
        public HsrpFacilityPropertiesEditDto() { }

        public HsrpFacilityPropertiesEditDto(Guid facilityId, HsrpFacilityPropertiesDetailDto Hfpcd)
        {
            FacilityId = facilityId;
            DateListed = Hfpcd.DateListed;
            AdditionalOrgUnit = Hfpcd.AdditionalOrgUnit;
            Geologist = Hfpcd.Geologist;
            VRPDate = Hfpcd.VRPDate;
            BrownfieldDate = Hfpcd.BrownfieldDate;
            DateDeListed = Hfpcd.DateDeListed;
        }

        [Required]
        public Guid FacilityId { get; set; }

        [Required]
        [Display(Name = "Date Listed")]
        public DateOnly DateListed { get; set; }

        [Display(Name = "Additional Org Unit")]
        public string AdditionalOrgUnit { get; set; }

        [Display(Name = "Geologist")]
        public string Geologist { get; set; }

        [Display(Name = "VRP Date")]
        public DateOnly? VRPDate { get; set; }

        [Display(Name = "Brownfield Date")]
        public DateOnly? BrownfieldDate { get; set; }

        [Display(Name = "Date De-listed")]
        public DateOnly? DateDeListed { get; set; }
    }
}
