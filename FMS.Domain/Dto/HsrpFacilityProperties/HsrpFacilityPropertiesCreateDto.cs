using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class HsrpFacilityPropertiesCreateDto
    {
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
