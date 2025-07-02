using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class SoilStatusCreateDto
    {
        [Display(Name = "Soil Status")]
        [Required(ErrorMessage = "Soil Status Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
