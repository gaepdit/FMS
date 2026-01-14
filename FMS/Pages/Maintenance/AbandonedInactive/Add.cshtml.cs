using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.AbandonedInactive
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IAbandonedInactiveRepository _repository;
        public AddModel(IAbandonedInactiveRepository repository) => _repository = repository;

        [BindProperty]
        public AbandonedInactiveCreateDto AbandonedInactive { get; set; }

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

            AbandonedInactive.TrimAll();

            if (await _repository.AbandonedInactiveNameExistsAsync(AbandonedInactive.Name))
            {
                ModelState.AddModelError("AbandonedInactive.Name", "Name entered already exists.");
            }

            if (await _repository.AbandonedInactiveDescriptionExistsAsync(AbandonedInactive.Description))
            {
                ModelState.AddModelError("AbandonedInactive.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateAbandonedInactiveAsync(AbandonedInactive);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.AbandonedInactive} \"{AbandonedInactive.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.AbandonedInactive });
        }
    }
}
