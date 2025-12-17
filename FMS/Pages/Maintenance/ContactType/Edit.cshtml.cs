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

namespace FMS.Pages.Maintenance.ContactType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IContactTypeRepository _repository;
        public EditModel(IContactTypeRepository repository) => _repository = repository;

        [BindProperty]
        public ContactTypeEditDto ContactType { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            ContactType = await _repository.GetContactTypeByIdAsync(id.Value);

            if (ContactType == null)
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

            ContactType.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.ContactTypeNameExistsAsync(ContactType.Name))
            {
                ModelState.AddModelError("ContactType.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateContactTypeAsync(Id, ContactType);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ContactTypeExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ContactType} \"{ContactType.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
