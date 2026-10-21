using ClosedXML.Attributes;
using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class SiteSummaryListDto
    {
        public SiteSummaryListDto(Facility facility)
        {
            FacilityNumber = facility.FacilityNumber;
            FacilityName = facility.Name;
            County = facility.County.Name;
            Class = facility.LocationDetails.LocationClass.Name;
        }

        [Display(Name = "HSI ID")]
        [XLColumn(Header = "HSI ID")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "County")]
        [XLColumn(Header = "County")]
        public string County { get; set; }

        [Display(Name = "Class")]
        [XLColumn(Header = "Class")]
        public string Class { get; set; }

        [Display(Name = "Site Summary")]
        [XLColumn(Header = "Site Summary")]
        public string SiteSummaryUrl
        {
            get
            {
                // Determine the environment and return the appropriate URL
                string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
                return environment switch
                {
                    "Development" => DomainConstants.SiteSummaryReportPathPdfDev + FacilityNumber + DomainConstants.pdfSuffix,
                    "UAT" => DomainConstants.SiteSummaryReportPathPdfUat + FacilityNumber + DomainConstants.pdfSuffix,
                    _ => DomainConstants.SiteSummaryReportPathPdfProd + FacilityNumber + DomainConstants.pdfSuffix,
                };
            }
        }

    }
}
