using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class FacilityTypeCreateDto
    {
        [Required]
        [Display(Name = "Code")]
        [StringLength(20)]
        [RegularExpression(FacilityType.FacilityTypeNamePattern, ErrorMessage = "Only letters and numbers allowed.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}