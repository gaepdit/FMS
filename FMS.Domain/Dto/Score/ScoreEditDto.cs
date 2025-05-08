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
            FacilityId = score.FacilityId;
            ScoredDate = score.ScoredDate;
            ScoredById = score.ScoredById;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }

        public Guid Id { get; set; }

        [Required]
        public Guid FacilityId { get; set; }

        public DateOnly ScoredDate { get; set; }

        public Guid ScoredById { get; set; }
        public string ScoredBy { get; set; }

        public string Comments { get; set; }

        public bool UseComments { get; set; }
    }
}
