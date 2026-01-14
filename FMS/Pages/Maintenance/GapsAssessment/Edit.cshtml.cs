using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.GapsAssessment
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IGapsAssessmentRepository _repository;
        public EditModel(IGapsAssessmentRepository repository) => _repository = repository;

        [BindProperty]
        public GapsAssessmentEditDto GapsAssessment { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            GapsAssessment = await _repository.GetGapsAssessmentByIdAsync(id.Value);

            if (GapsAssessment == null)
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

            GapsAssessment.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.GapsAssessmentNameExistsAsync(GapsAssessment.Name, Id))
            {
                ModelState.AddModelError("GapsAssessment.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateGapsAssessmentAsync(Id, GapsAssessment);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.GapsAssessmentExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.GapsAssessment} \"{GapsAssessment.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
