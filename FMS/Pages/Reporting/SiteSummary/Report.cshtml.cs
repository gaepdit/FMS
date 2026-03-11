using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.SiteSummary
{
    public class ReportModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public ReportModel(IReportingRepository repository)
        {
            _repository = repository;
        }

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
    }
}
