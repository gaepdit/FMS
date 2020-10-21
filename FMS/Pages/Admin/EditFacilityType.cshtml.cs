using System;
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
    public class EditFacilityTypeModel : PageModel
    {
        [BindProperty]
        public FacilityTypeEditDto FacilityType { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        private readonly IFacilityTypeRepository _facilityTypeRepository;
        public EditFacilityTypeModel(IFacilityTypeRepository facilityTypeRepository) => _facilityTypeRepository = facilityTypeRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            FacilityType = await _facilityTypeRepository.GetFacilityTypeAsync(id.Value);

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

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _facilityTypeRepository.FacilityTypeCodeExistsAsync(FacilityType.Code, Id))
            {
                ModelState.AddModelError("FacilityType.Code", "Code entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _facilityTypeRepository.UpdateFacilityTypeAsync(Id, FacilityType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _facilityTypeRepository.FacilityTypeExistsAsync(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            TempData?.SetDisplayMessage(Context.Success, $"FacilityType {FacilityType.Name} successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
