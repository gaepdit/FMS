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

namespace FMS.Pages.Maintenance.SoilStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly ISoilStatusRepository _repository;
        public EditModel(ISoilStatusRepository repository) => _repository = repository;

        [BindProperty]
        public SoilStatusEditDto SoilStatus { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            SoilStatus = await _repository.GetSoilStatusAsync(id.Value);

            if (SoilStatus == null)
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

            SoilStatus.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.SoilStatusNameExistsAsync(SoilStatus.Name, Id))
            {
                ModelState.AddModelError("SoilStatus.Name", "Name entered already exists.");
            }

            if (await _repository.SoilStatusDescriptionExistsAsync(SoilStatus.Description, Id))
            {
                ModelState.AddModelError("SoilStatus.Description", "Description entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateSoilStatusAsync(Id, SoilStatus);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.SoilStatusExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.SoilStatus} \"{SoilStatus.Name}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}