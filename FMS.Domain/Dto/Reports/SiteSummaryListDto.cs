using ClosedXML.Attributes;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SiteSummaryListDto
    {
        private const string siteSummaryReportPath = "https://dev-fms.gaepd.org/Reporting/SiteSummary/Report/";

        public SiteSummaryListDto(Facility facility) 
        {
            FacilityNumber = facility.FacilityNumber;
            FacilityName = facility.Name;
            SiteSummaryUrl = siteSummaryReportPath + facility.FacilityNumber;
        }

        [Display(Name = "HSI ID")]
        [XLColumn(Header = "HSI ID")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "Site Summary")]
        [XLColumn(Header = "Site Summary")]
        public string SiteSummaryUrl { get; set; }

    }
}
