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
            UseForScoring = newSubstance.UseForScoring;
        }
        public Guid FacilityId { get; set; }

        public Guid ChemicalId { get; set; }
        public Chemical Chemical { get; set; }

        public bool Groundwater { get; set; } = false;

        public bool Soil { get; set; } = false;

        public bool UseForScoring { get; set; } = false;

        public string Name => Chemical?.Name ?? string.Empty;
    }
}
