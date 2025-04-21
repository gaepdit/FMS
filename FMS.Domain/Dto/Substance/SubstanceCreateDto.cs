using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class SubstanceCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        public Guid ChemicalId { get; set; }

        public bool Groundwater { get; set; }

        public bool Soil { get; set; }

        [Display(Name = "Use for Scoring")]
        public bool UseForScoring { get; set; }
    }
}
