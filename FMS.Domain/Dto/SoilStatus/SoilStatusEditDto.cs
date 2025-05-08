using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SoilStatusEditDto
    {
        public SoilStatusEditDto() { }

        public SoilStatusEditDto(SoilStatus soilStatus)
        {
            Id = soilStatus.Id;
            Name = soilStatus.Name;
        }

        public Guid Id { get; set; }

        [Display(Name = "Soil Status")]
        [Required(ErrorMessage = "Soil Status Name is required.")]
        public string Name { get; set; }
    }
}
