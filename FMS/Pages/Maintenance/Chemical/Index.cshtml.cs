using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.Chemical
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IChemicalRepository _repository;
        public IndexModel(IChemicalRepository repository) => _repository = repository;

        public IReadOnlyList<ChemicalSummaryDto> Chemicals { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Chemicals = await _repository.GetChemicalListAsync();
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

            var chemical = await _repository.GetChemicalByIdAsync(itemId.Value);

            if (chemical == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateChemicalStatusAsync(itemId.Value, !chemical.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ChemicalExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            Chemicals = await _repository.GetChemicalListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                chemical.Active
                    ? $"{MaintenanceOptions.Chemical} \"{chemical.ChemicalName}\" successfully removed from list."
                    : $"{MaintenanceOptions.Chemical} \"{chemical.ChemicalName}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}