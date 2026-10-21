using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Event
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class AddModel : PageModel
    {
        private readonly IEventRepository _repository;
        private readonly IAllowedActionTakenRepository _allowedActionTakenRepository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly IComplianceOfficerRepository _complianceOfficerRepository;
        private readonly ISelectListHelper _listHelper;

        public AddModel(
            IEventRepository repository,
            IAllowedActionTakenRepository allowedActionTakenRepository,
            IFacilityRepository facilityRepository,
            IComplianceOfficerRepository complianceOfficerRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _allowedActionTakenRepository = allowedActionTakenRepository;
            _facilityRepository = facilityRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public EventCreateDto NewEvent { get; set; }

        public AllowedActionTakenSpec AllowedActionTakenSpec { get; set; } = new AllowedActionTakenSpec();

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid? ParentEventId { get; set; } = Guid.Empty;

        public IList<EventSummaryDto> Events { get; set; }

        public List<Guid> ComplianceOfficerGuidList { get; set; } = null;

        public SelectList AllowedEventTypes { get; private set; }
        public SelectList AllowedActionsTaken { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
        public SelectList EventContractors { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

        [TempData]
        [BindProperty]
        public EventSort SortBy { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, EventSort sortBy)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }
            Id = id.Value;
            SortBy = sortBy;

            Facility = await _facilityRepository.GetFacilityAsync(Id);

            if(Facility.OrganizationalUnit != null)
            {
                ComplianceOfficerGuidList = await _complianceOfficerRepository.GetComplianceOfficerListByProgramAsync(Facility.OrganizationalUnit.Id);
            }

            Events = await _repository.GetEventsByFacilityIdAsync(Id);

            ParentEventId ??= Guid.Empty;

            Events = EventSortHelper.SortEvents(Events, sortBy);

            NewEvent = new EventCreateDto
            {
                FacilityId = Id,
                ParentId = ParentEventId
            };

            ActiveTab = "Events";
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            AllowedActionTakenSpec = await _allowedActionTakenRepository.GetAllowedActionTakenByEventTypeAndActionTakenAsync(NewEvent.EventTypeId, NewEvent.ActionTakenId);
            if(AllowedActionTakenSpec != null)
            {
                if (NewEvent.StartDate > NewEvent.CompletionDate)
                {
                    ModelState.AddModelError("NewEvent.CompletionDate", "Start date cannot be later than Completion date.");
                }
                if (NewEvent.CompletionDate > DateOnly.FromDateTime(DateTime.Now))
                {
                    ModelState.AddModelError("NewEvent.CompletionDate", "Completion date cannot be in the future.");
                }
                if (AllowedActionTakenSpec.StartDateRequired && NewEvent.StartDate == null)
                {
                    ModelState.AddModelError("NewEvent.StartDate", "Start date is required for the selected Action Taken.");
                }
                if(AllowedActionTakenSpec.DueDateRequired && NewEvent.DueDate == null)
                {
                    ModelState.AddModelError("NewEvent.DueDate", "Due date is required for the selected Action Taken.");
                }
                if(AllowedActionTakenSpec.CompletionDateRequired && NewEvent.CompletionDate == null)
                {
                    ModelState.AddModelError("NewEvent.CompletionDate", "Completion date is required for the selected Action Taken.");
                }
            }

            if (!ModelState.IsValid)
            {
                Facility = await _facilityRepository.GetFacilityAsync(Id);
                if (Facility.OrganizationalUnit != null)
                {
                    ComplianceOfficerGuidList = await _complianceOfficerRepository.GetComplianceOfficerListByProgramAsync(Facility.OrganizationalUnit.Id);
                }
                Events = await _repository.GetEventsByFacilityIdAsync(Id);
                Events = EventSortHelper.SortEvents(Events, SortBy);
                AllowedActionTakenSpec = await _allowedActionTakenRepository.GetAllowedActionTakenByEventTypeAndActionTakenAsync(NewEvent.EventTypeId, NewEvent.ActionTakenId);
                AllowedActionTakenSpec ??= new AllowedActionTakenSpec();
                await PopulateSelectsAsync();
                return Page();
            }
            try
            {
                NewEvent.ParentId = ParentEventId;
                await _repository.CreateEventAsync(NewEvent);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the event.");
                Facility = await _facilityRepository.GetFacilityAsync(Id);
                if (Facility.OrganizationalUnit != null)
                {
                    ComplianceOfficerGuidList = await _complianceOfficerRepository.GetComplianceOfficerListByProgramAsync(Facility.OrganizationalUnit.Id);
                }
                Events = await _repository.GetEventsByFacilityIdAsync(Id);
                Events = EventSortHelper.SortEvents(Events, SortBy);
                AllowedActionTakenSpec = await _allowedActionTakenRepository.GetAllowedActionTakenByEventTypeAndActionTakenAsync(NewEvent.EventTypeId, NewEvent.ActionTakenId);
                AllowedActionTakenSpec ??= new AllowedActionTakenSpec();
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Event created successfully.");

            ActiveTab = "Events";
            return RedirectToPage("../Facilities/Details", null, new { id = NewEvent.FacilityId, tab = ActiveTab, sortBy = SortBy }, fragment: "TabPages");
        }

        private async Task PopulateSelectsAsync()
        {
            AllowedEventTypes = await _listHelper.AllowedEventTypesSelectListAsync(Facility.FacilityType.Id);
            AllowedActionsTaken = await _listHelper.ActionTakenSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync(false, ComplianceOfficerGuidList?.Count > 0 ? ComplianceOfficerGuidList : null);
            EventContractors = await _listHelper.EventContractorListAsync();
        }
    }
}
