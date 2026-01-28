using DocumentFormat.OpenXml.Wordprocessing;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.Events
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public IndexModel(IReportingRepository repository) => _repository = repository;

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostPendingAsync()
        {
            var fileName = $"Events_Pending_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "delistedByDateRangeReportList" Detailed Facility List to go to a report
            IReadOnlyList<EventSummaryDto> eventsPendingSummaryList = await _repository.GetEventsPendingAsync();



            return File(eventsPendingSummaryList.ExportExcelAsByteArray(ExportHelper.ReportType.Event), "application/vnd.ms-excel", fileName);
        }
    }
}
