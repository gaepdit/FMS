using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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
            Chemical = substance.Chemical;
            Groundwater = substance.Groundwater;
            Soil = substance.Soil;
            UseForGroundwaterScoring = substance.UseForGroundwaterScoring;
            UseForSoilScoring = substance.UseForSoilScoring;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Chemical")]
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
