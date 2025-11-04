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
            Description = location.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "GA EPD DIRECTOR’S DETERMINATION REGARDING CORRECTIVE ACTION: ")]
        public string Description { get; set; }

        [Display(Name = "Class")]
        public string Name { get; set; }
    }
}
