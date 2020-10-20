using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class CabinetEditDto
    {
        public CabinetEditDto() { }

        public CabinetEditDto(CabinetSummaryDto cabinet)
        {
            FirstFileLabel = cabinet.FirstFileLabel;
            Name = cabinet.Name;
            CabinetNumber = cabinet.CabinetNumber;
        }

        [Required]
        [StringLength(9)]
        [Display(Name = "First File Label")]
        public string FirstFileLabel { get; set; }

        public string Name { get; }
        public int CabinetNumber { get; }
    }
}