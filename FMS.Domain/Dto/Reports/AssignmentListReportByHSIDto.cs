using ClosedXML.Attributes;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class AssignmentListReportByHSIDto
    {
        public AssignmentListReportByHSIDto() { }

        public AssignmentListReportByHSIDto(Facility facility)
        {
            FacilityNumber = facility.FacilityNumber;
            FacilityName = facility.Name;
            FacilityStatusName = facility.FacilityStatus?.Name;
            CountyName = facility.County?.Name;
            OrganizationalUnitName = facility.HsrpFacilityProperties?.OrganizationalUnit?.Name;
            ComplianceOfficerName = facility.ComplianceOfficer?.Name;
        }

        [XLColumn(Header = "Status")]
        public string FacilityStatusName { get; set; }

        [XLColumn(Header = "HSI ID")]
        public string FacilityNumber { get; set; }

        [XLColumn(Header = "Site Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "County")]
        public string CountyName { get; set; }

        [XLColumn(Header = "C.O.")]
        public string ComplianceOfficerName { get; set; }

        [XLColumn(Header = "HSRA Buddy Unit")]
        public string OrganizationalUnitName { get; set; }
    }
}
