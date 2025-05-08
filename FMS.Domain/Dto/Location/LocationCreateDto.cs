using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; }

        public void TrimAll()
        {
            Class = Class?.Trim();
        }
    }
}
