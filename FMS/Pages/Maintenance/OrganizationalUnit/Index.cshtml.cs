using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IOrganizationalUnitRepository _repository;
        public IndexModel(IOrganizationalUnitRepository repository) => _repository = repository;

        public IReadOnlyList<OrganizationalUnitSummaryDto> OrganizationalUnits { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            OrganizationalUnits = await _repository.GetOrganizationalUnitListAsync();
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

            var organizationalUnit = await _repository.GetOrganizationalUnitAsync(itemId.Value);

            if (organizationalUnit == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateOrganizationalUnitStatusAsync(itemId.Value, !organizationalUnit.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.OrganizationalUnitExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            OrganizationalUnits = await _repository.GetOrganizationalUnitListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                organizationalUnit.Active
                    ? $"{MaintenanceOptions.OrganizationalUnit} \"{organizationalUnit.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.OrganizationalUnit} \"{organizationalUnit.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}