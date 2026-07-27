using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.UserPosition
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IUserPositionRepository _repository;
        public AddModel(IUserPositionRepository repository) => _repository = repository;

        [BindProperty]
        public UserPositionCreateDto UserPosition { get; set; }

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

            UserPosition.TrimAll();

            if (await _repository.UserPositionNameExistsAsync(UserPosition.Name))
            {
                ModelState.AddModelError("UserPosition.Name", "Name entered already exists.");
            }

            if (await _repository.UserPositionDescriptionExistsAsync(UserPosition.Description))
            {
                ModelState.AddModelError("UserPosition.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateUserPositionAsync(UserPosition);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.UserPosition} \"{UserPosition.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.UserPosition });
        }
    }
}
