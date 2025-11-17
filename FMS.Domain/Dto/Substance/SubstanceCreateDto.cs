using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class SubstanceCreateDto
    {
        public SubstanceCreateDto() { }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        public Guid ChemicalId { get; set; }
        public Chemical Chemical { get; set; }

        [Display(Name = "Present in GW")]
        public bool Groundwater { get; set; }

        [Display(Name = "Present in Soil")]
        public bool Soil { get; set; }

        [Display(Name = "Use for GW Scoring")]
        public bool UseForGroundwaterScoring { get; set; }

        [Display(Name = "Use for Onsite Scoring")]
        public bool UseForSoilScoring { get; set; }
    }
}
