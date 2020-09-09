using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetEditDto
    {
        public CabinetEditDto() { }
        public CabinetEditDto(Cabinet cabinet)
        {
            Active = cabinet.Active;
            Name = cabinet.Name;
        }

        public bool Active { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "File Cabinet Number")]
        public string Name { get; set; }
    }
}
