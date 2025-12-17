using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventTypeCreateDto
    {
        [Display(Name = "Event Type")]
        [Required(ErrorMessage = "Event Type Name is required.")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
