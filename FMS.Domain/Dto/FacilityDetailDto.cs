using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityDetailDto
    {
        public FacilityDetailDto(Facility facility)
        {
            Id = facility.Id;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            County = facility.County;
            FacilityStatus = facility.FacilityStatus;
        }          

        public Guid Id;

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;
        
        [Display(Name = "County")]
        public County County { get; set; }

        [Display(Name="Facility Status")]
        public FacilityStatus FacilityStatus { get; set; }
    }
}