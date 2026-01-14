using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.GroundwaterStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IGroundwaterStatusRepository _repository;
        public AddModel(IGroundwaterStatusRepository repository) => _repository = repository;

        [BindProperty]
        public GroundwaterStatusCreateDto GroundwaterStatus { get; set; }

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

            GroundwaterStatus.TrimAll();

            if (await _repository.GroundwaterStatusNameExistsAsync(GroundwaterStatus.Name))
            {
                ModelState.AddModelError("GroundwaterStatus.Name", "Name entered already exists.");
            }

            if (await _repository.GroundwaterStatusDescriptionExistsAsync(GroundwaterStatus.Description))
            {
                ModelState.AddModelError("GroundwaterStatus.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateGroundwaterStatusAsync(GroundwaterStatus);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.GroundwaterStatus} \"{GroundwaterStatus.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.GroundwaterStatus });
        }
    }
}
