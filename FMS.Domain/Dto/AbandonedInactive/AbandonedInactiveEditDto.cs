using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

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
        }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [Display(Name = "Pertinent Information for Aban/Inac Sites")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
