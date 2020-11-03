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
    public class AddFacilityStatusModel : PageModel
    {
        [BindProperty]
        public FacilityStatusCreateDto FacilityStatus { get; set; }

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        public AddFacilityStatusModel(IFacilityStatusRepository facilityStatusRepository) =>
            _facilityStatusRepository = facilityStatusRepository;

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

            FacilityStatus.TrimAll();

            // When adding a new Facility Status, make sure the number doesn't already exist before trying to save.
            if (await _facilityStatusRepository.FacilityStatusStatusExistsAsync(FacilityStatus.Status))
            {
                ModelState.AddModelError("FacilityStatus.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _facilityStatusRepository.CreateFacilityStatusAsync(FacilityStatus);

            TempData?.SetDisplayMessage(Context.Success,
                $"Facility Status {FacilityStatus.Status} successfully created.");

            return RedirectToPage("./Index", "select", new {MaintenanceSelection = MaintenanceOptions.FacilityStatus});
        }
    }
}