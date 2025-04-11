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
            Rank = score.Rank;
            ScoredDate = score.ScoredDate;
            ScoredBy = score.ScoredBy;
            Comments = score.Comments;
        }
        public Guid FacilityId { get; set; }
        public int Rank { get; set; }
        public DateOnly ScoredDate { get; set; }
        public string ScoredBy { get; set; }
        public string Comments { get; set; }      
    }
}
