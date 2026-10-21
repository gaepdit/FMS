using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IOrganizationalUnitRepository _repository;
        private readonly ISelectListHelper _listHelper;
        public AddModel(IOrganizationalUnitRepository repository, ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public OrganizationalUnitCreateDto OrganizationalUnit { get; set; }

        public SelectList UserPrograms { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
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
                await PopulateSelectsAsync();
                return Page();
            }

            await _repository.CreateOrganizationalUnitAsync(OrganizationalUnit);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.OrganizationalUnit} \"{OrganizationalUnit.Name}\" successfully created.");
            await PopulateSelectsAsync();
            return RedirectToPage("./Index", "select",
                new {MaintenanceSelection = MaintenanceOptions.OrganizationalUnit});
        }
        private async Task PopulateSelectsAsync()
        {
            UserPrograms = await _listHelper.UserProgramsSelectListAsync();
        }
    }
}