using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IOrganizationalUnitRepository _repository;
        public AddModel(IOrganizationalUnitRepository repository) => _repository = repository;

        [BindProperty]
        public OrganizationalUnitCreateDto OrganizationalUnit { get; set; }

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

            OrganizationalUnit.TrimAll();

            // When adding a new Org, make sure the number doesn't already exist before trying to save.
            if (await _repository.OrganizationalUnitNameExistsAsync(OrganizationalUnit.Name))
            {
                ModelState.AddModelError("OrganizationalUnit.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateOrganizationalUnitAsync(OrganizationalUnit);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.OrganizationalUnit} \"{OrganizationalUnit.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new {MaintenanceSelection = MaintenanceOptions.OrganizationalUnit});
        }
    }
}