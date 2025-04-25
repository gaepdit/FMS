using FMS.Domain.Entities.Base;
using FMS.Domain.Dto;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class SoilStatus : BaseActiveModel
    {
        public SoilStatus() { }

        public SoilStatus(SoilStatusCreateDto soilStatus)
        {
            Name = soilStatus.Name;
        }

        [Display(Name = "Soil Status")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
