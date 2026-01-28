using ClosedXML.Attributes;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class DelistedReportByDateRangeDto
    {
        public DelistedReportByDateRangeDto() { }

        public DelistedReportByDateRangeDto(Facility facility)
        {
            HSIID = facility.FacilityNumber;
            Acres = facility.Parcels != null ? facility.Parcels.Sum(p => p.Acres) : null;
            FacilityName = facility.Name;
            FacilityTypeName = facility.FacilityType != null ? facility.FacilityType.Name : string.Empty;
            ListedDate = facility.HsrpFacilityProperties.DateListed ?? DateOnly.MinValue;
            DelistedDate = facility.HsrpFacilityProperties.DateDeListed ?? DateOnly.MinValue;
            CountyName = facility.County != null ? facility.County.Name : string.Empty;
            ComplianceOfficerName = facility.ComplianceOfficer != null ? facility.ComplianceOfficer.Name : string.Empty;
            HSRAComplianceOfficerName = facility.HsrpFacilityProperties != null && facility.HsrpFacilityProperties.ComplianceOfficer != null ? facility.HsrpFacilityProperties.ComplianceOfficer.Name : string.Empty;
        }

        [XLColumn(Header = "HSI ID")]
        public string HSIID { get; set; }

        [XLColumn(Header = "Acres")]
        public double? Acres { get; set; }

        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "Facility Type")]
        public string FacilityTypeName { get; set; }

        [XLColumn(Header = "Listed Date")]
        public DateOnly? ListedDate { get; set; }

        [XLColumn(Header = "Delisted Date")]
        public DateOnly? DelistedDate { get; set; }

        [XLColumn(Header = "County")]
        public string CountyName { get; set; }

        [XLColumn(Header = "Compliance Officer")]
        public string ComplianceOfficerName { get; set; }

        [XLColumn(Header = "HSRA Compliance Officer")]
        public string HSRAComplianceOfficerName { get; set; }
    }
}

