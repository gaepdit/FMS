using DocumentFormat.OpenXml.Wordprocessing;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.Events
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public IndexModel(IReportingRepository repository) => _repository = repository;

        [Display(Name = "Start Date")]
        [BindProperty]
        public DateOnly? StartDate { get; set; } = null;

        [Display(Name = "End Date")]
        [BindProperty]
        public DateOnly? EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostPendingAsync()
        {
            var fileName = $"Events_Pending_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsPendingList" List to go to a report
            IList<EventReportDto> eventsPendingList = await _repository.GetEventsReportsAsync();

            // Sort list by Organizational Unit and filter only pending events
            eventsPendingList = eventsPendingList 
                .Where(e => e.CompletionDate == null).ToList();
            //eventsPendingList = EventSortHelper.SortReportEvents(eventsPendingList, EventReportSort.OrgUnitDesc);

            // Map to EventsPendingReportDto
            var eventsPendingReportList = from p in eventsPendingList
                                         select new EventsPendingReportDto(p);
              
            // Export to Excel
            return File(eventsPendingReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventPending), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostCompletedAsync()
        {
            var fileName = $"Events_Completed_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsCompletedList" List to go to a report
            IList<EventReportDto> eventsCompletedList = await _repository.GetEventsReportsAsync();

            // Sort list by Organizational Unit and filter only pending events
            eventsCompletedList = eventsCompletedList
                .Where(e => e.CompletionDate != null).ToList();
            eventsCompletedList = EventSortHelper.SortReportEvents(eventsCompletedList, EventReportSort.OrgUnitDesc);

            // Map to EventsCompletedReportDto
            var eventsCompletedReportList = from p in eventsCompletedList
                                            select new EventsCompletedReportDto (p);

            // Export to Excel
            return File(eventsCompletedReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompleted, StartDate, EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostComplianceAsync()
        {
            var fileName = $"Events_Compliance_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsComplianceList" List to go to a report
            IList<EventReportDto> eventsComplianceList = await _repository.GetEventsReportsAsync();

            // Sort list by Organizational Unit and filter only pending events
            eventsComplianceList = eventsComplianceList
                .Where(e => e.CompletionDate != null).ToList();
            eventsComplianceList = EventSortHelper.SortReportEvents(eventsComplianceList, EventReportSort.OrgUnitDesc);

            // Map to EventsComplianceReportDto
            var eventsComplianceReportList = from p in eventsComplianceList
                                            select new EventsComplianceReportDto(p);

            // Export to Excel
            return File(eventsComplianceReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompliance), "application/vnd.ms-excel", fileName);
        }
    }
}
