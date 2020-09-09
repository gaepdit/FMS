using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetCreateDto
    {
        // File Cabinet Number
        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }
    }
}
