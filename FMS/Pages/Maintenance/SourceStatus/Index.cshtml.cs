using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.SourceStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly ISourceStatusRepository _repository;
        public IndexModel(ISourceStatusRepository repository) => _repository = repository;

        public IReadOnlyList<SourceStatusSummaryDto> SourceStatuses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            SourceStatuses = await _repository.GetSourceStatusListAsync();
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

            var sourceStatus = await _repository.GetSourceStatusAsync(itemId.Value);

            if (sourceStatus == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateSourceStatusStatusAsync(itemId.Value, !sourceStatus.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.SourceStatusExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                sourceStatus.Active
                    ? $"{MaintenanceOptions.SourceStatus} \"{sourceStatus.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.SourceStatus} \"{sourceStatus.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
