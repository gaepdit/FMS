using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IOrganizationalUnitRepository _repository;
        public EditModel(IOrganizationalUnitRepository repository) => _repository = repository;

        [BindProperty]
        public OrganizationalUnitEditDto OrganizationalUnit { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            OrganizationalUnit = await _repository.GetOrganizationalUnitAsync(id.Value);

            if (OrganizationalUnit == null)
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

            OrganizationalUnit.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.OrganizationalUnitNameExistsAsync(OrganizationalUnit.Name, Id))
            {
                ModelState.AddModelError("OrganizationalUnit.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateOrganizationalUnitAsync(Id, OrganizationalUnit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.OrganizationalUnitExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.OrganizationalUnit} \"{OrganizationalUnit.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}