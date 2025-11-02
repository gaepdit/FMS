using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AbandonedInactiveCreateDto
    {
        [Display(Name = "Pertinent Information for Aban/Inac Sites")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
