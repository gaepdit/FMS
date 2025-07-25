using FMS.Domain.Dto;
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
        private readonly IActionTakenRepository _actionTakenRepository;
        private readonly IEventTypeRepository _eventTypeRepository;
        public EditModel(
            IAllowedActionTakenRepository repository,
            IActionTakenRepository actionTakenRepository,
            IEventTypeRepository eventTypeRepository)
        {
            _repository = repository;
            _actionTakenRepository = actionTakenRepository;
            _eventTypeRepository = eventTypeRepository;
        }
        public IReadOnlyList<AllowedActionTakenSummaryDto> AllowedActionsTaken { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public EventTypeEditDto EventType { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            AllowedActionsTaken = await _repository.GetAllowedActionTakenListAsync(id);

            EventType = await _eventTypeRepository.GetEventTypeByIdAsync(id);

            DisplayMessage = TempData?.GetDisplayMessage();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? itemId, Guid? id)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var actionTaken = await _actionTakenRepository.GetActionTakenAsync(itemId.Value);

            var eventType = await _eventTypeRepository.GetEventTypeByIdAsync(id.Value);

            if (actionTaken == null || eventType == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateAllowedActionTakenAsync(itemId.Value, id.Value);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.AllowedActionTakenExistsAsync(itemId.Value))
                {
                    return NotFound();
                }
                throw;
            }

            var allowedActionTaken = await _repository.GetAllowedActionTakenAsync(id.Value, itemId.Value);

            TempData?.SetDisplayMessage(Context.Success,
                allowedActionTaken.Active
                    ? $"{MaintenanceOptions.ActionTaken} \"{allowedActionTaken.ActionTakenId}\" successfully removed from list."
                    : $"{MaintenanceOptions.ActionTaken} \"{allowedActionTaken.ActionTakenId}\" successfully restored.");

            return RedirectToPage("./Edit", new { id = allowedActionTaken.EventTypeId });
        }
    }
}
