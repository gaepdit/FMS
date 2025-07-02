using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GroundwaterStatusCreateDto
    {
        [Display(Name = "Groundwater Status")]
        [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters.")]
        [Required(ErrorMessage = "Groundwater Status Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
