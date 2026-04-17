using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Reporting.SiteSummary
{
    [AllowAnonymous]
    public class ReportModel : PageModel
    {
        private readonly IReportingRepository _repository;
        private readonly IConfiguration _configuration;

        public ReportModel(IReportingRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public string GoogleMapsApiKey => _configuration["GoogleMapSettings:ApiKey"] ?? string.Empty;

        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        public bool ShowHeader { get; set; } = false;

        [BindProperty]
        public IReadOnlyList<SiteSummaryReportDto> ReportList { get; set; } = new List<SiteSummaryReportDto>();

        [BindProperty]
        public SiteSummaryReportDto Report { get; set; }

        public async Task<IActionResult> OnGetAsync(SiteSummaryQuerySpec spec = null, [FromRoute] string hsiId = null)
        {
            if (!string.IsNullOrEmpty(hsiId))
            {
                Report = await _repository.GetSingleFacilitySiteSummaryDtoAsync(hsiId);
                ReportList = new List<SiteSummaryReportDto>() { Report };
                ShowHeader = false;
                return Page();
            }

            if (User.Identity is not { IsAuthenticated: true })
                return Challenge();

            Spec = spec;
            Spec.TrimAll();
            ShowHeader = Spec.ShowHeader;

            ReportList = await _repository.GetFacilitySiteSummaryDtoAsync(Spec);

            return Page();
        }

        public string GetGoogleMapsUrl(SiteSummaryReportDto facility)
        {
            if (facility.Latitude != 0 && facility.Longitude != 0 && facility.LocationDetails != null)
            {
                return
                    $"https://maps.googleapis.com/maps/api/staticmap?center={facility.Latitude},{facility.Longitude}&zoom={facility.LocationDetails?.MapZoom}&size=250x250&markers=color:red|{facility.Latitude},{facility.Longitude}&maptype=roadmap&key={GoogleMapsApiKey}&style=feature:poi|visibility:off";
            }

            return null;
        }

        public string GetStatusLanguage(SiteSummaryReportDto facility) =>
            SiteSummaryHelper.GetCleanupStatusLanguage(facility);

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
            foreach (var parcel in facility.Parcels)
            {
                if (parcel.ParcelType?.Name == "SubList")
                {
                    return true;
                }
            }

            return false;
        }
    }
}
