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
            Class = location.Class;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "Facility is required.")]
        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]   
        [Required(ErrorMessage = "Class is required.")]
        public string Class { get; set; }
    }
}
