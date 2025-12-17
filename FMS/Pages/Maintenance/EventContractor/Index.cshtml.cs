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

namespace FMS.Pages.Maintenance.EventContractor
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IEventContractorRepository _repository;
        public IndexModel(IEventContractorRepository repository) => _repository = repository;

        public IReadOnlyList<EventContractorSummaryDto> EventContractors { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            EventContractors = await _repository.GetEventContractorListAsync(false);
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

            var eventContractor = await _repository.GetEventContractorByIdAsync(itemId.Value);

            if (eventContractor == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateEventContractorStatusAsync(itemId.Value);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.EventContractorExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                eventContractor.Active
                    ? $"{MaintenanceOptions.EventContractor} \"{eventContractor.Name}\" successfully removed from list."
                    : $"{MaintenanceOptions.EventContractor} \"{eventContractor.Name}\" successfully restored.");

            return RedirectToPage("./Index");
        }

    }
}
