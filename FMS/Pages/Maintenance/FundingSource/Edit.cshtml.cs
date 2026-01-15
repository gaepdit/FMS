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
    public class EditModel : PageModel
    {
        private readonly IFundingSourceRepository _repository;
        public EditModel(IFundingSourceRepository repository) => _repository = repository;

        [BindProperty]
        public FundingSourceEditDto FundingSource { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            FundingSource = await _repository.GetFundingSourceByIdAsync(id.Value);

            if (FundingSource == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            FundingSource.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.FundingSourceNameExistsAsync(FundingSource.Name, Id))
            {
                ModelState.AddModelError("FundingSource.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateFundingSourceAsync(Id, FundingSource);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FundingSourceExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.FundingSource} \"{FundingSource.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
