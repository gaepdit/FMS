using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.EventType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IEventTypeRepository _repository;
        public AddModel(IEventTypeRepository repository) => _repository = repository;

        [BindProperty]
        public EventTypeCreateDto EventType { get; set; }

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

            EventType.TrimAll();

            if (await _repository.EventTypeNameExistsAsync(EventType.Name))
            {
                ModelState.AddModelError("EventType.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateEventTypeAsync(EventType);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.EventType} \"{EventType.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.EventType });
        }
    }
}
