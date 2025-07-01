using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.ContactTitle
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IContactTitleRepository _repository;
        public AddModel(IContactTitleRepository repository) => _repository = repository;

        [BindProperty]
        public ContactTitleCreateDto ContactTitle { get; set; }

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

            ContactTitle.TrimAll();

            if (await _repository.ContactTitleNameExistsAsync(ContactTitle.Name))
            {
                ModelState.AddModelError("ContactTitle.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateContactTitleAsync(ContactTitle);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ContactTitle} \"{ContactTitle.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.ContactTitle });
        }
    }
}
