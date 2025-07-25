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
            ScoredById = score.ScoredById;
            Comments = score.Comments;
            UseComments = score.UseComments;
        }
        public Guid FacilityId { get; set; }

        [Display(Name = "Scored Date")]
        public DateOnly ScoredDate { get; set; }

        public Guid? ScoredById { get; set; }
        [Display(Name = "Scored By")]
        public ComplianceOfficer ScoredBy { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }   

        [Display(Name = "Use alt language for Site Summary Report")]
        public bool UseComments { get; set; } = false;
    }
}
