using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.SoilStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly ISoilStatusRepository _repository;
        public IndexModel(ISoilStatusRepository repository) => _repository = repository;

        public IReadOnlyList<SoilStatusSummaryDto> SoilStatuses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            SoilStatuses = await _repository.GetSoilStatusListAsync();
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

            var soilStatus = await _repository.GetSoilStatusAsync(itemId.Value);

            if (soilStatus == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateSoilStatusStatusAsync(itemId.Value, !soilStatus.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.SoilStatusExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                soilStatus.Active
                    ? $"{MaintenanceOptions.SoilStatus} \"{soilStatus.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.SoilStatus} \"{soilStatus.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
