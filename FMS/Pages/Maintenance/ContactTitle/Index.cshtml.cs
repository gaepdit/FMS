using System;
using System.Collections.Generic;
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
    public class IndexModel : PageModel
    {
        private readonly IContactTitleRepository _repository;
        public IndexModel(IContactTitleRepository repository) => _repository = repository;

        public IReadOnlyList<ContactTitleSummaryDto> ContactTitles { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ContactTitles = await _repository.GetContactTitleListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var contactTitle = await _repository.GetContactTitleByIdAsync(itemId.Value);

            if (contactTitle == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateContactTitleStatusAsync(itemId.Value, !contactTitle.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ContactTitleExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            ContactTitles = await _repository.GetContactTitleListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                contactTitle.Active
                    ? $"{MaintenanceOptions.ContactTitle} \"{contactTitle.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.ContactTitle}  \" {contactTitle.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
