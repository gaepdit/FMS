using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Location : BaseActiveModel
    {
        public Location() { }

        public Location(Guid facilityId)
        {
            Id = Guid.NewGuid();
            FacilityId = facilityId;
        }

        public Location(Guid id, LocationCreateDto location)
        {
            Id = id;
            FacilityId = location.FacilityId;
            LocationClassId = location.LocationClassId;
            MapType = location.MapType;
            MapZoom = location.MapZoom;
        }

        public Guid FacilityId { get; set; }
        public Guid? LocationClassId { get; set; }
        public LocationClass LocationClass { get; set; }

        [Display(Name = "Map Type")]
        public string MapType { get; set; }

        [Display(Name = "Map Zoom")]
        public string MapZoom { get; set; }
    }
}
