using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ActionTakenCreateDto
    {
        [Display(Name = "Action Taken")]
        [Required(ErrorMessage = "Action Taken Name is required.")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
