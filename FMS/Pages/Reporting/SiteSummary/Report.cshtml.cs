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
            Spec.TrimAll();

            ReportList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);
            
            return Page();
        }

        public string GetGoogleMapsUrl(SiteSummaryReportDto facility)
        {
            return new UrlHelper(_configuration).GetGoogleMapsUrlLink(facility.Latitude, facility.Longitude, facility.LocationDetails.MapZoom, facility.LocationDetails.MapType);
        }

        public string GetStatusLanguage(SiteSummaryReportDto facility) => SiteSummaryHelper.GetCleanupStatusLanguage(facility);

        public string GetScoreLanguage(SiteSummaryReportDto facility)
        {
            var groundWaterLang = SiteSummaryHelper.GetLanguageForGWScore(facility);
            var onsiteScoreLang = SiteSummaryHelper.GetLanguageForOSScore(facility);
            var exLang = "";
            if (facility.ScoreDetails != null && facility.ScoreDetails.UseComments)
            {
                exLang = SiteSummaryHelper.GetLanguageForExceptions(facility);
            }
            return groundWaterLang + onsiteScoreLang + exLang;
        }

        public bool HasSublistedParcels(SiteSummaryReportDto facility)
        {
            foreach(var parcel in facility.Parcels)
            {
                if(parcel.ParcelType?.Name == "SubList")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
