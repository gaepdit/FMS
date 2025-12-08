using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Required]
        public Guid FacilityId { get; set; }

        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }
        public LocationClass LocationClass { get; set; }
    }
}
