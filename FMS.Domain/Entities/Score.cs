using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Score : BaseActiveModel
    {
        public Score() { }

        public Score(ScoreCreateDto score)
        {
            FacilityId = score.FacilityId;
            ScoredDate = score.ScoredDate;
            ScoredBy = score.ScoredBy;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }
        public Guid FacilityId { get; set; }

        public DateOnly ScoredDate { get; set; }

        public string ScoredBy { get; set; }

        public string Comments { get; set; }   
        
        public bool UseComments { get; set; } = false;
    }
}
