using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityTypeCreateDto
    {
        [Required]
        [StringLength(20)]
        [RegularExpression(FacilityType.FacilityTypeNamePattern, ErrorMessage = "Only letters and numbers allowed.")]
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