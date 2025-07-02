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
            Active = soilStatus.Active;
            Name = soilStatus.Name;
            Description = soilStatus.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Soil Status")]
        [Required(ErrorMessage = "Soil Status Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
