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
            Name = location.Name;
            Class = location.Class;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        public string Name { get; set; }

        [Display(Name = "Class")]
        public string Class { get; set; }
    }
}
