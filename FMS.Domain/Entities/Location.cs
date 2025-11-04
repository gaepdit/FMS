using System;
using System.ComponentModel.DataAnnotations;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Location : BaseActiveModel, INamedModel
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
            Name = location.Name;
            Description = location.Description;
        }

        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        public string Name { get; set; }

        [Display(Name = "GA EPD DIRECTOR’S DETERMINATION REGARDING CORRECTIVE ACTION: ")]
        public string Description { get; set; }
    }
}
