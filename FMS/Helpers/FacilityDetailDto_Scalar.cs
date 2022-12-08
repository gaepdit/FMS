using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FMS.Domain.Entities;
using FMS.Domain.Extensions;

namespace FMS.Domain.Dto
{
    public class FacilityDetailDto_Scalar
    {
        public FacilityDetailDto_Scalar(FacilityDetailDto facility)
        {
            FacilityGuid = facility.Id.ToString();
            FileLabel = facility.FileLabel;
            FileGuid = facility.Id.ToString();
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            County = facility.County.ToString();
            FacilityStatus = facility.FacilityStatus.ToString();
            FacilityType = facility.FacilityType.ToString();
            BudgetCode = facility.BudgetCode.ToString();
            OrganizationalUnit = facility.OrganizationalUnit.ToString();
            ComplianceOfficer = facility.ComplianceOfficer.ToString();
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            IsRetained = facility.IsRetained;
            Cabinets = facility.CabinetsToString;
            RetentionRecords = facility.RetentionRecordsToString;
        }
        [Display(Name = "Facility Guid")]
        public String FacilityGuid { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }

        [Display(Name = "Active Site")]
        public bool Active { get; }

        [Display(Name = "County")]
        public string County { get; }

        [Display(Name = "Facility Status")]
        public string FacilityStatus { get; }

        [Display(Name = "Type/Environmental Interest")]
        public string FacilityType { get; }

        [Display(Name = "Budget Code")]
        public string BudgetCode { get; }

        [Display(Name = "Organizational Unit")]
        public string OrganizationalUnit { get; }

        [Display(Name = "Compliance Officer")]
        public string ComplianceOfficer { get; }

        [Display(Name = "File Label")]
        public string FileLabel { get; }

        [Display(Name = "File Guid")]
        public string FileGuid { get; }

        [Display(Name = "Location Description")]
        public string Location { get; }

        [Display(Name = "Street Address")]
        public string Address { get; }

        [Display(Name = "City")]
        public string City { get; }

        [Display(Name = "State")]
        public string State { get; }

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
        public string Cabinets { get; }

        [Display(Name = "Retention Records")]
        public string RetentionRecords { get; }
    }
}