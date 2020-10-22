using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetEditDto
    {
        public CabinetEditDto() { }

        public CabinetEditDto(CabinetSummaryDto cabinet)
        {
            FirstFileLabel = cabinet.FirstFileLabel;
        }

        [Required]
        [StringLength(8)]
        [Display(Name = "First File Label")]
        public string FirstFileLabel { get; set; }
    }
}