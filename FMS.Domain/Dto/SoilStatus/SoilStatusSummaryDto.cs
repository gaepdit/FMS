using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SoilStatusSummaryDto
    {
        public SoilStatusSummaryDto() { }
        public SoilStatusSummaryDto(SoilStatus soilStatus)
        {
            Id = soilStatus.Id;
            Name = soilStatus.Name;
            Active = soilStatus.Active;
        }
        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Soil Status")]
        public string Name { get; set; }
    }
}
