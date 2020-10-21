using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityTypeCreateDto
    {
        public bool Active { get; set; }

        // Existing numeric code
        public int Code { get; set; }

        [StringLength(20)]
        [Display(Name = "Facility Type")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
