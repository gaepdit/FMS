using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.OverallStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IOverallStatusRepository _repository;
        public IndexModel(IOverallStatusRepository repository) => _repository = repository;

        public IReadOnlyList<OverallStatusSummaryDto> OverallStatuses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            OverallStatuses = await _repository.GetOverallStatusListAsync();
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

            var overallStatus = await _repository.GetOverallStatusAsync(itemId.Value);

            if (overallStatus == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateOverallStatusStatusAsync(itemId.Value, !overallStatus.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.OverallStatusExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                overallStatus.Active
                    ? $"{MaintenanceOptions.OverallStatus} \"{overallStatus.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.OverallStatus} \"{overallStatus.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
