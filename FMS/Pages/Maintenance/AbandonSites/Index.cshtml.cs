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

namespace FMS.Pages.Maintenance.AbandonSites
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IAbandonSitesRepository _repository;
        public IndexModel(IAbandonSitesRepository repository) => _repository = repository;

        public IReadOnlyList<AbandonSitesSummaryDto> AbandonSites { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AbandonSites = await _repository.GetAbandonSitesListAsync();
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

            var abandonSite = await _repository.GetAbandonSitesByIdAsync(itemId.Value);

            if (abandonSite == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateAbandonSitesStatusAsync(itemId.Value, !abandonSite.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.AbandonSitesExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            AbandonSites = await _repository.GetAbandonSitesListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                abandonSite.Active
                    ? $"{MaintenanceOptions.AbandonSites} \"{abandonSite.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.AbandonSites} \"{abandonSite.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
