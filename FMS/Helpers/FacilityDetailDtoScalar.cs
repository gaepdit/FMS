using ClosedXML.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FacilityDetailDtoScalar
    {
        public FacilityDetailDtoScalar(FacilityDetailDto facility)
        {
            FileLabel = facility.FileLabel;
            FacilityNumber = facility.FacilityNumber;
            Name = facility.Name;
            Active = facility.Active;
            County = facility.County.Name;
            FacilityStatus = facility.FacilityStatus?.Name;
            FacilityType = facility.FacilityType?.Name;
            BudgetCode = facility.BudgetCode?.Name;
            OrganizationalUnit = facility.OrganizationalUnit?.Name;
            ComplianceOfficer = facility.ComplianceOfficer?.Name;
            Location = facility.Location;
            Address = facility.Address;
            City = facility.City;
            State = facility.State;
            PostalCode = facility.PostalCode;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            HSInumber = facility.HSInumber;
            DeterminationLetterDate = facility.DeterminationLetterDate;
            Comments = facility.Comments;
            PreRQSMcleanup = facility.PreRQSMcleanup;
            ImageChecked = facility.ImageChecked;
            DeferredOnSiteScoring = facility.DeferredOnSiteScoring;
            AdditionalDataRequested = facility.AdditionalDataRequested;
            VRPReferral = facility.VRPReferral;
            RNDateReceived = facility.RNDateReceived;
            HistoricalUnit = facility.HistoricalUnit;
            HistoricalComplianceOfficer = facility.HistoricalComplianceOfficer;
            TaxId = facility.TaxId;
            HasERecord = facility.HasERecord;
            IsRetained = facility.IsRetained;
            Cabinets = facility.CabinetsToString;
            RetentionRecords = facility.RetentionRecordsToString;
        }

        [XLColumn(Header = "Facility Number")]
        public string FacilityNumber { get; }

        [XLColumn(Header = "Facility Name")]
        public string Name { get; }

        [XLColumn(Header = "Active Site")]
        public bool Active { get; }

        [XLColumn(Header = "County")]
        public string County { get; }

        [XLColumn(Header = "Facility Status")]
        public string FacilityStatus { get; }

        [XLColumn(Header = "Type/Environmental Interest")]
        public string FacilityType { get; }

        [XLColumn(Header = "Budget Code")]
        public string BudgetCode { get; }

        [XLColumn(Header = "Organizational Unit")]
        public string OrganizationalUnit { get; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficer { get; }

        [XLColumn(Header = "File Label")]
        public string FileLabel { get; }

        [XLColumn(Header = "Location Description")]
        public string Location { get; }

        [XLColumn(Header = "Street Address")]
        public string Address { get; }

        [XLColumn(Header = "City")]
        public string City { get; }

        [XLColumn(Header = "State")]
        public string State { get; }

        [XLColumn(Header = "ZIP Code")]
        public string PostalCode { get; }

        [XLColumn(Header = "Latitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Latitude { get; }

        [XLColumn(Header = "Longitude")]
        [DisplayFormat(DataFormatString = "{0:F6}")]
        public decimal Longitude { get; }

        // The following properties only apply to Release Notifications
        [XLColumn(Header = "HSI Number")]
        public string HSInumber { get; set; }

        [XLColumn(Header = "Determination Letter Date")]
        public DateOnly? DeterminationLetterDate { get; set; }

        [XLColumn(Header = "Comments")]
        public string Comments { get; set; }

        [XLColumn(Header = "Pre-RQSM Cleanup")]
        public bool PreRQSMcleanup { get; set; }

        [XLColumn(Header = "Image Checked")]
        public bool ImageChecked { get; set; }

        [XLColumn(Header = "Deferred OnSite Scoring")]
        public bool DeferredOnSiteScoring { get; set; }

        [XLColumn(Header = "Additional Data Requested")]
        public bool AdditionalDataRequested { get; set; }

        [XLColumn(Header = "VRP Referral")]
        public bool VRPReferral { get; set; }

        [XLColumn(Header = "Date Received")]
        public DateOnly? RNDateReceived { get; set; }

        [XLColumn(Header = "Historical Unit")]
        public string HistoricalUnit { get; set; }

        [XLColumn(Header = "Historical C.O.")]
        public string HistoricalComplianceOfficer { get; set; }

        [XLColumn(Header = "Tax ID")]
        public string TaxId { get; set; }

        [XLColumn(Header = "Has Electronic Records")]
        public bool HasERecord { get; set; }

        [XLColumn(Header = "Is Retained Onsite")]
        public bool IsRetained { get; }

        [XLColumn(Header = "Cabinets")]
        public string Cabinets { get; }

        [XLColumn(Header = "Retention Records")]
        public string RetentionRecords { get; }
    }
}