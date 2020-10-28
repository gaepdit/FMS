using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    /// <summary>
    /// Type/Environmental Interest
    /// </summary>
    public class FacilityType : BaseActiveModel, INamedModel
    {
        public FacilityType() { }

        public FacilityType(FacilityTypeCreateDto newFacilityType)
        {
            Name = newFacilityType.Name;
            Description = newFacilityType.Description;
        }

        [StringLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        public string DisplayName => $"{Name} ({Description})";
    }
}