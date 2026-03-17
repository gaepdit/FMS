using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Helpers;

namespace FMS.Pages.Reporting.SiteSummary
{
    public class ReportModel : PageModel
    {
        private readonly IReportingRepository _repository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public ReportModel(IReportingRepository repository, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public string GoogleMapsApiKey => _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;

        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        [BindProperty]
        public IReadOnlyList<SiteSummaryReportDto> ReportList { get; set; } = new List<SiteSummaryReportDto>();

        public async Task<PageResult> OnGetAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;

            ReportList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);

            return Page();
        }

        public string GetGoogleMapsUrl(SiteSummaryReportDto facility)
        {
            if (facility.Latitude != 0 && facility.Longitude != 0)
            {
                return $"https://maps.googleapis.com/maps/api/staticmap?center={facility.Latitude},{facility.Longitude}&zoom={facility.LocationDetails.MapZoom}&size=250x250&markers=color:red%7C{facility.Latitude},{facility.Longitude}&maptype={facility.LocationDetails.MapType}&key={GoogleMapsApiKey}";
            }
            return null;
        }

        public string GetStatusLanguage(SiteSummaryReportDto facility)
        {
            var cleanupstat = SiteSummaryHelper.GetCleanupStatusParameter(facility);
            var statusLanguage = SiteSummaryHelper.GetCleanupStatusLanguage(facility, cleanupstat);
            return statusLanguage ;
        }

        public string GetScoreLanguage(SiteSummaryReportDto facility)
        {
            var groundWaterLang = SiteSummaryHelper.GetLanguageForGWScore(facility);
            var onsiteScoreLang = SiteSummaryHelper.GetLanguageForOSScore(facility);
            return groundWaterLang + onsiteScoreLang;
        }
    }
}
