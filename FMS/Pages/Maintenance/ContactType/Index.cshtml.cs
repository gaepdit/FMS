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
    public class IndexModel : PageModel
    {
        private readonly IContactTypeRepository _repository;
        public IndexModel(IContactTypeRepository repository) => _repository = repository;

        public IReadOnlyList<ContactTypeSummaryDto> ContactTypes { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ContactTypes = await _repository.GetContactTypeListAsync();
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

            var contactType = await _repository.GetContactTypeByIdAsync(itemId.Value);

            if (contactType == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateContactTypeStatusAsync(itemId.Value, !contactType.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ContactTypeExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            ContactTypes = await _repository.GetContactTypeListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                contactType.Active
                    ? $"{MaintenanceOptions.ContactType} \"{contactType.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.ContactType} \"{contactType.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}
