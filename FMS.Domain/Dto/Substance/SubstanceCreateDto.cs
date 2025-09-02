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

        public bool Groundwater { get; set; }

        public bool Soil { get; set; }

        [Display(Name = "Use for Scoring")]
        public bool UseForScoring { get; set; }
    }
}
