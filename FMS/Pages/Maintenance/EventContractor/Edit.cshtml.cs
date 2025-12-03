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

namespace FMS.Pages.Maintenance.EventContractor
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IEventContractorRepository _repository;
        public EditModel(IEventContractorRepository repository) => _repository = repository;

        [BindProperty]
        public EventContractorEditDto EventContractor { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            EventContractor = await _repository.GetEventContractorByIdAsync(id.Value);

            if (EventContractor == null)
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

            EventContractor.TrimAll();

            if (EventContractor.Name.Length > 20)
            {
                ModelState.AddModelError("EventContractor.Name", "Name cannot exceed 20 characters.");
            }

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.EventContractorNameExistsAsync(EventContractor.Name))
            {
                ModelState.AddModelError("EventContractor.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateEventContractorAsync(Id, EventContractor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.EventContractorExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.EventContractor} \"{EventContractor.Name}\" successfully updated.");
            return RedirectToPage("./Index");
        }

    }
}
