using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.ActionTaken
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IActionTakenRepository _repository;
        public AddModel(IActionTakenRepository repository) => _repository = repository;

        [BindProperty]
        public ActionTakenCreateDto ActionTaken { get; set; }

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

            ActionTaken.TrimAll();

            // When adding a new Action Taken, make sure the number doesn't already exist before trying to save.
            if (await _repository.ActionTakenNameExistsAsync(ActionTaken.Name))
            {
                ModelState.AddModelError("ActionTaken.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateActionTakenAsync(ActionTaken);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ActionTaken} \"{ActionTaken.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.ActionTaken });
        }
    }
}
