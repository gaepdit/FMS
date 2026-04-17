using ClosedXML.Attributes;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class ListedReportByDateDto
    {
        public ListedReportByDateDto() { }

        public ListedReportByDateDto(Facility facility)
        {
            ListedDate = facility.HsrpFacilityProperties.DateListed.Value;
            HSIID = facility.FacilityNumber;
            FacilityName = facility.Name;
            CountyName = facility.County != null ? facility.County.Name : string.Empty;
            OrgUnitName = facility.OrganizationalUnit != null ? facility.OrganizationalUnit.Name : string.Empty;
            ComplianceOfficerName = facility.ComplianceOfficer != null ? facility.ComplianceOfficer.Name : string.Empty;
            HSRAComplianceOfficerName =
                facility.HsrpFacilityProperties != null && facility.HsrpFacilityProperties.ComplianceOfficer != null
                    ? facility.HsrpFacilityProperties.ComplianceOfficer.Name
                    : string.Empty;
        }

        [XLColumn(Header = "Listed Date")]
        public DateOnly? ListedDate { get; set; }

        [XLColumn(Header = "HSI ID")]
        public string HSIID { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "County")]
        public string CountyName { get; set; }

        [XLColumn(Header = "Org Unit")]
        public string OrgUnitName { get; set; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficerName { get; set; }

        [XLColumn(Header = "HSRA Compliance Officer")]
        public string HSRAComplianceOfficerName { get; set; }
    }
}
