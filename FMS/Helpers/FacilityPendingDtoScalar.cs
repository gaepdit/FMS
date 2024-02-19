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
            HistoricalUnit = facility.HistoricalUnit;
            ComplianceOfficer = facility.ComplianceOfficer?.Name;
            RNDateReceived = facility.RNDateReceived;
            Comments = facility.Comments;
        }

        [XLColumn(Header = "Facility Number")]
        public string FacilityNumber { get; }

        [XLColumn(Header = "Facility Name")]
        public string Name { get; }

        [XLColumn(Header = "Organizational Unit")]
        public string OrganizationalUnit { get; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficer { get; }

        [XLColumn(Header = "Historical Unit")]
        public string HistoricalUnit { get; set; }

        [XLColumn(Header = "Date Received")]
        public DateOnly? RNDateReceived { get; set; }

        [XLColumn(Header = "Comments")]
        public string Comments { get; set; }
    }
}
