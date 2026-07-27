using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.UserProgram
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IUserProgramRepository _repository;
        public AddModel(IUserProgramRepository repository) => _repository = repository;

        [BindProperty]
        public UserProgramCreateDto UserProgram { get; set; }

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

            UserProgram.TrimAll();

            if (await _repository.UserProgramNameExistsAsync(UserProgram.Name))
            {
                ModelState.AddModelError("UserProgram.Name", "Name entered already exists.");
            }

            if (await _repository.UserProgramDescriptionExistsAsync(UserProgram.Description))
            {
                ModelState.AddModelError("UserProgram.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateUserProgramAsync(UserProgram);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.UserProgram} \"{UserProgram.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.UserProgram });
        }
    }
}
