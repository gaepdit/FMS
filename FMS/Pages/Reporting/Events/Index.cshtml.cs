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
            var selectedFacilityTypes = new List<string> { "HSI", "VRP" };

            // "eventsPendingList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes);

            // Filter and sort list for Events Pending Report
            var eventsPendingList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventPending);

            // Map to EventsPendingReportDto
            var eventsPendingReportList = from p in eventsPendingList
                                         select new EventsPendingReportDto(p);
              
            // Export to Excel
            return File(eventsPendingReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventPending), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostCompletedAsync()
        {
            var fileName = $"Activity_Completed_By_CO_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP" };

            // "eventsCompletedList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes);

            // Sort list by Organizational Unit and filter only pending events
            var eventsCompletedList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventCompleted, StartDate, EndDate);

            // Map to EventsCompletedReportDto
            var eventsCompletedReportList = from p in eventsCompletedList
                                            select new EventsCompletedReportDto (p);

            // Export to Excel
            return File(eventsCompletedReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompleted, StartDate, EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostComplianceAsync()
        {
            var fileName = $"Events_Compliance_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP" };
            var selectedEventTypes = new List<string> { "Notice of Violation", "Consent Order",  "Administrative Order" };  

            // "eventsComplianceList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, selectedEventTypes);

            // Sort list by Organizational Unit and filter only pending events
            var eventsComplianceList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventCompliance, StartDate, EndDate);

            // Map to EventsComplianceReportDto
            var eventsComplianceReportList = from p in eventsComplianceList
                                            select new EventsComplianceReportDto(p);

            // Export to Excel
            return File(eventsComplianceReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompliance, StartDate, EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostCompletedOutstandingAsync()
        {
            var fileName = $"Events_Completed_Outstanding_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP", "BROWN" };
            var selectedEventTypes = new List<string> 
            { 
                "Compliance Status Report", 
                "Corrective Action Plan",
                "Groundwater Monitoring Report",
                "Progress Report / Misc. Report",
                "Prospective Purchaser Compliance Status Report",
                "VRP Compliance Status Report",
                "VRP Corrective Action Plan",
                "VRP Progress Report / Misc. Report"
            };

            // "eventsComplianceList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, selectedEventTypes);

            // Sort list by Organizational Unit and filter only pending events
            var eventsCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventCompletedOutstanding, StartDate, EndDate);

            // Map to EventsCompletedOutstandingReportDto
            var eventsCompletedOutstandingReportList = from p in eventsCompletedOutstandingList
                                             select new EventsCompletedOutstandingReportDto(p);

            // Export to Excel
            return File(eventsCompletedOutstandingReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompletedOutstanding, StartDate, EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostActivityCompletedAsync()
        {
            var fileName = $"Events_Activity_Completed_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP", "BROWN" };

            // "eventsActivityCompletedList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes);

            // Sort list by CO and filter only date range
            var eventsActivityCompletedList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventActivityCompleted, StartDate, EndDate);
            // Map to EventsActivityCompletedByCOReportDto
            var eventsActivityCompletedReportList = from p in eventsActivityCompletedList
                                             select new EventsActivityCompletedByCOReportDto(p);

            // Export to Excel
            return File(eventsActivityCompletedReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventActivityCompleted, StartDate, EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostNoActionTakenAsync()
        {
            var fileName = $"Events_NoActionTaken_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP", "BROWN" };

            // "eventsNoActionTakenList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes);

            // Filter and sort list for Events NoActionTaken Report
            var eventsNoActionTakenList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventNoActionTaken);

            // Map to EventsNoActionTakenReportDto
            var eventsNoActionTakenReportList = from p in eventsNoActionTakenList
                                          select new EventsNoActionTakenReportDto(p);

            // Export to Excel
            return File(eventsNoActionTakenReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventNoActionTaken), "application/vnd.ms-excel", fileName);
        }
    }
}
