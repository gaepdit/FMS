using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Substance : BaseActiveModel, INamedModel
    {
        public Substance() { }
        public Substance(SubstanceCreateDto newSubstance)
        {
            FacilityId = newSubstance.FacilityId;
            ChemicalId = newSubstance.ChemicalId;
            Groundwater = newSubstance.Groundwater;
            Soil = newSubstance.Soil;
            UseForGroundwaterScoring = newSubstance.UseForGroundwaterScoring;
            UseForSoilScoring = newSubstance.UseForSoilScoring;
        }
        public Guid FacilityId { get; set; }

        public Guid ChemicalId { get; set; }
        public Chemical Chemical { get; set; }

        public bool Groundwater { get; set; } = false;

        public bool Soil { get; set; } = false;

        public bool UseForGroundwaterScoring { get; set; } = false;

        public bool UseForSoilScoring { get; set; } = false;

        public string Name => Chemical?.Name ?? string.Empty;
    }
}
