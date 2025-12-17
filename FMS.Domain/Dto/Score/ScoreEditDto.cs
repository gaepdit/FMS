using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ScoreEditDto
    {
        public ScoreEditDto() { }

        public ScoreEditDto(Score score)
        {
            Id = score.Id;
            Active = score.Active;
            FacilityId = score.FacilityId;
            ScoredDate = score.ScoredDate;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Scored Date")]
        public DateOnly? ScoredDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Use language from comments field in Site Summary")]
        public bool UseComments { get; set; }
    }
}
