using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.UserPosition
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IUserPositionRepository _repository;
        public EditModel(IUserPositionRepository repository) => _repository = repository;

        [BindProperty]
        public UserPositionEditDto UserPosition { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            UserPosition = await _repository.GetUserPositionAsync(id.Value);

            if (UserPosition == null)
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

            UserPosition.TrimAll();

            // If editing UserPosition, make sure the new Name doesn't already exist before trying to save.
            if (await _repository.UserPositionNameExistsAsync(UserPosition.Name, Id))
            {
                ModelState.AddModelError("UserPosition.Name", "Name entered already exists.");
            }

            if (await _repository.UserPositionDescriptionExistsAsync(UserPosition.Description, Id))
            {
                ModelState.AddModelError("UserPosition.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateUserPositionAsync(Id, UserPosition);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.UserPositionExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.UserPosition} \"{UserPosition.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
