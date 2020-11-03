using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusCreateDto
    {
        [Required]
        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        public void TrimAll()
        {
            Status = Status?.Trim();
        }
    }
}