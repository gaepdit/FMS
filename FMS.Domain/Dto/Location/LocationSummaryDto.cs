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
            LocationClassId = location.LocationClassId;
            MapType = location.MapType;
            MapZoom = location.MapZoom;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }

        [Display(Name = "Map Type")]
        public string MapType { get; set; } = "hybrid";

        [Display(Name = "Map Zoom")]
        public string MapZoom { get; set; } = "13";
    }
}
