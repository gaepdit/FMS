using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GroundwaterStatusCreateDto
    {
        [Display(Name = "Groundwater Status")]
        [Required(ErrorMessage = "Groundwater Status Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
