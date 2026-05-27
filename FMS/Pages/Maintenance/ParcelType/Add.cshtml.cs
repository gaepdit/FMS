using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.ParcelType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IParcelTypeRepository _repository;
        public AddModel(IParcelTypeRepository repository) => _repository = repository;

        [BindProperty]
        public ParcelTypeCreateDto ParcelType { get; set; }

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

            ParcelType.TrimAll();

            if (await _repository.ParcelTypeNameExistsAsync(ParcelType.Name))
            {
                ModelState.AddModelError("ParcelType.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateParcelTypeAsync(ParcelType);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ParcelType} \"{ParcelType.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.ParcelType });
        }
    }
}
