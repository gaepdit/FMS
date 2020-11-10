using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.FacilityType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IFacilityTypeRepository _repository;
        public EditModel(IFacilityTypeRepository repository) => _repository = repository;

        [BindProperty]
        public FacilityTypeEditDto FacilityType { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            FacilityType = await _repository.GetFacilityTypeAsync(id.Value);

            if (FacilityType == null)
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

            FacilityType.TrimAll();

            // If editing Code and Description, make sure the new Code and Description don't already exist
            // before trying to save.
            if (await _repository.FacilityTypeNameExistsAsync(FacilityType.Name, Id))
            {
                ModelState.AddModelError("FacilityType.Name", "Code entered already exists.");
            }

            if (await _repository.FacilityTypeDescriptionExistsAsync(FacilityType.Description, Id))
            {
                ModelState.AddModelError("FacilityType.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateFacilityTypeAsync(Id, FacilityType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FacilityTypeExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.FacilityType} '{FacilityType.Name}' successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}