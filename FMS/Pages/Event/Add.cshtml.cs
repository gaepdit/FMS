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
        private readonly ISelectListHelper _listHelper;

        public AddModel(
            IEventRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public EventCreateDto NewEvent { get; set; }

        public SelectList EventTypes { get; private set; }
        public SelectList ActionsTaken { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
       
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
                await _repository.CreateEventAsync(NewEvent);
                
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while creating the event.");
                
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Event created successfully.");

            return RedirectToPage("../Facilities/Details", new { id = NewEvent.FacilityId, tab = "Events" });
        }

        private async Task PopulateSelectsAsync()
        {
            EventTypes = await _listHelper.EventTypesSelectListAsync();
            ActionsTaken = await _listHelper.ActionTakenSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
        }
    }
}
