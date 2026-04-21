using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Reporting.SiteSummary
{
    public class SiteSummaryListModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public SiteSummaryListModel(IReportingRepository repository)
        {
            _repository = repository;
        }

        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        [BindProperty]
        public IReadOnlyList<SiteSummaryListDto> ReportList { get; set; } = [];

        public async Task<IActionResult> OnGetAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;
            Spec.TrimAll();

            ReportList = await _repository.GetSiteSummaryListAsync(Spec);

            return Page();
        }
    }
}
