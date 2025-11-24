using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Infrastructure.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Event
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class EditModel : PageModel
    {
        private readonly IEventRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IEventRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public EventEditDto EditEvent { get; set; }

        public FacilityDetailDto Facility { get; set; }

        public IList<EventSummaryDto> Events { get; set; }

        public SelectList EventTypes { get; private set; }
        public SelectList AllowedActionsTaken { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
        public SelectList EventContractors { get; private set; }

        [BindProperty]
        public Guid Id { get; set; }

        [TempData]
        [BindProperty]
        public string ActiveTab { get; set; } = "Events";

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            EditEvent = await _repository.GetEventByIdAsync(id);
            if (EditEvent == null)
            {
                return NotFound();
            }
            Facility = await _facilityRepository.GetFacilityAsync(EditEvent.FacilityId);
           
            Events = EventSortHelper.SortEvents(Facility.Events);

            await PopulateSelectsAsync();
            ActiveTab = "Events";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }
            try
            {
                await _repository.UpdateEventAsync(EditEvent);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateSelectsAsync();
                return Page();
            }

            ActiveTab = "Events";

            TempData?.SetDisplayMessage(Context.Success, $"Event successfully updated.");

            return RedirectToPage("../Facilities/Details", new { id = EditEvent.FacilityId });
        }

        public async Task<IActionResult> OnPostExportButtonAsync()
        {
            var facility = await _facilityRepository.GetFacilityAsync(EditEvent.FacilityId);
            var fileName = $"FMS_{facility.Name}_Event_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            var eventEdit = await _repository.GetEventByIdAsync(Id);
            var parentId = eventEdit.EventType.Name == "HWTF Master Project" ? eventEdit.Id : eventEdit.ParentId;

            // "EventDetailList" Detailed Event List to go to a report
            IReadOnlyList<EventSummaryDto> eventReportList = 
                eventEdit.EventType.Name == "HWTF Master Project" ? (IReadOnlyList<EventSummaryDto>)await _repository.GetEventsByFacilityIdAndParentIdAsync(facility.Id, (Guid)parentId) : (IReadOnlyList<EventSummaryDto>)await _repository.GetEventsByFacilityIdAsync(facility.Id);

            var eventDetailList = from p in eventReportList select new EventSummaryDtoScalar(p, facility.Name, facility.FacilityNumber);
            ActiveTab = "Events";
            return File(eventDetailList.ExportExcelAsByteArray(ExportHelper.ReportType.Event), "application/vnd.ms-excel", fileName);
        }

        private async Task PopulateSelectsAsync()
        {
            EventTypes = await _listHelper.EventTypesSelectListAsync();
            AllowedActionsTaken = await _listHelper.AllowedActionTakenSelectListAsync(EditEvent.EventTypeId);
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync(true);
            EventContractors = await _listHelper.EventContractorListAsync();
        }
    }
}
