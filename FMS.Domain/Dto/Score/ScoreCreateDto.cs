using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ScoreCreateDto
    {
        public Guid FacilityId { get; set; }

        [Display(Name = "Scored Date")]
        public DateOnly? ScoredDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Use language from comments field in Site Summary")]
        public bool UseComments { get; set; } = false;
    }
}
