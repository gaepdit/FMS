using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class CabinetEditDto
    {
        public CabinetEditDto() { }

        public CabinetEditDto(CabinetSummaryDto cabinet)
        {
            Name = cabinet.Name;
            FirstFileLabel = cabinet.FirstFileLabel;
        }

        [Required]
        [Display(Name = "Cabinet Number")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "First File Label")]
        [StringLength(8)]
        [RegularExpression(File.FileLabelPattern, ErrorMessage = "The File Label is invalid.")]
        public string FirstFileLabel { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            FirstFileLabel = FirstFileLabel?.Trim();
        }
    }
}