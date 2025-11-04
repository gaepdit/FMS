using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationEditDto
    {
        public LocationEditDto() { }

        public LocationEditDto(Guid facilityId)
        {
            Id = Id = Guid.NewGuid();
            FacilityId = facilityId;
        }

        public LocationEditDto(Location location)
        {
            Id = location.Id;
            Active = location.Active;
            FacilityId = location.FacilityId;
            Name = location.Name;
            Description = location.Description;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Facility is required.")]
        public Guid FacilityId { get; set; }

        [Display(Name = "GA EPD DIRECTOR’S DETERMINATION REGARDING CORRECTIVE ACTION: ")]
        public string Description { get; set; }

        [Display(Name = "Class")]   
        [Required(ErrorMessage = "Class is required.")]
        public string Name { get; set; }
    }
}
