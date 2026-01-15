using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class SoilStatus : BaseActiveModel, INamedModel
    {
        public SoilStatus() { }

        public SoilStatus(SoilStatusCreateDto soilStatus)
        {
            Name = soilStatus.Name;
            Description = soilStatus.Description;
        }

        [Display(Name = "Soil Status")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        public string DisplayName => $"{Name} ({Description})";
    }
}
