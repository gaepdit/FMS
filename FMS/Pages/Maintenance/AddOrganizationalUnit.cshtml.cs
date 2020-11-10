using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddOrganizationalUnitModel : PageModel
    {
        [BindProperty]
        public OrganizationalUnitCreateDto OrganizationalUnit { get; set; }

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        public AddOrganizationalUnitModel(IOrganizationalUnitRepository organizationalUnitRepository) =>
            _organizationalUnitRepository = organizationalUnitRepository;

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
            if (await _organizationalUnitRepository.OrganizationalUnitNameExistsAsync(OrganizationalUnit.Name))
            {
                ModelState.AddModelError("OrganizationalUnit.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _organizationalUnitRepository.CreateOrganizationalUnitAsync(OrganizationalUnit);

            TempData?.SetDisplayMessage(Context.Success,
                $"Organizational Unit {OrganizationalUnit.Name} successfully created.");

            return RedirectToPage("./Index", "select",
                new {MaintenanceSelection = MaintenanceOptions.OrganizationalUnit});
        }
    }
}