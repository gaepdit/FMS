using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationEditDto
    {
        public LocationEditDto() { }

        public LocationEditDto(Location location)
        {
            FacilityId = location.FacilityId;
            Score = location.Score;
        }
        public Guid FacilityId { get; set; }

        [Display(Name = "Score")]
        public string Score { get; set; }
    }
}
