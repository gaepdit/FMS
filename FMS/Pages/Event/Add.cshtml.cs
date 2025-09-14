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
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
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
        public EventCreateDto NewEvent { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid? ParentEventId { get; set; } = Guid.Empty;

        public IEnumerable<EventSummaryDto> Events { get; set; }

        public SelectList EventTypes { get; private set; }
        public SelectList ActionsTaken { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, Guid? parentId)
        {
            if (id == null || id == Guid.Empty)
            {
                return NotFound();
            }

            NewEvent = new EventCreateDto
            {
                FacilityId = id.Value,
                ParentId = parentId,
            };

            Facility = await _facilityRepository.GetFacilityAsync(id.Value); 

            Events = (parentId.HasValue
                ? await _repository.GetEventsByFacilityIdAndParentIdAsync(id.Value, parentId.Value)
                : await _repository.GetEventsByFacilityIdAsync(id.Value));

            ActiveTab = "Events";
            await PopulateSelectsAsync();
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
                NewEvent.ParentId = ParentEventId;
                await _repository.CreateEventAsync(NewEvent);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the event.");
                
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
            ActionsTaken = await _listHelper.ActionTakenSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
        }
    }
}
