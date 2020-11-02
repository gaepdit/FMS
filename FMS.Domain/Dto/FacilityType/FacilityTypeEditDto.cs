using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityTypeEditDto
    {
        public FacilityTypeEditDto()
        {
            // Required for EditFacilityType page
        }

        public FacilityTypeEditDto(FacilityType facilityType)
        {
            Active = facilityType.Active;
            Name = facilityType.Name;
            Description = facilityType.Description;
        }

        public bool Active { get; set; }

        [Required]
        [StringLength(20)]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Only letters and numbers allowed.")]
        [Display(Name = "Code")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}