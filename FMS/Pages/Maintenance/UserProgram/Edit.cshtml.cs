using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.UserProgram
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IUserProgramRepository _repository;
        public EditModel(IUserProgramRepository repository) => _repository = repository;

        [BindProperty]
        public UserProgramEditDto UserProgram { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            UserProgram = await _repository.GetUserProgramAsync(id.Value);

            if (UserProgram == null)
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

            UserProgram.TrimAll();

            // If editing UserProgram, make sure the new Name doesn't already exist before trying to save.
            if (await _repository.UserProgramNameExistsAsync(UserProgram.Name, Id))
            {
                ModelState.AddModelError("UserProgram.Name", "Name entered already exists.");
            }

            if (await _repository.UserProgramDescriptionExistsAsync(UserProgram.Description, Id))
            {
                ModelState.AddModelError("UserProgram.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateUserProgramAsync(Id, UserProgram);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.UserProgramExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.UserProgram} \"{UserProgram.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
