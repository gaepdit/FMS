using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IOrganizationalUnitRepository _repository;
        private readonly ISelectListHelper _listHelper;
        public EditModel(IOrganizationalUnitRepository repository, ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public OrganizationalUnitEditDto OrganizationalUnit { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        [Display(Name = "Program")]
        public Guid? UserProgramId { get; set; }

        public SelectList UserPrograms { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                await PopulateSelectsAsync();
                return NotFound();
            }

            Id = id.Value;
            OrganizationalUnit = await _repository.GetOrganizationalUnitAsync(id.Value);
            UserProgramId = OrganizationalUnit?.UserProgram?.Id;

            if (OrganizationalUnit == null)
            {
                await PopulateSelectsAsync();
                return NotFound();
            }
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

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.OrganizationalUnitNameExistsAsync(OrganizationalUnit.Name, Id))
            {
                ModelState.AddModelError("OrganizationalUnit.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            try
            {
                await _repository.UpdateOrganizationalUnitAsync(Id, OrganizationalUnit, UserProgramId);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.OrganizationalUnitExistsAsync(Id))
                {
                    await PopulateSelectsAsync();
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.OrganizationalUnit} \"{OrganizationalUnit.Name}\" successfully updated.");
            await PopulateSelectsAsync();
            return RedirectToPage("./Index");
        }

        private async Task PopulateSelectsAsync()
        {
            UserPrograms = await _listHelper.UserProgramsSelectListAsync();
        }
    }
}