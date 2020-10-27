using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditOrganizationalUnitModel : PageModel
    {
        [BindProperty]
        public OrganizationalUnitEditDto OrganizationalUnit { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;
        public EditOrganizationalUnitModel(IOrganizationalUnitRepository organizationalUnitRepository) => _organizationalUnitRepository = organizationalUnitRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            OrganizationalUnit = await _organizationalUnitRepository.GetOrganizationalUnitAsync(id.Value);

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
            if (await _organizationalUnitRepository.OrganizationalUnitNameExistsAsync(OrganizationalUnit.Name, Id))
            {
                ModelState.AddModelError("OrganizationalUnit.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _organizationalUnitRepository.UpdateOrganizationalUnitAsync(Id, OrganizationalUnit);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _organizationalUnitRepository.OrganizationalUnitExistsAsync(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData?.SetDisplayMessage(Context.Success, $"Organizational Unit {OrganizationalUnit.Name} successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
