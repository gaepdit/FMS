using System;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Score : BaseActiveModel
    {
        public Score() { }
        public Score(ScoreCreateDto scd)
        {
            FacilityId = scd.FacilityId;
            Rank = scd.Rank;
            ScoredDate = scd.ScoredDate;
            ScoredBy = scd.ScoredBy;
            Comments = scd.Comments;
        }
        public Guid FacilityId { get; set; }
        public int Rank { get; set; }
        public DateOnly ScoredDate { get; set; }
        public string ScoredBy { get; set; }
        public string Comments { get; set; }      
    }
}
