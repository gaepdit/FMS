using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Class is required.")]
        public string Class { get; set; }

        public void TrimAll()
        {
            Class = Class?.Trim();
            Name = Name?.Trim();
        }
    }
}
