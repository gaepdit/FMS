using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.SourceStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly ISourceStatusRepository _repository;
        public AddModel(ISourceStatusRepository repository) => _repository = repository;

        [BindProperty]
        public SourceStatusCreateDto SourceStatus { get; set; }

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

            SourceStatus.TrimAll();

            if (await _repository.SourceStatusNameExistsAsync(SourceStatus.Name))
            {
                ModelState.AddModelError("SourceStatus.Name", "Name entered already exists.");
            }

            if (await _repository.SourceStatusDescriptionExistsAsync(SourceStatus.Description))
            {
                ModelState.AddModelError("SourceStatus.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateSourceStatusAsync(SourceStatus);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.SourceStatus} \"{SourceStatus.Name}\" successfully created.");

            return RedirectToPage("./Index",
                new { MaintenanceSelection = MaintenanceOptions.SourceStatus });
        }
    }
}
