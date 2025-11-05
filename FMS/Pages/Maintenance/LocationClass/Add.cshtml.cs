using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.LocationClass
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly ILocationClassRepository _repository;
        public AddModel(ILocationClassRepository repository) => _repository = repository;

        [BindProperty]
        public LocationClassCreateDto LocationClass { get; set; }

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

            LocationClass.TrimAll();

            if (await _repository.LocationClassNameExistsAsync(LocationClass.Name))
            {
                ModelState.AddModelError("LocationClass.Name", "Class Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateLocationClassAsync(LocationClass);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.LocationClass} \"{LocationClass.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.LocationClass });
        }
    }
}
