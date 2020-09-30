using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetEditDto
    {
        public CabinetEditDto() { }
        public CabinetEditDto(CabinetSummaryDto cabinet)
        {
            Delete = !cabinet.Active;
            Name = cabinet.Name;
        }

        public bool Delete { get; set; }

        [Required]
        [StringLength(5)]
        [Display(Name = "Cabinet Number")]
        public string Name { get; set; }
    }
}
