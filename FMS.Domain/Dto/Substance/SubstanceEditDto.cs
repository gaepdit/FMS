using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SubstanceEditDto
    {
        public SubstanceEditDto() { }

        public SubstanceEditDto(Substance substance)
        {
            Id = substance.Id;
            FacilityId = substance.FacilityId;
            ChemicalId = substance.Chemical.Id;
            Groundwater = substance.Groundwater;
            Soil = substance.Soil;
            UseForScoring = substance.UseForScoring;
        }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        public Guid ChemicalId { get; set; }

        public bool Groundwater { get; set; }

        public bool Soil { get; set; }

        [Display(Name = "Use for Scoring")]
        public bool UseForScoring { get; set; }
    }
}
