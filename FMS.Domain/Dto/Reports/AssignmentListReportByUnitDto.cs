using ClosedXML.Attributes;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class AssignmentListReportByUnitDto
    {
        public AssignmentListReportByUnitDto() { }

        public AssignmentListReportByUnitDto(Facility facility)
        {
            ComplianceOfficer = facility.ComplianceOfficer?.Name;
            FacilityNumber = facility.FacilityNumber;
            FacilityName = facility.Name;
            FacilityStatusName = facility.FacilityStatus?.Name;
            CountyName = facility.County?.Name;
            OrganizationalUnit = facility.OrganizationalUnit?.Name;
            GeologistName = facility.HsrpFacilityProperties?.ComplianceOfficer?.Name;
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
        public string ComplianceOfficer { get; set; }

        [XLColumn(Header = "HSRA Unit")]
        public string OrganizationalUnit { get; set; }

        [XLColumn(Header = "Geologist")]
        public string GeologistName { get; set; }
    }
}
