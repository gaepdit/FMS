using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusCreateDto
    {
        public bool Active { get; set; }

        [Display(Name = "Facility Status")]
        [Required]
        public string Status { get; set; }

        public void TrimAll()
        {
            Status = Status?.Trim();
        }
    }
}
