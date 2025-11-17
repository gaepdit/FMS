using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Entities
{
    public class Score : BaseActiveModel
    {
        public Score() { }

        public Score(Guid facilityId)
        {
            Id = Guid.NewGuid();
            FacilityId = facilityId;
        }

        public Score(ScoreCreateDto score)
        {
            FacilityId = score.FacilityId;
            ScoredDate = score.ScoredDate;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }

        public Score(ScoreEditDto score)
        {
            Id = score.Id;
            Active = score.Active;
            FacilityId = score.FacilityId;
            ScoredDate = score.ScoredDate;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }

        public Guid FacilityId { get; set; }

        [Display(Name = "Scored Date")]
        public DateOnly? ScoredDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }   

        [Display(Name = "Use language from comments field in Site Summary")]
        public bool UseComments { get; set; } = false;
    }
}
