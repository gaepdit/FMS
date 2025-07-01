using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.FundingSource
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IFundingSourceRepository _repository;
        public AddModel(IFundingSourceRepository repository) => _repository = repository;

        [BindProperty]
        public FundingSourceCreateDto FundingSource { get; set; }

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            FundingSource.TrimAll();

            if (await _repository.FundingSourceNameExistsAsync(FundingSource.Name))
            {
                ModelState.AddModelError("FundingSource.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateFundingSourceAsync(FundingSource);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.FundingSource} \"{FundingSource.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.FundingSource });
        }
    }
}
