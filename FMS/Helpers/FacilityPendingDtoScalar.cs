using ClosedXML.Attributes;
using FMS.Domain.Dto;
using System;

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
    }
}
