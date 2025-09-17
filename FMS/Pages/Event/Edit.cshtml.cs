using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Event
{
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
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

        [BindProperty]
        public Guid? ParentEventId { get; set; } = Guid.Empty;

        public SelectList EventTypes { get; private set; }
        public SelectList ActionsTaken { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

        [BindProperty]
        public Guid Id { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;
            EditEvent = await _repository.GetEventByIdAsync(id);
            if (EditEvent == null)
            {
                return NotFound();
            }
            Facility = await _facilityRepository.GetFacilityAsync(EditEvent.FacilityId);

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

        private async Task PopulateSelectsAsync()
        {
            EventTypes = await _listHelper.EventTypesSelectListAsync();
            ActionsTaken = await _listHelper.ActionTakenSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
        }
    }
}
