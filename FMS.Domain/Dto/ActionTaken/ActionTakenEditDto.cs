using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ActionTakenEditDto
    {
        public ActionTakenEditDto()
        {
            // Required for EditActionTaken page
        }

        public ActionTakenEditDto(ActionTaken actionTaken)
        {
            Active = actionTaken.Active;
            Name = actionTaken.Name;
        }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Action Taken Name is required.")]
        [Display(Name = "Action Taken")]
        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
