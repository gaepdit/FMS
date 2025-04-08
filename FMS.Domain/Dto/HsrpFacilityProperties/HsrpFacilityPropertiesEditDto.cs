using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesEditDto
    {
        public HsrpFacilityPropertiesEditDto() { }

        public HsrpFacilityPropertiesEditDto(Guid facilityId, HsrpFacilityPropertiesDetailDto Hfpcd)
        {
            FacilityId = Hfpcd.FacilityId;
            DateListed = (DateOnly)(Hfpcd?.DateListed);
            AdditionalOrgUnit = Hfpcd?.AdditionalOrgUnit;
            Geologist = Hfpcd?.Geologist;
            VRPDate = (DateOnly)(Hfpcd?.VRPDate);
            BrownfieldDate = (DateOnly)(Hfpcd?.BrownfieldDate);
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
        public DateOnly VRPDate { get; set; }

        [Display(Name = "Brownfield Date")]
        public DateOnly BrownfieldDate { get; set; }
    }
}
