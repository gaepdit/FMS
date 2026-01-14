using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.OverallStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IOverallStatusRepository _repository;
        public EditModel(IOverallStatusRepository repository) => _repository = repository;

        [BindProperty]
        public OverallStatusEditDto OverallStatus { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            OverallStatus = await _repository.GetOverallStatusAsync(id.Value);

            if (OverallStatus == null)
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

            OverallStatus.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.OverallStatusNameExistsAsync(OverallStatus.Name, Id))
            {
                ModelState.AddModelError("OverallStatus.Name", "Name entered already exists.");
            }

            if (await _repository.OverallStatusDescriptionExistsAsync(OverallStatus.Description, Id))
            {
                ModelState.AddModelError("OverallStatus.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateOverallStatusAsync(Id, OverallStatus);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.OverallStatusExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.OverallStatus} \"{OverallStatus.Name}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}