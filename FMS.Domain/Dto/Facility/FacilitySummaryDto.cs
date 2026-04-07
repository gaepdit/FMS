using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class FacilitySummaryDto
    {
        public FacilitySummaryDto(Facility facility)
        {
            Id = facility.Id;
            FileLabel = facility.File == null ? string.Empty : facility.File.FileLabel; 
            FacilityNumber = facility.FacilityNumber;
            FacilityType = facility.FacilityType;
            Name = facility.Name;
            Active = facility.Active;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            IsRetained = facility.IsRetained;
            HasERecord = facility.HasERecord;
            LocationClassId = facility.LocationDetails?.LocationClassId;
            AdditionalOrgUnitId = facility.HsrpFacilityProperties?.OrganizationalUnitId;
            UEC = facility.StatusDetails?.UEC?? false;
            Liens = facility.StatusDetails?.Lien ?? false;
            FinancialAssurance = facility.StatusDetails?.FinancialAssurance ?? false;
            Landfills = facility.StatusDetails?.LandFill ?? false;
            ISWQS = facility.StatusDetails?.ISWQS ?? false;
            Cabinets = new List<string>();
            RetentionRecords = facility.RetentionRecords?
                    .Select(e => new RetentionRecordSummaryDto(e)).ToList()
                ?? new List<RetentionRecordSummaryDto>();
        }

        public Guid Id { get; }

        [Display(Name = "File")]
        public string FileLabel { get; }

        [Display(Name = "Facility Number")]
        public string FacilityNumber { get; }

        [Display(Name = "Type/Env. Interest")]
        public FacilityType FacilityType { get; }

        [Display(Name = "Facility Name")]
        public string Name { get; }

        [Display(Name = "Active Site")]
        public bool Active { get; }

        [Display(Name = "Street Address")]
        public string Address { get; }

        [Display(Name = "City")]
        public string City { get; }

        [Display(Name = "State")]
        public string State { get; }

        [Display(Name = "ZIP Code")]
        public string PostalCode { get; }

        [Display(Name = "Is Retained Onsite")]
        public bool IsRetained { get; }

        //Advanced Properties for search
        [Display(Name = "Class")]
        public Guid? LocationClassId { get; set; }

        [Display(Name = "Add'l Org. Unit")]
        public Guid? AdditionalOrgUnitId { get; set; }

        [Display(Name = "UEC")]
        public bool UEC { get; set; }

        [Display(Name = "Liens")]
        public bool Liens { get; set; }

        [Display(Name = "Financial Assurance")]
        public bool FinancialAssurance { get; set; }

        [Display(Name = "Landfills")]
        public bool Landfills { get; set; }

        [Display(Name = "ISWQS")]
        public bool ISWQS { get; set; }

        // Defines if electronic records are availble on Sharepoint
        [Display(Name = "Has E-Records")]
        public bool HasERecord { get; }

        [Display(Name = "Cabinets")]
        public List<string> Cabinets { get; set; }

        [Display(Name = "Retention Records")]
        public List<RetentionRecordSummaryDto> RetentionRecords { get; }
    }
}