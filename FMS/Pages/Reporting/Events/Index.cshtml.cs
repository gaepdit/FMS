using FMS.Domain.Dto;
using FMS.Domain.Dto.Reports;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FMS.Pages.Reporting.Events
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;
        private readonly ISelectListHelper _listHelper;
        private readonly IEventTypeRepository _eventTypeRepository;
        public IndexModel(
            IReportingRepository repository,
            ISelectListHelper listHelper,
            IEventTypeRepository eventTypeRepository)   
        {
            _repository = repository;
            _listHelper = listHelper;
            _eventTypeRepository = eventTypeRepository;
        }

        [BindProperty]
        public EventReportSpecDto Spec { get; set; }

        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
        public SelectList EventTypes { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Spec = new EventReportSpecDto();
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostPendingAsync(EventReportSpecDto spec)
        {
            Spec = spec;
            var fileName = $"Events_Pending_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP" };

            // "eventsPendingList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, spec);

            // Filter and sort list for Events Pending Report
            var eventsPendingList = EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventPending);

            // Map to EventsPendingReportDto
            var eventsPendingReportList = from p in eventsPendingList
                select new EventsPendingReportDto(p);

            // Export to Excel
            return File(eventsPendingReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventPending),
                "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostCompletedAsync(EventReportSpecDto spec)
        {
            Spec = spec;
            var fileName = $"Activity_Completed_By_CO_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP" };

            // "eventsCompletedList" List to go to a report
            IList<EventReportDto> eventsList = await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            // Sort list by Organizational Unit and filter only pending events
            var eventsCompletedList =
                EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventCompleted, Spec.StartDate, Spec.EndDate);
            // Map to EventsCompletedReportDto
            var eventsCompletedReportList = from p in eventsCompletedList
                select new EventsCompletedReportDto(p);

            // Export to Excel
            return File(
                eventsCompletedReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompleted, Spec.StartDate,
                    Spec.EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostComplianceAsync(EventReportSpecDto spec)
        {
            Spec = spec;
            var fileName = $"Events_Compliance_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var selectedFacilityTypes = new List<string> { "HSI", "VRP" };
            Spec.EventTypeId = new List<Guid>
            {
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Notice of Violation"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Consent Order"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Administrative Order")
            };

            // "eventsComplianceList" List to go to a report
            IList<EventReportDto> eventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            // Sort list by Organizational Unit and filter only pending events
            var eventsComplianceList =
                EventSortHelper.OrderReportEventQuery(eventsList, EventReportSort.EventCompliance, Spec.StartDate, Spec.EndDate);
            // Map to EventsComplianceReportDto
            var eventsComplianceReportList = from p in eventsComplianceList
                select new EventsComplianceReportDto(p);

            // Export to Excel
            return File(
                eventsComplianceReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventCompliance, Spec.StartDate,
                    Spec.EndDate), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostCompletedOutstandingAsync(EventReportSpecDto spec)
        {
            Spec = spec;
            var fileName = $"Events_Completed_Outstanding_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var hsiCompCount = 0;
            var hsiRecCount = 0;
            var hsiCompTotal = 0.0m;
            var vrpCompCount = 0;
            var vrpRecCount = 0;
            var vrpCompTotal = 0.0m;
            var brnCompCount = 0;
            var brnRecCount = 0;
            var brnCompTotal = 0.0m;
            var days = 0.0m;
            IEnumerable<AbndInacChecklistReviewReportDto> checkListAIReportList = null;

            //******************* HSI ********************
            var selectedFacilityTypes = new List<string> { "HSI" };
            Spec.EventTypeId = new List<Guid>
            {
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Compliance Status Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Corrective Action Plan"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Groundwater Monitoring Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Progress Report / Misc. Report")
            };

            IList<EventReportDto> hsiEventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            var hsiCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(hsiEventsList,
                EventReportSort.EventCompletedOutstanding, Spec.StartDate, Spec.EndDate);

            foreach (EventReportDto item in hsiCompletedOutstandingList)
            {
                hsiRecCount++;
                days = (decimal)((item.CompletionDate.HasValue && item.StartDate.HasValue)
                    ? (item.CompletionDate.Value.ToDateTime(TimeOnly.MinValue) -
                       item.StartDate.Value.ToDateTime(TimeOnly.MinValue)).TotalDays
                    : 0);
                if (days != 0)
                {
                    hsiCompCount++;
                    hsiCompTotal += days;
                }
            }

            decimal hsiAvg = Math.Round(hsiCompCount == 0 ? 0 : hsiCompTotal / hsiCompCount, 1);

            var hsiCompletedOutstandingReportList = from p in hsiCompletedOutstandingList
                select new EventsCompletedOutstandingReportDto(p);

            //*************************** VRP ******************************
            selectedFacilityTypes = ["VRP"];
            Spec.EventTypeId = new List<Guid>
            {
                await _eventTypeRepository.GetEventTypeIdByNameAsync("VRP Compliance Status Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("VRP Corrective Action Plan"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("VRP Progress Report / Misc. Report")
            };

            IList<EventReportDto> vrpEventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            var vrpCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(vrpEventsList,
                EventReportSort.EventCompletedOutstanding, Spec.StartDate, Spec.EndDate);

            foreach (var item in vrpCompletedOutstandingList)
            {
                days = (decimal)((item.CompletionDate.HasValue && item.StartDate.HasValue)
                    ? (item.CompletionDate.Value.ToDateTime(TimeOnly.MinValue) -
                       item.StartDate.Value.ToDateTime(TimeOnly.MinValue)).TotalDays
                    : 0);
                vrpRecCount++;
                if (days != 0)
                {
                    vrpCompCount++;
                    vrpCompTotal += days;
                }
            }

            decimal vrpAvg = Math.Round(vrpCompCount == 0 ? 0 : vrpCompTotal / vrpCompCount, 1);

            var vrpCompletedOutstandingReportList = from p in vrpCompletedOutstandingList
                select new EventsCompletedOutstandingReportDto(p);

            //********************** Brownfields *********************
            selectedFacilityTypes = ["BROWN"];
            Spec.EventTypeId = new List<Guid>
            {
               await _eventTypeRepository.GetEventTypeIdByNameAsync("Prospective Purchaser Compliance Status Report"),
               await _eventTypeRepository.GetEventTypeIdByNameAsync("Prospective Purchaser Corrective Action Plan")
            };

            IList<EventReportDto> brownEventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            var brownCompletedOutstandingList = EventSortHelper.OrderReportEventQuery(brownEventsList,
                EventReportSort.EventCompletedOutstanding, Spec.StartDate, Spec.EndDate);

            foreach (var item in brownCompletedOutstandingList)
            {
                days = (decimal)((item.CompletionDate.HasValue && item.StartDate.HasValue)
                    ? (item.CompletionDate.Value.ToDateTime(TimeOnly.MinValue) -
                       item.StartDate.Value.ToDateTime(TimeOnly.MinValue)).TotalDays
                    : 0);
                brnRecCount++;
                if (days != 0)
                {
                    brnCompCount++;
                    brnCompTotal += days;
                }
            }

            decimal brnAvg = Math.Round(brnCompCount == 0 ? 0 : brnCompTotal / brnCompCount, 1);

            var brownCompletedOutstandingReportList = from p in brownCompletedOutstandingList
                select new EventsCompletedOutstandingReportDto(p);

            // Export to Excel
            return File(hsiCompletedOutstandingReportList.ExportExcelAsByteArray(
                    ExportHelper.ReportType.EventCompletedOutstanding,
                    Spec.StartDate,
                    Spec.EndDate,
                    vrpCompletedOutstandingReportList,
                    brownCompletedOutstandingReportList,
                    checkListAIReportList,
                    hsiCompCount,
                    hsiRecCount,
                    hsiAvg,
                    vrpCompCount,
                    vrpRecCount,
                    vrpAvg,
                    brnCompCount,
                    brnRecCount,
                    brnAvg),
                "application/vnd.ms-excel",
                fileName);
        }

        public async Task<IActionResult> OnPostOutstandingAsync(EventReportSpecDto spec)
        {
            Spec = spec;
            var fileName = $"Events_Outstanding_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            var hsiRecCount = 0;
            var vrpRecCount = 0;
            var brnRecCount = 0;
            IEnumerable<AbndInacChecklistReviewReportDto> checkListAIReportList = null;

            //******************* HSI ********************
            var selectedFacilityTypes = new List<string> { "HSI" };
            Spec.EventTypeId = new List<Guid>
            {
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Compliance Status Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Corrective Action Plan"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Groundwater Monitoring Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Progress Report / Misc. Report")
            };

            IList<EventReportDto> hsiEventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            var hsiOutstandingList =
                EventSortHelper.OrderReportEventQuery(hsiEventsList, EventReportSort.EventOutstanding);

            hsiRecCount += hsiOutstandingList.Count();

            var hsiOutstandingReportList = from p in hsiOutstandingList select new EventsOutstandingReportDto(p);

            //*************************** VRP ******************************
            selectedFacilityTypes = ["VRP"];
            Spec.EventTypeId = new List<Guid>
            {
                await _eventTypeRepository.GetEventTypeIdByNameAsync("VRP Compliance Status Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("VRP Corrective Action Plan"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("VRP Progress Report / Misc. Report")
            };

            IList<EventReportDto> vrpEventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            var vrpOutstandingList =
                EventSortHelper.OrderReportEventQuery(vrpEventsList, EventReportSort.EventOutstanding);

            vrpRecCount += vrpOutstandingList.Count();

            var vrpOutstandingReportList = from p in vrpOutstandingList select new EventsOutstandingReportDto(p);

            //********************** Brownfields *********************
            selectedFacilityTypes = ["BROWN"];
            Spec.EventTypeId = new List<Guid>
            {
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Prospective Purchaser Compliance Status Report"),
                await _eventTypeRepository.GetEventTypeIdByNameAsync("Prospective Purchaser Corrective Action Plan")
            };

            IList<EventReportDto> brownEventsList =
                await _repository.GetEventsReportsAsync(selectedFacilityTypes, Spec);

            var brownOutstandingList =
                EventSortHelper.OrderReportEventQuery(brownEventsList, EventReportSort.EventOutstanding);

            brnRecCount += brownOutstandingList.Count();

            var brownOutstandingReportList = from p in brownOutstandingList select new EventsOutstandingReportDto(p);

            // Export to Excel
            return File(hsiOutstandingReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventOutstanding,
                    null,
                    null,
                    vrpOutstandingReportList,
                    brownOutstandingReportList,
                    checkListAIReportList,
                    0,
                    hsiRecCount,
                    0,
                    0,
                    vrpRecCount,
                    0,
                    0,
                    brnRecCount),
                "application/vnd.ms-excel",
                fileName);
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
            return File(
                eventsNoActionTakenReportList.ExportExcelAsByteArray(ExportHelper.ReportType.EventNoActionTaken),
                "application/vnd.ms-excel", fileName);
        }
        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
            EventTypes = await _listHelper.EventTypesSelectListAsync();
        }
    }
}
