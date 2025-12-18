using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GapsAssessmentCreateDto
    {
        [Required(ErrorMessage = "Gaps Assessment Name is required.")]
        [Display(Name = "Gaps Assessment")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
