using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.OverallStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IOverallStatusRepository _repository;
        public AddModel(IOverallStatusRepository repository) => _repository = repository;

        [BindProperty]
        public OverallStatusCreateDto OverallStatus { get; set; }

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

            OverallStatus.TrimAll();

            if (await _repository.OverallStatusNameExistsAsync(OverallStatus.Name))
            {
                ModelState.AddModelError("OverallStatus.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateOverallStatusAsync(OverallStatus);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.OverallStatus} \"{OverallStatus.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.OverallStatus });
        }
    }
}
