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
            Active = substance.Active;
            FacilityId = substance.FacilityId;
            ChemicalId = substance.Chemical.Id;
            Groundwater = substance.Groundwater;
            Soil = substance.Soil;
            UseForScoring = substance.UseForScoring;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Chemical")]
        public Guid ChemicalId { get; set; }
        public Chemical Chemical { get; set; }

        public bool Groundwater { get; set; }

        public bool Soil { get; set; }

        [Display(Name = "Use for Scoring")]
        public bool UseForScoring { get; set; }
    }
}
