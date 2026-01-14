using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.FundingSource
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IFundingSourceRepository _repository;
        public IndexModel(IFundingSourceRepository repository) => _repository = repository;

        public IReadOnlyList<FundingSourceSummaryDto> FundingSources { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FundingSources = await _repository.GetFundingSourceListAsync();
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

            var fundingSource = await _repository.GetFundingSourceByIdAsync(itemId.Value);

            if (fundingSource == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateFundingSourceStatusAsync(itemId.Value, !fundingSource.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FundingSourceExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                fundingSource.Active
                    ? $"{MaintenanceOptions.FundingSource} \"{fundingSource.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.FundingSource} \"{fundingSource.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
