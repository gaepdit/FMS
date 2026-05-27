using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.ParcelType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IParcelTypeRepository _repository;
        public IndexModel(IParcelTypeRepository repository) => _repository = repository;

        public IReadOnlyList<ParcelTypeSummaryDto> ParcelTypes { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ParcelTypes = await _repository.GetParcelTypeListAsync();
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

            var parcelType = await _repository.GetParcelTypeAsync(itemId.Value);

            if (parcelType == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateParcelTypeStatusAsync(itemId.Value, !parcelType.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ParcelTypeExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                parcelType.Active
                    ? $"{MaintenanceOptions.ParcelType} \"{parcelType.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.ParcelType} \"{parcelType.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
