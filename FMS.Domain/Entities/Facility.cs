using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Facility : BaseActiveModel
    {
        public Facility() { }

        public Facility(FacilityCreateDto newFacility)
        {
            FacilityNumber = newFacility.FacilityNumber;
            Name = newFacility.Name;
            CountyId = newFacility.CountyId;
            FacilityStatusId = newFacility.FacilityStatusId;
            FacilityTypeId = newFacility.FacilityTypeId;
            BudgetCodeId = newFacility.BudgetCodeId;
            OrganizationalUnitId = newFacility.OrganizationalUnitId;
            ComplianceOfficerId = newFacility.ComplianceOfficerId;
            Location = newFacility.Location;
            Address = newFacility.Address;
            City = newFacility.City;
            State = newFacility.State;
            PostalCode = newFacility.PostalCode;
            Latitude = newFacility.Latitude ?? 0;
            Longitude = newFacility.Longitude ?? 0;
            // The following properties only apply to Release Notifications
            HSInumber = newFacility.HSInumber;
            NonHSILetterDate = newFacility.NonHSILetterDate;
            Comments = newFacility.Comments;
            PreRQSMcleanup = newFacility.PreRQSMcleanup;
            ImageChecked = newFacility.ImageChecked;
            DeferredOnSiteScoring = newFacility.DeferredOnSiteScoring;
            AdditionalDataRequested = newFacility.AdditionalDataRequested;
            VRPReferral = newFacility.VRPReferral;
            HasERecord = newFacility.HasERecord;
        }

        // Existing ID for Facility May be used by Programs - System Generated, but not a Guid
        public string FacilityNumber { get; set; }

        // File label and Cabinet where this Facility is located
        public Guid FileId { get; set; }
        public File File { get; set; }

        // Type of Facility
        public Guid? FacilityTypeId { get; set; }
        public FacilityType FacilityType { get; set; }

        // Unit overseeing this Facility
        public Guid? OrganizationalUnitId { get; set; }
        public OrganizationalUnit OrganizationalUnit { get; set; }

        // Program Budget for this Facility
        public Guid? BudgetCodeId { get; set; }
        public BudgetCode BudgetCode { get; set; }

        // Facility Name
        public string Name { get; set; }

        // Compliance Officer assigned to this Facility
        public Guid? ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        public Guid? FacilityStatusId { get; set; }
        public FacilityStatus FacilityStatus { get; set; }

        // Location description distinct from mailing address
        public string Location { get; set; }

        // Facility Address
        [StringLength(100)]
        public string Address { get; set; }

        [StringLength(30)]
        public string City { get; set; }

        [StringLength(20)]
        public string State { get; set; }

        [StringLength(10)]
        public string PostalCode { get; set; }

        // site Coordinates
        [Column(TypeName = "decimal(8, 6)")]
        public decimal Latitude { get; set; }

        [Column(TypeName = "decimal(9, 6)")]
        public decimal Longitude { get; set; }

        public int CountyId { get; set; }

        public County County { get; set; }

        public bool IsRetained { get; set; } = true;

        // The following properties only apply to Release Notifications
        [StringLength(5)]
        public string HSInumber { get; set; }

        [AllowNull]
        public DateOnly NonHSILetterDate { get; set; }

        [StringLength(200)]
        public string Comments { get; set; }

        public bool PreRQSMcleanup { get; set; } = true;

        public bool ImageChecked { get; set; } = true;

        public bool DeferredOnSiteScoring { get; set; } = true;

        public bool AdditionalDataRequested { get; set; } = true;

        public bool VRPReferral { get; set; } = true;

        public bool HasERecord { get; set; } = true;

        [AllowNull]
        public DateOnly RNDateReceived { get; set; }

        public string HistoricalUnit { get; set; }

        public string HistoricalComplianceOfficer { get; set; }

        public string TaxId { get; set; }

        // List of retention records for this Facility
        public ICollection<RetentionRecord> RetentionRecords { get; set; }
    }
}