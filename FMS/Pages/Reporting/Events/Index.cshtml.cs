using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
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
            var selectedFacilityTypes = new List<string> { "HSI" }; //, "VRP", "BROWN" 
            var hsiEventTypes = new List<string> 
            {
                "Compliance Status Report",
                "Corrective Action Plan",
                "Groundwater Monitoring Report",
                "Progress Report / Misc. Report" 
            };

            IList<EventReportDto> hsiEventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, hsiEventTypes);

            var hsiCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(hsiEventsList, EventReportSort.EventCompletedOutstanding, StartDate, EndDate);

            var hsiCompletedOutstandingReportList = from p in hsiCompletedOutstandingList    select new EventsCompletedOutstandingReportDto(p);

            selectedFacilityTypes = ["VRP"];
            var vrpEventTypes = new List<string>
            {
                "VRP Compliance Status Report",
                "VRP Corrective Action Plan",
                "VRP Progress Report / Misc. Report"
            };

            IList<EventReportDto> vrpEventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, vrpEventTypes);

            var vrpCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(vrpEventsList, EventReportSort.EventCompletedOutstanding, StartDate, EndDate);

            var vrpCompletedOutstandingReportList = from p in vrpCompletedOutstandingList select new EventsCompletedOutstandingReportDto(p);

            selectedFacilityTypes = ["BROWN"];
            var brownEventTypes = new List<string>
            {
                "Prospective Purchaser Compliance Status Report",
                "Prospective Purchaser Corrective Action Plan"
            };

            IList<EventReportDto> brownEventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, brownEventTypes);

            var brownCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(brownEventsList, EventReportSort.EventCompletedOutstanding, StartDate, EndDate);

            var brownCompletedOutstandingReportList = from p in brownCompletedOutstandingList select new EventsCompletedOutstandingReportDto(p);

            // Export to Excel
            return File(hsiCompletedOutstandingReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompletedOutstanding, StartDate, EndDate, vrpCompletedOutstandingReportList, brownCompletedOutstandingReportList), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostNoActionTakenAsync()
        {
            var fileName = $"Events_NoActionTaken_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsNoActionTakenList" List to go to a report
            IList<EventsNoActionTakenReportDto> eventsList = await _repository.GetEventsNoActionTakenReportAsync();

            // Map to EventsNoActionTakenReportDto
            var eventsNoActionTakenReportList = from p in eventsList
                                                select new EventsNoActionTakenReportDto(p);

            // Export to Excel
            return File(eventsNoActionTakenReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventNoActionTaken), "application/vnd.ms-excel", fileName);
        }
    }
}
