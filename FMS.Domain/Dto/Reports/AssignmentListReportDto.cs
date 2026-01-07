using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class AssignmentListReportDto
    {
        public AssignmentListReportDto() { }

        public AssignmentListReportDto(Facility facility)
        {
            FacilityNumber = facility.FacilityNumber;
            FacilityName = facility.Name;
            EnvironmentalInterestName = facility.FacilityType?.Name;
            CountyName = facility.County?.Name;
            OrganizationalUnitName = facility.OrganizationalUnit?.Name;
            ComplianceOfficerName = facility.ComplianceOfficer?.Name;
            GeologistName = facility.HsrpFacilityProperties?.ComplianceOfficer?.Name;
        }

        public string FacilityNumber { get; set; }

        public string FacilityName { get; set;}

        public string EnvironmentalInterestName { get; set; }

        public string CountyName { get; set; }

        public string OrganizationalUnitName { get; set; }

        public string ComplianceOfficerName { get; set; }

        public string GeologistName { get; set; }
    }
}
