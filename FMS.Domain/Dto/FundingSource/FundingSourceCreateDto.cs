using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FundingSourceCreateDto
    {
        [Display(Name = "Funding Source")]
        [Required(ErrorMessage = "Funding Source Name is required.")]
        public string Name { get; set; }
    }
}
