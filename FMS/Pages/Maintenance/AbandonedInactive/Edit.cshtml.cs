using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.AbandonedInactive
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IAbandonedInactiveRepository _repository;
        public EditModel(IAbandonedInactiveRepository repository) => _repository = repository;

        [BindProperty]
        public AbandonedInactiveEditDto AbandonedInactive { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            AbandonedInactive = await _repository.GetAbandonedInactiveAsync(id.Value);

            if (AbandonedInactive == null)
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

            AbandonedInactive.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.AbandonedInactiveNameExistsAsync(AbandonedInactive.Name, Id))
            {
                ModelState.AddModelError("AbandonedInactive.Name", "Name entered already exists.");
            }

            if (await _repository.AbandonedInactiveNameExistsAsync(AbandonedInactive.Description, Id))
            {
                ModelState.AddModelError("AbandonedInactive.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateAbandonedInactiveAsync(Id, AbandonedInactive);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.AbandonedInactiveExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.AbandonedInactive} \"{AbandonedInactive.Name}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}
