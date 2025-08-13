using System;
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
    public class EditModel : PageModel
    {
        private readonly IAbandonSitesRepository _repository;
        public EditModel(IAbandonSitesRepository repository) => _repository = repository;

        [BindProperty]
        public AbandonSitesEditDto AbandonSites { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            AbandonSites = await _repository.GetAbandonSitesByIdAsync(id.Value);

            if (AbandonSites == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AbandonSites.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.AbandonSitesNameExistsAsync(AbandonSites.Name, Id))
            {
                ModelState.AddModelError("AbandonSites.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateAbandonSitesAsync(Id, AbandonSites);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.AbandonSitesExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.AbandonSites} \"{AbandonSites.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
