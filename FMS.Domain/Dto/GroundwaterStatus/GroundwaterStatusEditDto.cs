using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class GroundwaterStatusEditDto
    {
        public GroundwaterStatusEditDto() { }

        public GroundwaterStatusEditDto(GroundwaterStatus groundwaterStatus)
        {
            Id = groundwaterStatus.Id;
            Active = groundwaterStatus.Active;
            Name = groundwaterStatus.Name;
            Description = groundwaterStatus.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Groundwater Status")]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        [Required(ErrorMessage = "Groundwater Status Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }

        public string DisplayName => $"{Name} ({Description})";
    }
}
