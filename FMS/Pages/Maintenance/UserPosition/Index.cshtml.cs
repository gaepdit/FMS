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
    public class IndexModel : PageModel
    {
        private readonly IUserPositionRepository _repository;
        public IndexModel(IUserPositionRepository repository) => _repository = repository;

        public IReadOnlyList<UserPositionSummaryDto> UserPositions { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            UserPositions = await _repository.GetUserPositionListAsync();
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

            var userPosition = await _repository.GetUserPositionAsync(itemId.Value);

            if (userPosition == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateUserPositionStatusAsync(itemId.Value, !userPosition.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.UserPositionExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                userPosition.Active
                    ? $"{MaintenanceOptions.UserPosition} \"{userPosition.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.UserPosition} \"{userPosition.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
