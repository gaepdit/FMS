using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ActionTakenCreateDto
    {
        [Display(Name = "Actgion Taken")]
        [Required(ErrorMessage = "Action Taken Name is required.")]
        public string Name { get; set; }
    }
}
