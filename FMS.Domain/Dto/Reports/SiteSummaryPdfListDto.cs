using ClosedXML.Attributes;
using FMS.Domain.Entities;
using FMS;

namespace FMS.Domain.Dto.Reports
{
    public class SiteSummaryPdfListDto
    {
        public SiteSummaryPdfListDto(Facility facility)
        {
            FacilityNumber = facility.FacilityNumber;
            FacilityName = facility.Name;
            Latitude = facility.Latitude;
            Longitude = facility.Longitude;
            Address = facility.Address;
            City = facility.City;
            County = facility.County.Name;
            ListDate = facility.HsrpFacilityProperties.DateListed;
            Class = facility.LocationDetails.LocationClass.Name;
            InvestigationCleanupFunding = facility.StatusDetails.FundingSource.Name switch
            {
                "A" => "A",
                "LE" => "L",
                "LI" => "RP",
                "P" => "RP",
                "SE" => "L",
                "SI" => "RP",
                _ => string.Empty
            };
            Icon = facility.StatusDetails.FundingSource.Name switch
            {
                "A" => "small_yellow",
                "LE" => "small_green",
                "LI" => "small_green",
                "P" => "small_red",
                "SE" => "small_red",
                "SI" => "small_red",
                _ => string.Empty
            };
        }

        [Display(Name = "HSI ID")]
        [XLColumn(Header = "HSI ID")]
        public string FacilityNumber { get; set; }

        [Display(Name = "Facility Name")]
        [XLColumn(Header = "Facility Name")]
        public string FacilityName { get; set; }

        [Display(Name = "Latitude")]
        [XLColumn(Header = "Latitude")]
        public decimal Latitude { get; set; }

        [Display(Name = "Longitude")]
        [XLColumn(Header = "Longitude")]
        public decimal Longitude { get; set; }

        [Display(Name = "Address")]
        [XLColumn(Header = "Address")]
        public string Address { get; set; }
        
        [Display(Name = "City")]
        [XLColumn(Header = "City")]
        public string City { get; set; }
        
        [Display(Name = "County")]
        [XLColumn(Header = "County")]
        public string County { get; set; }

        [Display(Name = "List Date")]
        [XLColumn(Header = "List Date")]
        public DateOnly? ListDate { get; set; }

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
                    "Development" => DomainConstants.siteSummaryReportPathPdfDev + FacilityNumber + DomainConstants.pdfSuffix,
                    "UAT" => DomainConstants.siteSummaryReportPathPdfUat + FacilityNumber + DomainConstants.pdfSuffix,
                    _ => DomainConstants.siteSummaryReportPathPdfProd + FacilityNumber + DomainConstants.pdfSuffix,
                };
            }
        }

        [Display(Name = "Investigation/Cleanup Funding")]
        [XLColumn(Header = "Investigation/Cleanup Funding")]
        public string InvestigationCleanupFunding { get; set; }

        [Display(Name = "Icon")]
        [XLColumn(Header = "Icon")]
        public string Icon { get; set; }
    }
}
