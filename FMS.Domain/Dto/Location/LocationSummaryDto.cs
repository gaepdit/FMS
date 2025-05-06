using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class LocationSummaryDto
    {
        public LocationSummaryDto() { }

        public LocationSummaryDto(Location location)
        {
            Id = location.Id;
            Active = location.Active;
            FacilityId = location.FacilityId;
            Score = location.Score;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Score")]
        public string Score { get; set; }
    }
}
