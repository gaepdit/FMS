using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Extensions;

namespace FMS.Domain.Dto
{
    public class FacilityDetailDto
    {
        public FacilityDetailDto(Facility facility)
        {
            Id = facility.Id;
            FileLabel = facility.File.FileLabel;
            FileId = facility.File.Id;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            County = facility.County;
            FacilityStatus = facility.FacilityStatus;
            FacilityType = facility.FacilityType;
            BudgetCode = facility.BudgetCode;
            OrganizationalUnit = facility.OrganizationalUnit;
            ComplianceOfficer = facility.ComplianceOfficer;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            IsRetained = facility.IsRetained;
            Cabinets = new List<string>();
            RetentionRecords = facility.RetentionRecords?
                    .Select(e => new RetentionRecordDetailDto(e)).ToList()
                ?? new List<RetentionRecordDetailDto>();
        }

        public Guid Id { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; set; }

        [Display(Name = "Active Site")]
        public bool Active { get; }

        [Display(Name = "County")]
        public County County { get; set; }

        [Display(Name = "Facility Status")]
        public FacilityStatus FacilityStatus { get; }

        [Display(Name = "Type/Environmental Interest")]
        public FacilityType FacilityType { get; }

        [Display(Name = "Budget Code")]
        public BudgetCode BudgetCode { get; }

        [Display(Name = "Organizational Unit")]
        public OrganizationalUnit OrganizationalUnit { get; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; }

        [Display(Name = "File Label")]
        public string FileLabel { get; }

        public Guid FileId { get; }

        [Display(Name = "Location Description")]
        public string Location { get; }

        [Display(Name = "Street Address")]
        public string Address { get; }

        [Display(Name = "City")]
        public string City { get; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        public string PostalCode { get; }

        [Display(Name = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; }

        [Display(Name = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; }

        [Display(Name = "Is Retained Onsite")]
        public bool IsRetained { get; }

        [Display(Name = "Cabinets")]
        public List<string> Cabinets { get; set; }

        [Display(Name = "Retention Records")]
        public List<RetentionRecordDetailDto> RetentionRecords { get; }

        // Used for CSV file output to CSV Helper
        public string CabinetsToString => IsRetained ? string.Join(", ", Cabinets) : "Not retained";

        public string RetentionRecordsToString => RetentionRecords.Count > 0
            ? RetentionRecords.Select(r => r.Summary).ConcatNonEmpty(Environment.NewLine)
            : "none";
    }
}