using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.FacilityStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IFacilityStatusRepository _repository;
        public AddModel(IFacilityStatusRepository repository) => _repository = repository;

        [BindProperty]
        public FacilityStatusCreateDto FacilityStatus { get; set; }

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
            if (await _repository.FacilityStatusStatusExistsAsync(FacilityStatus.Status))
            {
                ModelState.AddModelError("FacilityStatus.Status", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateFacilityStatusAsync(FacilityStatus);

            TempData?.SetDisplayMessage(Context.Success,
                $"Facility Status {FacilityStatus.Status} successfully created.");

            return RedirectToPage("./Index");
        }
    }
}