using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.GroundwaterStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IGroundwaterStatusRepository _repository;
        public IndexModel(IGroundwaterStatusRepository repository) => _repository = repository;

        public IReadOnlyList<GroundwaterStatusSummaryDto> GroundwaterStatuses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            GroundwaterStatuses = await _repository.GetGroundwaterStatusListAsync();
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

            var groundwaterStatus = await _repository.GetGroundwaterStatusAsync(itemId.Value);

            if (groundwaterStatus == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateGroundwaterStatusStatusAsync(itemId.Value, !groundwaterStatus.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.GroundwaterStatusExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                groundwaterStatus.Active
                    ? $"{MaintenanceOptions.GroundwaterStatus} \"{groundwaterStatus.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.GroundwaterStatus} \"{groundwaterStatus.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
