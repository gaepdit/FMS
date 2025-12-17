using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.AbandonedInactive
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IAbandonedInactiveRepository _repository;
        public IndexModel(IAbandonedInactiveRepository repository) => _repository = repository;

        public IReadOnlyList<AbandonedInactiveSummaryDto> AbandonedInactives { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AbandonedInactives = await _repository.GetAbandonedInactiveListAsync();
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

            var abandonedInactive = await _repository.GetAbandonedInactiveAsync(itemId.Value);

            if (abandonedInactive == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateAbandonedInactiveStatusAsync(itemId.Value, !abandonedInactive.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.AbandonedInactiveExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                abandonedInactive.Active
                    ? $"{MaintenanceOptions.AbandonedInactive} \"{abandonedInactive.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.AbandonedInactive} \"{abandonedInactive.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
