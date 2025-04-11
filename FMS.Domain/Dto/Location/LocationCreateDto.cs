using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Required]
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Score")]
        public string Score { get; set; }

        public void TrimAll()
        {
            Score = Score?.Trim();
        }
    }
}
