using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class SoilStatusSummaryDto
    {
        public SoilStatusSummaryDto() { }
        public SoilStatusSummaryDto(SoilStatus soilStatus)
        {
            Id = soilStatus.Id;
            Name = soilStatus.Name;
            Description = soilStatus.Description;
            Active = soilStatus.Active;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Soil Status")]
        public string Name { get; set; }

        public string Description { get; set; }

        //public string DisplayName => $"{Name} ({Description})";
    }
}
