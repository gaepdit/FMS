using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class GroundwaterStatus : BaseActiveModel, INamedModel
    {
        public GroundwaterStatus() { }

        public GroundwaterStatus(GroundwaterStatusCreateDto groundwaterStatus)
        {
            Name = groundwaterStatus.Name;
            Description = groundwaterStatus.Description;
        }

        [StringLength(20)]
        public string Name { get; set; }
        public string Description { get; set; }

        public string DisplayName => $"{Name} ({Description})";

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
