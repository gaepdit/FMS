using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.ActionTaken
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IActionTakenRepository _repository;
        public IndexModel(IActionTakenRepository repository) => _repository = repository;

        public IReadOnlyList<ActionTakenSummaryDto> ActionsTaken { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ActionsTaken = await _repository.GetActionTakenListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var actionTaken = await _repository.GetActionTakenAsync(itemId.Value);

            if (actionTaken == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateActionTakenStatusAsync(itemId.Value, !actionTaken.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ActionTakenExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            ActionsTaken = await _repository.GetActionTakenListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                actionTaken.Active
                    ? $"{MaintenanceOptions.ActionTaken} \"{actionTaken.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.ActionTaken} \"{actionTaken.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
