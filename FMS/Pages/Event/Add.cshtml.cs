using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Pages.Maintenance;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Event
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class AddModel : PageModel
    {
        private readonly IEventRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public AddModel(
            IEventRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public EventCreateDto NewEvent { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid? ParentEventId { get; set; } = Guid.Empty;

        public IList<EventSummaryDto> Events { get; set; }

        public SelectList EventTypes { get; private set; }
        public SelectList AllowedActionsTaken { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
        public SelectList EventContractors { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

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
            if (!ModelState.IsValid)
            {
                Facility = await _facilityRepository.GetFacilityAsync(Id);
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
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Event created successfully.");

            ActiveTab = "Events";
            return RedirectToPage("../Facilities/Details", new { id = NewEvent.FacilityId });
        }

        private async Task PopulateSelectsAsync()
        {
            EventTypes = await _listHelper.EventTypesSelectListAsync();
            AllowedActionsTaken = await _listHelper.ActionTakenSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync(true);
            EventContractors = await _listHelper.EventContractorListAsync();
        }
    }
}
