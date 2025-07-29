using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Maintenance.AllowedActionTaken
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IAllowedActionTakenRepository _repository;
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IAllowedActionTakenHelper _allowedActionTakenHelper;
        public EditModel(
            IAllowedActionTakenRepository repository,
            IEventTypeRepository eventTypeRepository,
            IAllowedActionTakenHelper allowedActionTakenHelper)
        {
            _repository = repository;
            _eventTypeRepository = eventTypeRepository;
            _allowedActionTakenHelper = allowedActionTakenHelper;
        }
        public IList<AllowedActionTakenSpec> AllowedActionsTakenList { get; set; }

        public DisplayMessage DisplayMessage { get; private set; }

        public EventTypeEditDto EventType { get; private set; }

        [BindProperty]
        public AllowedActionTakenSpec AllowedActionTakenSpec { get; set; }

        [BindProperty]
        [HiddenInput]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            EventType = await _eventTypeRepository.GetEventTypeByIdAsync(id.Value);
            AllowedActionTakenSpec = new AllowedActionTakenSpec();

            if (EventType == null)
            {
                return NotFound();
            }

            AllowedActionsTakenList = await _allowedActionTakenHelper.GetAllowedActionTakenListByEventIdAsync(EventType.Id);

            DisplayMessage = TempData?.GetDisplayMessage();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        { 
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Id = AllowedActionTakenSpec.EventTypeId;

            if (!await _repository.AllowedActionTakenExistsAsync(AllowedActionTakenSpec.Id))
            {
                await _repository.CreateAllowedActionTakenAsync(AllowedActionTakenSpec);
            }
            else
            {
                try
                {
                    await _repository.DeleteAllowedActionTakenAsync(AllowedActionTakenSpec.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.AllowedActionTakenExistsAsync(AllowedActionTakenSpec.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
                
            AllowedActionsTakenList = await _repository.GetAllowedActionTakenListAsync(Id);

            TempData?.SetDisplayMessage(Context.Success,
                AllowedActionTakenSpec.Active
                    ? $"{MaintenanceOptions.AllowedActionTaken} \"{AllowedActionTakenSpec.ActionTakenName}\" successfully Deleted."
                    : $"{MaintenanceOptions.AllowedActionTaken} \"{AllowedActionTakenSpec.ActionTakenName}\" successfully Added.");

            return RedirectToPage("Edit", new { id = Id });
        }
    }
}
