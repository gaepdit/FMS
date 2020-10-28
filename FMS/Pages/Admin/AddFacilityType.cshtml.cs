using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddFacilityTypeModel : PageModel
    {
        [BindProperty]
        public FacilityTypeCreateDto FacilityType { get; set; }

        private readonly IFacilityTypeRepository _facilityTypeRepository;
        public AddFacilityTypeModel(IFacilityTypeRepository facilityTypeRepository) => _facilityTypeRepository = facilityTypeRepository;

        public IActionResult OnGet()
        {
            FacilityType = new FacilityTypeCreateDto();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            FacilityType.TrimAll();

            // When adding a new Facility Type, make sure the Code and Description don't already exist
            // before trying to save.
            if (await _facilityTypeRepository.FacilityTypeNameExistsAsync(FacilityType.Name))
            {
                ModelState.AddModelError("FacilityType.Name", "Code entered already exists.");
            }

            if (await _facilityTypeRepository.FacilityTypeDescriptionExistsAsync(FacilityType.Description))
            {
                ModelState.AddModelError("FacilityType.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _facilityTypeRepository.CreateFacilityTypeAsync(FacilityType);

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.FacilityType} '{FacilityType.Name}' successfully created.");

            return RedirectToPage("./Index", "select", new {MaintenanceSelection = MaintenanceOptions.FacilityType});
        }
    }
}
