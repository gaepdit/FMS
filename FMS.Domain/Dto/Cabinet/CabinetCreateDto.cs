using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetCreateDto
    {
        [Required]
        [StringLength(5)]
        [Display(Name = "Cabinet Number")]
        public string Name { get; set; }
    }
}
