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

namespace FMS.Pages.Maintenance.LocationClass
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly ILocationClassRepository _repository;
        public EditModel(ILocationClassRepository repository) => _repository = repository;

        [BindProperty]
        public LocationClassEditDto LocationClass { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            LocationClass = await _repository.GetLocationClassAsync(id.Value);

            if (LocationClass == null)
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

            LocationClass.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.LocationClassNameExistsAsync(LocationClass.Name, Id))
            {
                ModelState.AddModelError("LocationClass.Name", "Class Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateLocationClassAsync(Id, LocationClass);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.LocationClassExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.LocationClass} \"{LocationClass.Name}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}
