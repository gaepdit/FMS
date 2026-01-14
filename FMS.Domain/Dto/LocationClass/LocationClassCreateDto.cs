using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationClassCreateDto
    {
        [Display(Name = "Location Class Name")]
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Display(Name = "Location Class Description")]
        public string Description { get; set; }

        public bool Active { get; set; } = true;

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
