using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.GroundwaterStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IGroundwaterStatusRepository _repository;
        public EditModel(IGroundwaterStatusRepository repository) => _repository = repository;

        [BindProperty]
        public GroundwaterStatusEditDto GroundwaterStatus { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            GroundwaterStatus = await _repository.GetGroundwaterStatusAsync(id.Value);

            if (GroundwaterStatus == null)
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

            GroundwaterStatus.TrimAll();

            if (GroundwaterStatus.Name.Length > 20)
            {
                ModelState.AddModelError("GroundwaterStatus.Name", "Name cannot exceed 20 characters.");
            }

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.GroundwaterStatusNameExistsAsync(GroundwaterStatus.Name, Id))
            {
                ModelState.AddModelError("GroundwaterStatus.Name", "Name entered already exists.");
            }

            if (await _repository.GroundwaterStatusDescriptionExistsAsync(GroundwaterStatus.Description, Id))
            {
                ModelState.AddModelError("GroundwaterStatus.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateGroundwaterStatusAsync(Id, GroundwaterStatus);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.GroundwaterStatusExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.GroundwaterStatus} \"{GroundwaterStatus.Name}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}