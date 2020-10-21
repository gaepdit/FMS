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
            FacilityType = new FacilityTypeCreateDto { Active = true };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            FacilityType.TrimAll();

            // When adding a new Budget Code, make sure the number doesn't already exist before trying to save.
            if (await _facilityTypeRepository.FacilityTypeCodeExistsAsync(FacilityType.Code))
            {
                ModelState.AddModelError("FacilityType.Code", "Code entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _facilityTypeRepository.CreateFacilityTypeAsync(FacilityType);

            TempData?.SetDisplayMessage(Context.Success, $"Facility Type {FacilityType.Code} successfully created.");

            return RedirectToPage("./Index");
        }
    }
}
