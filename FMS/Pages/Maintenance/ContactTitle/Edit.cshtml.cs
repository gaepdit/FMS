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

namespace FMS.Pages.Maintenance.ContactTitle
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IContactTitleRepository _repository;
        public EditModel(IContactTitleRepository repository) => _repository = repository;

        [BindProperty]
        public ContactTitleEditDto ContactTitle { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            ContactTitle = await _repository.GetContactTitleByIdAsync(id.Value);

            if (ContactTitle == null)
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

            ContactTitle.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.ContactTitleNameExistsAsync(ContactTitle.Name))
            {
                ModelState.AddModelError("ContactTitle.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateContactTitleAsync(Id, ContactTitle);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ContactTitleExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ContactTitle} \"{ContactTitle.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
