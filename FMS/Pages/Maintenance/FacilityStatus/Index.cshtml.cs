using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.FacilityStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IFacilityStatusRepository _repository;
        public IndexModel(IFacilityStatusRepository repository) => _repository = repository;

        public IReadOnlyList<FacilityStatusSummaryDto> FacilityStatuses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FacilityStatuses = await _repository.GetFacilityStatusListAsync();
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

            var facilityStatus = await _repository.GetFacilityStatusAsync(itemId.Value);

            if (facilityStatus == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateFacilityStatusStatusAsync(itemId.Value, !facilityStatus.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FacilityStatusExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            FacilityStatuses = await _repository.GetFacilityStatusListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                facilityStatus.Active
                    ? $"{MaintenanceOptions.FacilityStatus} \"{facilityStatus.Status}\" successfully removed from list."
                    : $"{MaintenanceOptions.FacilityStatus} \"{facilityStatus.Status}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}