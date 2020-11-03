using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityStatusEditDto
    {
        public FacilityStatusEditDto()
        {
            // Required for EditFacilityStatus page
        }

        public FacilityStatusEditDto(FacilityStatus facilityStatus)
        {
            Active = facilityStatus.Active;
            Status = facilityStatus.Status;
        }

        public bool Active { get; set; }

        [Required]
        [Display(Name = "Facility Status")]
        public string Status { get; set; }

        public void TrimAll()
        {
            Status = Status?.Trim();
        }
    }
}