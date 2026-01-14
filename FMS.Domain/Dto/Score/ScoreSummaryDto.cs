using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "Use language from comments field in Site Summary")]
        public bool UseComments { get; set; }
    }
}
