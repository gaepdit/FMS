using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.AbandonSites
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IAbandonSitesRepository _repository;
        public AddModel(IAbandonSitesRepository repository) => _repository = repository;

        [BindProperty]
        public AbandonSitesCreateDto AbandonSites { get; set; }

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            AbandonSites.TrimAll();

            // When adding a new Action Taken, make sure the number doesn't already exist before trying to save.
            if (await _repository.AbandonSitesNameExistsAsync(AbandonSites.Name))
            {
                ModelState.AddModelError("AbandonSites.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateAbandonSitesAsync(AbandonSites);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.AbandonSites} \"{AbandonSites.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.AbandonSites });
        }
    }
}
