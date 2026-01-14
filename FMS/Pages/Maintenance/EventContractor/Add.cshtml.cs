using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.EventContractor
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IEventContractorRepository _repository;
        public AddModel(IEventContractorRepository repository) => _repository = repository;

        [BindProperty]
        public EventContractorCreateDto EventContractor { get; set; }

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

            EventContractor.TrimAll();

            if (await _repository.EventContractorNameExistsAsync(EventContractor.Name))
            {
                ModelState.AddModelError("EventContractor.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateEventContractorAsync(EventContractor);

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.EventContractor} \"{EventContractor.Name}\" successfully created.");

            return RedirectToPage("./Index", "select",
                new { MaintenanceSelection = MaintenanceOptions.EventContractor });
        }
    }
}
