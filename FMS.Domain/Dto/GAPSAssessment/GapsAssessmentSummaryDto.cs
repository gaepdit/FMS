using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
