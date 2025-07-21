using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
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
            FileLabel = facility.File?.FileLabel;
            FileId = facility.File?.Id;
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
            HasERecord = facility.HasERecord;
            // *** these properties only apply to Release Notifications ***
            HSInumber = facility.HSInumber;
            DeterminationLetterDate = facility.DeterminationLetterDate;
            PreRQSMcleanup = facility.PreRQSMcleanup;
            ImageChecked = facility.ImageChecked;
            DeferredOnSiteScoring = facility.DeferredOnSiteScoring;
            AdditionalDataRequested = facility.AdditionalDataRequested;
            VRPReferral = facility.VRPReferral;
            RNDateReceived = facility.RNDateReceived;
            HistoricalUnit = facility.HistoricalUnit;
            HistoricalComplianceOfficer = facility.HistoricalComplianceOfficer;
            // ********************************************************
            IsRetained = facility.IsRetained;
            Comments = facility.Comments;
            Cabinets = new List<string>();
            RetentionRecords = facility.RetentionRecords?
                    .Select(e => new RetentionRecordDetailDto(e)).ToList()
                ?? new List<RetentionRecordDetailDto>();
            // Phase III additions for HSI
            HsrpFacilityPropertyDetails = facility.HsrpFacilityProperties;
            LocationDetails = facility.LocationDetails;
            Parcels = facility.Parcels?
                .Select(p => new ParcelSummaryDto(p)).ToList() ?? 
                new List<ParcelSummaryDto>();
            Contacts = facility.Contacts ?
                .Select(c => new ContactSummaryDto(c)).ToList() ?? new List<ContactSummaryDto>();
            ScoreDetails = facility.ScoreDetails;
            GroundwaterScoreDetails = facility.GroundwaterScoreDetails;
            OnsiteScoreDetails = facility.OnsiteScoreDetails;
            Substances = facility.Substances ?
                .Select(s => new SubstanceSummaryDto(s)).ToList() ?? new List<SubstanceSummaryDto>();
            StatusDetails = facility.StatusDetails;
            Events = facility.Events ?
                .Select(e => new EventSummaryDto(e)).ToList() ?? new List<EventSummaryDto>();
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

        [Display(Name = "Type/Env. Interest")]
        public FacilityType FacilityType { get; }

        [Display(Name = "Budget Code")]
        public BudgetCode BudgetCode { get; }

        [Display(Name = "Organizational Unit")]
        public OrganizationalUnit OrganizationalUnit { get; }

        [Display(Name = "Compliance Officer")]
        public ComplianceOfficer ComplianceOfficer { get; }

        [Display(Name = "File Label")]
        public string FileLabel { get; }

        public Guid? FileId { get; }

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

        // The following properties only apply to Release Notifications
        [Display(Name = "HSI Number")]
        public string HSInumber { get; set; }

        [Display(Name = "Determination Letter Date", Prompt = "None Entered")]
        public DateOnly? DeterminationLetterDate { get; set; }

        [Display(Name = "Pre-RQSM Cleanup")]
        public bool PreRQSMcleanup { get; set; }

        [Display(Name = "Image Checked")]
        public bool ImageChecked { get; set; }

        [Display(Name = "Brownfield Deferral")]
        public bool DeferredOnSiteScoring { get; set; }

        [Display(Name = "Additional Data Requested")]
        public bool AdditionalDataRequested { get; set; }

        [Display(Name = "VRP Deferral")]
        public bool VRPReferral { get; set; }

        [Display(Name = "Date Received")]
        public DateOnly? RNDateReceived { get; set; }

        [Display(Name = "Historical Unit")]
        public string HistoricalUnit { get; set; }

        [Display(Name = "Historical C.O.")]
        public string HistoricalComplianceOfficer { get; set; }

        [Display(Name = "Has Electronic Records")]
        public bool HasERecord { get; set; }

        [Display(Name = "Is Retained Onsite")]
        public bool IsRetained { get; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Cabinets")]
        public List<string> Cabinets { get; set; }

        [Display(Name = "Retention Records")]
        public List<RetentionRecordDetailDto> RetentionRecords { get; }

        // Used for CSV file output to CSV Helper
        public string CabinetsToString => IsRetained ? string.Join(", ", Cabinets) : "Not retained";

        public string RetentionRecordsToString => RetentionRecords.Count > 0
            ? RetentionRecords.Select(r => r.Summary).ConcatNonEmpty(Environment.NewLine)
            : "none";

        // Phase III additions for HSI
        public HsrpFacilityProperties HsrpFacilityPropertyDetails { get; set; }

        [Display(Name = "Class")]
        public Location LocationDetails { get; set; }

        public List<ParcelSummaryDto> Parcels { get; }

        public List<ContactSummaryDto> Contacts { get; }

        public Score ScoreDetails { get; set; }

        public GroundwaterScore GroundwaterScoreDetails { get; set; }

        public OnsiteScore OnsiteScoreDetails { get; set; }

        public List<SubstanceSummaryDto> Substances { get; }

        public Status StatusDetails { get; set; }

        public List<EventSummaryDto> Events { get; }
    }
}