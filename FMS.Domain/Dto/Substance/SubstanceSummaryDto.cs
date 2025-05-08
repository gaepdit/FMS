using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SubstanceSummaryDto
    {
        public SubstanceSummaryDto(Substance substance)
        {
            Id = substance.Id;
            FacilityId = substance.FacilityId;
            ChemicalId = substance.ChemicalId;
            Groundwater = substance.Groundwater;
            Soil = substance.Soil;
            UseForScoring = substance.UseForScoring;
        }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        public Guid ChemicalId { get; set; }
        [Display(Name = "Chemical Name")]
        public Chemical Chemical { get; set; }

        public bool Groundwater { get; set; }

        public bool Soil { get; set; }

        [Display(Name = "Use for Scoring")]
        public bool UseForScoring { get; set; }
    }
}
