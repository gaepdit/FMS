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
            File = facility.File;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            County = facility.County;
            FacilityStatus = facility.FacilityStatus;
            FacilityType = facility.FacilityType;
            BudgetCode = facility.BudgetCode;
            OrganizationalUnit = facility.OrganizationalUnit;
            EnvironmentalInterest = facility.EnvironmentalInterest;
            ComplianceOfficer = facility.ComplianceOfficer;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
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

        [Display(Name = "Facility Type")]
        public FacilityType FacilityType { get; set; }

        [Display(Name = "Budget Code")]
        public BudgetCode BudgetCode { get; set; }

        [Display(Name = "Organizational Unit")]
        public OrganizationalUnit OrganizationalUnit { get; set; }

        [Display(Name = "Environmental Interest")]
        public EnvironmentalInterest EnvironmentalInterest { get; set; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; set; }

        [Display(Name = "File Label")]
        public File File { get; set; }

        [Display(Name = "Location Description")]
        public string Location { get; set; }

        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [Display(Name ="Latitude")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitude")]
        public decimal Longitude { get; set; }
    }
}