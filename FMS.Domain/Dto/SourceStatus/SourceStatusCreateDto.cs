using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class SourceStatusCreateDto
    {
        [Display(Name = "Source Status")]
        [Required(ErrorMessage = "Source Status Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
    }
}
