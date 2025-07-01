using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.ContactType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IContactTypeRepository _repository;
        public AddModel(IContactTypeRepository repository) => _repository = repository;

        [BindProperty]
        public ContactTypeCreateDto ContactType { get; set; }

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            ContactType.TrimAll();

            if (await _repository.ContactTypeNameExistsAsync(ContactType.Name))
            {
                ModelState.AddModelError("ContactType.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateContactTypeAsync(ContactType);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ContactType} \"{ContactType.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.ContactType });
        }
    }
}
