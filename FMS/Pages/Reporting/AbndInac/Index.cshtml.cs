using FMS.Domain.Dto;
using FMS.Domain.Dto.Reports;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Reporting.AbndInac
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public IndexModel(IReportingRepository repository) => _repository = repository;

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostTrackerAsync()
        {
            var fileName = $"AbndInacStatusTracker_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // First Worksheet "ABND/INAC" List to go to report
            IReadOnlyList<AbndInacStatusTrackerDto> abndInacReportList =
                await _repository.GetAbndInacStatusTrackerReportAsync();

            // Second Worksheet "Checklist" List to go to report
            IReadOnlyList<AbndInacChecklistReviewDto> checkList = await _repository.GetAbndInacChecklistReviewAsync();

            var checkListReportList = from p in checkList select new AbndInacChecklistReviewReportDto(p);

            DateOnly? StartDate = null;
            DateOnly? EndDate = null;
            IEnumerable<AbndInacStatusTrackerDto> vrpCompletedOutstandingReportList = null;
            IEnumerable<AbndInacStatusTrackerDto> brownCompletedOutstandingReportList = null;

            // Export to Excel
            return File(abndInacReportList.ExportExcelAsByteArray(ExportHelper.ReportType.AbndInacStatusTracker,
                    StartDate,
                    EndDate,
                    vrpCompletedOutstandingReportList,
                    brownCompletedOutstandingReportList,
                    checkListReportList),
                "application/vnd.ms-excel",
                fileName);
        }

        public async Task<IActionResult> OnPostCostAsync()
        {
            var fileName = $"AbndCostEstimateReport_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsPendingList" List to go to a report
            IReadOnlyList<AbndCostEstimateReportDto> abndInacReportList =
                await _repository.GetAbndCostEstimateReportAsync();

            // Export to Excel
            return File(abndInacReportList.ExportExcelAsByteArray(ExportHelper.ReportType.AbndCostEstimateReport),
                "application/vnd.ms-excel", fileName);
        }
    }
}
