using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ContactTypeCreateDto
    {
        [Required(ErrorMessage = "Contact Title is required.")]
        [Display(Name = "Contact Type")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
