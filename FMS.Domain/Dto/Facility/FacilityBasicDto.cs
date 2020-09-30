using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityBasicDto
    {
        public FacilityBasicDto(Facility facility)
        {
            Id = facility.Id;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
        }

        public Guid Id { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }
    }
}