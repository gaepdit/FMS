using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto.Reports
{
    public class DelistedReportByDateDto
    {
        public DelistedReportByDateDto() { }

        public DelistedReportByDateDto(Facility facility)
        {
            DelistedDate = facility.HsrpFacilityProperties.DateDeListed.HasValue ? facility.HsrpFacilityProperties.DateDeListed.Value : null;
            HSIID = facility.FacilityNumber;
            FacilityName = facility.Name;
            CountyName = facility.County != null ? facility.County.Name : string.Empty;
            OrgUnitName = facility.OrganizationalUnit != null ? facility.OrganizationalUnit.Name : string.Empty;
            ComplianceOfficerName = facility.ComplianceOfficer != null ? facility.ComplianceOfficer.Name : string.Empty;
            HSRAComplianceOfficerName = facility.HsrpFacilityProperties != null && facility.HsrpFacilityProperties.ComplianceOfficer != null ? facility.HsrpFacilityProperties.ComplianceOfficer.Name : string.Empty;
        }
        public DateOnly? DelistedDate { get; set; }
        public string HSIID { get; set; }
        public string FacilityName { get; set; }
        public string CountyName { get; set; }
        public string OrgUnitName { get; set; }
        public string ComplianceOfficerName { get; set; }
        public string HSRAComplianceOfficerName { get; set; }
        }
}
