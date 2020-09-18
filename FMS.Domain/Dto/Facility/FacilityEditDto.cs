using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    // TODO #56: reconsider mandatory fields
    public class FacilityEditDto
    {
        public FacilityEditDto() { }
        public FacilityEditDto(FacilityDetailDto facility)
        {
            FileId = facility.FileId;
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

        [Display(Name = "Facility Status"), Required]
        public Guid FacilityStatusId { get; set; }

        [Display(Name = "Facility Type"), Required]
        public Guid FacilityTypeId { get; set; }

        [Display(Name = "Budget Code"), Required]
        public Guid BudgetCodeId { get; set; }

        [Display(Name = "Organizational Unit"), Required]
        public Guid OrganizationalUnitId { get; set; }

        [Display(Name = "Environmental Interest")]
        public Guid EnvironmentalInterestId { get; set; }

        [Display(Name = "Compliance Officer"), Required]
        public Guid ComplianceOfficerId { get; set; }

        [Display(Name = "File Label"), Required]
        public Guid? FileId { get; set; }

        [Display(Name = "Location Description")]
        public string Location { get; set; }

        [Display(Name = "Street Address"), Required]
        public string Address { get; set; }

        [Display(Name = "City"), Required]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Zip Code"), Required]
        public string PostalCode { get; set; }

        [Display(Name = "Latitude"), Required]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitude"), Required]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; set; }
    }
}