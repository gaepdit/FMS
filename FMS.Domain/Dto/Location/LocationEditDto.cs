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
            Id = location.Id;
            Active = location.Active;
            FacilityId = location.FacilityId;
            LocationClassId = location.LocationClass?.Id;
            MapType = location.MapType;
            MapZoom = location.MapZoom;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        [Required(ErrorMessage = "Class is required.")]
        public Guid? LocationClassId { get; set; }

        [Display(Name = "Map Type")]
        public string MapType { get; set; }

        [Display(Name = "Map Zoom")]
        public string MapZoom { get; set; }
    }
}
