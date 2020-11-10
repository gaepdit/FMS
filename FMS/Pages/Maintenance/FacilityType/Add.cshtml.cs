using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.FacilityType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IFacilityTypeRepository _repository;
        public AddModel(IFacilityTypeRepository repository) => _repository = repository;

        [BindProperty]
        public FacilityTypeCreateDto FacilityType { get; set; }

        public void OnGet()
        {
            // Method intentionally left empty.
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
            if (await _repository.FacilityTypeNameExistsAsync(FacilityType.Name))
            {
                ModelState.AddModelError("FacilityType.Name", "Code entered already exists.");
            }

            if (await _repository.FacilityTypeDescriptionExistsAsync(FacilityType.Description))
            {
                ModelState.AddModelError("FacilityType.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateFacilityTypeAsync(FacilityType);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.FacilityType} '{FacilityType.Name}' successfully created.");

            return RedirectToPage("./Index");
        }
    }
}