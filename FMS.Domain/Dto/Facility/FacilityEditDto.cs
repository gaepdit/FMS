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
            FileLabel = facility.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            CountyId = facility.County.Id;
            FacilityStatusId = facility.FacilityStatus.Id;
            FacilityTypeId = facility.FacilityType.Id;
            BudgetCodeId = facility.BudgetCode.Id;
            OrganizationalUnitId = facility.OrganizationalUnit.Id;
            EnvironmentalInterestId = facility.EnvironmentalInterest?.Id;
            ComplianceOfficerId = facility.ComplianceOfficer.Id;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
        }

        [Required]
        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; set; }

        [Required]
        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; set; } = true;

        [Required]
        [Display(Name = "County")]
        public int CountyId { get; set; }

        [Required]
        [Display(Name = "Facility Status")]
        public Guid FacilityStatusId { get; set; }

        [Required]
        [Display(Name = "Facility Type")]
        public Guid FacilityTypeId { get; set; }

        [Required]
        [Display(Name = "Budget Code")]
        public Guid BudgetCodeId { get; set; }

        [Required]
        [Display(Name = "Organizational Unit")]
        public Guid OrganizationalUnitId { get; set; }

        [Display(Name = "Environmental Interest")]
        public Guid? EnvironmentalInterestId { get; set; }

        [Required]
        [Display(Name = "Compliance Officer")]
        public Guid ComplianceOfficerId { get; set; }

        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        [Display(Name = "Location Description")]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Street Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; set; }

        [Required]
        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; set; }
    }
}