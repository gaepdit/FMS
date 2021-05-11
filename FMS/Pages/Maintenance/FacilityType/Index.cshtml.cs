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

namespace FMS.Pages.Maintenance.FacilityType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IFacilityTypeRepository _repository;
        public IndexModel(IFacilityTypeRepository repository) => _repository = repository;

        public IReadOnlyList<FacilityTypeSummaryDto> FacilityTypes { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FacilityTypes = await _repository.GetFacilityTypeListAsync();
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

            var facilityType = await _repository.GetFacilityTypeAsync(itemId.Value);

            if (facilityType == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateFacilityTypeStatusAsync(itemId.Value, !facilityType.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FacilityTypeExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            FacilityTypes = await _repository.GetFacilityTypeListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                facilityType.Active
                    ? $"{MaintenanceOptions.FacilityType} \"{facilityType.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.FacilityType} \"{facilityType.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}