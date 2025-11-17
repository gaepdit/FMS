using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ScoreSummaryDto
    {
        public ScoreSummaryDto(Score score)
        {
            Id = score.Id;
            FacilityId = score.FacilityId;
            ScoredDate = score.ScoredDate;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Scored Date")]
        public DateOnly? ScoredDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Use alternate language from comments field")]
        public bool UseComments { get; set; }
    }
}
