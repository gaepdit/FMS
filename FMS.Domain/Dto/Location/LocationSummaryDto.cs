using FMS.Domain.Entities;
using System.ComponentModel.DataAnnotations;

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
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }
    }
}
