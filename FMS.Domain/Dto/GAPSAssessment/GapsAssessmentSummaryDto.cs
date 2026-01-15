using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class GapsAssessmentSummaryDto
    {
        public GapsAssessmentSummaryDto(GapsAssessment gapsAssessment)
        {
            Id = gapsAssessment.Id;
            Active = gapsAssessment.Active;
            Name = gapsAssessment.Name;
            Description = gapsAssessment.Description;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [Display(Name = "GAPS Assessment Code")]
        public string Name { get; }

        public string Description { get; }
    }
}
