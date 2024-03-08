using ClosedXML.Attributes;
using FMS.Domain.Dto;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Helpers
{
    public class FacilityPendingDtoScalar
    {
        public FacilityPendingDtoScalar(FacilityDetailDto facility)
        {
            FacilityNumber = facility.FacilityNumber.Substring(2);
            Name = facility.Name;
            OrganizationalUnit = facility.OrganizationalUnit?.Name;
            ComplianceOfficer = facility.ComplianceOfficer?.Name;
            RNDateReceived = facility.RNDateReceived;
            Comments = facility.Comments;
            PreRQSMcleanup = facility.PreRQSMcleanup;
            DeferredOnSiteScoring = facility.DeferredOnSiteScoring;
            AdditionalDataRequested = facility.AdditionalDataRequested;
            VRPReferral = facility.VRPReferral;
            ImageChecked = facility.ImageChecked;
        }

        [XLColumn(Header = "Notification ID")]
        public string FacilityNumber { get; }

        [XLColumn(Header = "Facility Name")]
        public string Name { get; }

        [XLColumn(Header = "Org Unit")]
        public string OrganizationalUnit { get; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficer { get; }

        [XLColumn(Header = "Date Received")]
        public DateOnly? RNDateReceived { get; set; }

        [XLColumn(Header = "Comments")]
        public string Comments { get; set; }

        [XLColumn(Header = "Pre-RQSM Cleanup")]
        public bool PreRQSMcleanup { get; set; }

        [XLColumn(Header = "Brownfield Deferral")]
        public bool DeferredOnSiteScoring { get; set; }

        [XLColumn(Header = "Additional Data Requested")]
        public bool AdditionalDataRequested { get; set; }

        [XLColumn(Header = "VRP Deferral")]
        public bool VRPReferral { get; set; }

        [XLColumn(Header = "Image Checked")]
        public bool ImageChecked { get; set; }
    }
}
