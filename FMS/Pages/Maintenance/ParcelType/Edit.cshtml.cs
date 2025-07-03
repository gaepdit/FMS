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

namespace FMS.Pages.Maintenance.ParcelType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IParcelTypeRepository _repository;
        public EditModel(IParcelTypeRepository repository) => _repository = repository;

        [BindProperty]
        public ParcelTypeEditDto ParcelType { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            ParcelType = await _repository.GetParcelTypeAsync(id.Value);

            if (ParcelType == null)
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

            ParcelType.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.ParcelTypeNameExistsAsync(ParcelType.Name, Id))
            {
                ModelState.AddModelError("ParcelType.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateParcelTypeAsync(Id, ParcelType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ParcelTypeExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ParcelType} \"{ParcelType.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
