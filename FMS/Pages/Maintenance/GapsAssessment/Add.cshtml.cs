using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace FMS.Pages.Maintenance.GapsAssessment
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IGapsAssessmentRepository _repository;
        public AddModel(IGapsAssessmentRepository repository) => _repository = repository;

        [BindProperty]
        public GapsAssessmentCreateDto GapsAssessment { get; set; }

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

            GapsAssessment.TrimAll();

            // When adding a new Action Taken, make sure the number doesn't already exist before trying to save.
            if (await _repository.GapsAssessmentNameExistsAsync(GapsAssessment.Name))
            {
                ModelState.AddModelError("GapsAssessment.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateGapsAssessmentAsync(GapsAssessment);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.GapsAssessment} \"{GapsAssessment.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.GapsAssessment });
        }
    }
}
