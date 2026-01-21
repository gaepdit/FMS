using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto.Reports
{
    public class DelistedReportByDateRangeDto
    {
        public DelistedReportByDateRangeDto() { }

        public DelistedReportByDateRangeDto(Facility facility, DateOnly startDate, DateOnly endDate, double? totalAcres)
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
            StartDate = startDate;
            EndDate = endDate;
            TotalAcres = totalAcres;
        }

        public string HSIID { get; set; }
        public double? Acres { get; set; }
        public string FacilityName { get; set; }
        public string FacilityTypeName { get; set; }
        public DateOnly? ListedDate { get; set; }
        public DateOnly? DelistedDate { get; set; }
        public string CountyName { get; set; }
        public string ComplianceOfficerName { get; set; }
        public string HSRAComplianceOfficerName { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public double? TotalAcres { get; set; }
    }
}

