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

namespace FMS.Pages.Maintenance.LocationClass
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly ILocationClassRepository _repository;
        public IndexModel(ILocationClassRepository repository) => _repository = repository;

        public IReadOnlyList<LocationClassSummaryDto> LocationClasses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            LocationClasses = await _repository.GetLocationClassListAsync();
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

            var LocationClass = await _repository.GetLocationClassAsync(itemId.Value);

            if (LocationClass == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateLocationClassStatusAsync(itemId.Value, !LocationClass.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.LocationClassExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                LocationClass.Active
                    ? $"{MaintenanceOptions.LocationClass} \"{LocationClass.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.LocationClass} \"{LocationClass.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
