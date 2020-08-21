using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityEditDto
    {
        public FacilityEditDto() { }
        public FacilityEditDto(FacilityDetailDto facility)
        {
            FileId = facility.File.Id;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            CountyId = facility.County.Id;
            FacilityStatusId = facility.FacilityStatus.Id;
            FacilityTypeId = facility.FacilityType.Id;
            BudgetCodeId = facility.BudgetCode.Id;
            OrganizationalUnitId = facility.OrganizationalUnit.Id;
            EnvironmentalInterestId = facility.EnvironmentalInterest.Id;
            ComplianceOfficerId = facility.ComplianceOfficer.Id;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
        }

        [Display(Name = "Facility Number"), Required]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name"), Required]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;

        [Display(Name = "County"), Required]
        public int CountyId { get; set; }

        [Display(Name = "Facility Status")]
        public Guid FacilityStatusId { get; set; }

        [Display(Name = "Facility Type")]
        public Guid FacilityTypeId { get; set; }

        [Display(Name = "Budget Code")]
        public Guid BudgetCodeId { get; set; }

        [Display(Name = "Organizational Unit")]
        public Guid OrganizationalUnitId { get; set; }

        [Display(Name = "Environmental Interest")]
        public Guid EnvironmentalInterestId { get; set; }

        [Display(Name = "Compliance Officer")]
        public Guid ComplianceOfficerId { get; set; }

        [Display(Name = "File Label")]
        public Guid FileId { get; set; }

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

        [Display(Name = "Latitude")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        public double Longitude { get; set; }
    }
}