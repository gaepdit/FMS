using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GapsAssessmentEditDto
    {
        public GapsAssessmentEditDto()
        {
            // Required for EditGapsAssessment page
        }

        public GapsAssessmentEditDto(GapsAssessment gapsAssessment)
        {
            Active = gapsAssessment.Active;
            Name = gapsAssessment.Name;
            Description = gapsAssessment.Description;
        }

        public bool Active { get; set; }

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

        public string GetName()
        {
            return $"{Name} - ({Description})";
        }
    }
}
