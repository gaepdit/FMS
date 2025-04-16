using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class DocumentTypeCreateDto
    {
        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "Document Type Name is required.")]
        public string Name { get; set; }
    }
}
