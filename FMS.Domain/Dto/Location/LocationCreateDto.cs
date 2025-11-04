using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        public string Description { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Class is required.")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Description = Description?.Trim();
            Name = Name?.Trim();
        }
    }
}
