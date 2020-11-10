using System;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public bool Delete { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public bool ShowChange { get; set; }

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;
        public DeleteModel(IOrganizationalUnitRepository organizationalUnitRepository) => _organizationalUnitRepository = organizationalUnitRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var organizationalUnit = await _organizationalUnitRepository.GetOrganizationalUnitAsync(id.Value);

            if (organizationalUnit == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Delete = !organizationalUnit.Active;
            Name = organizationalUnit.Name;
            ShowChange = false;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Name = (await _organizationalUnitRepository.GetOrganizationalUnitAsync(Id)).Name;
                return Page();
            }

            try
            {
                await _organizationalUnitRepository.UpdateOrganizationalUnitStatusAsync(Id, !Delete);
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
            Name = (await _organizationalUnitRepository.GetOrganizationalUnitAsync(Id)).Name;
            ShowChange = true;

            return Page();
        }
    }
}
