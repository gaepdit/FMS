using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ActionTakenCreateDto
    {
        [Display(Name = "Document Type")]
        [Required(ErrorMessage = "Document Type Name is required.")]
        public string name { get; set; }
    }
}
