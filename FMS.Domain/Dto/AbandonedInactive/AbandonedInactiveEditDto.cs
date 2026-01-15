using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AbandonedInactiveEditDto
    {
        public AbandonedInactiveEditDto()
        {
            // Required for Edit page
        }

        public AbandonedInactiveEditDto(AbandonedInactive abanInac)
        {
            Active = abanInac.Active;
            Name = abanInac.Name;
            Description = abanInac.Description;
        }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Pertinent Information for Aban/Inac Sites")]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
