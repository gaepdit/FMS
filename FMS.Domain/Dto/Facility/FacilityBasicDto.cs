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
            Active = facility.Active;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            OrganizationalUnit = facility.OrganizationalUnit;
            ComplianceOfficer = facility.ComplianceOfficer;
            County = facility.County;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }

        [Display(Name = "Organizational Unit")]
        public OrganizationalUnit OrganizationalUnit { get; set; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "County")]
        public County County { get; set; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; set; }
    }
}