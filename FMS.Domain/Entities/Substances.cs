using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Substances : BaseActiveModel, INamedModel
    {
        public Substances() { }
        public Substances(SubstancesCreateDto newSubstance)
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

        public bool Groundwater { get; set; }

        public bool Soil { get; set; }

        public bool UseForScoring { get; set; } = false;

        public string Name => Chemical?.Name ?? string.Empty;
    }
}
