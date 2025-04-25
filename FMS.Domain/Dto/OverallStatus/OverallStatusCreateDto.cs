using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class OverallStatusCreateDto
    {
        [Display(Name = "Overall Status")]
        [Required(ErrorMessage = "Overall Status Name is required.")]
        public string Name { get; set; }
    }
}
