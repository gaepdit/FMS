using FMS.Domain.Dto;
using FMS.Domain.Entities;
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
    public class IndexModel : PageModel
    {
        private readonly IUserProgramRepository _repository;
        public IndexModel(IUserProgramRepository repository) => _repository = repository;

        public IReadOnlyList<UserProgramSummaryDto> UserPrograms { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserPrograms = await _repository.GetUserProgramListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userProgram = await _repository.GetUserProgramAsync(itemId.Value);

            if (userProgram == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateUserProgramStatusAsync(itemId.Value, !userProgram.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.UserProgramExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                userProgram.Active
                    ? $"{MaintenanceOptions.UserProgram} \"{userProgram.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.UserProgram} \"{userProgram.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}