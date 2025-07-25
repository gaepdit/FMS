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

namespace FMS.Pages.Maintenance.AllowedActionTaken
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IEventTypeRepository _repository;
        public IndexModel(IEventTypeRepository repository) => _repository = repository;
        public IReadOnlyList<EventTypeSummaryDto> EventTypes { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            EventTypes = await _repository.GetEventTypeListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        //public async Task<IActionResult> OnPostAsync(Guid? itemId)
        //{
        //    if (itemId == null)
        //    {
        //        return BadRequest();
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    var eventType = await _repository.GetEventTypeByIdAsync(itemId.Value);

        //    if (eventType == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        await _repository.UpdateEventTypeStatusAsync(itemId.Value, !eventType.Active);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!await _repository.EventTypeExistsAsync(itemId.Value))
        //        {
        //            return NotFound();
        //        }

        //        throw;
        //    }

        //    EventTypes = await _repository.GetEventTypeListAsync();

        //    TempData?.SetDisplayMessage(Context.Success,
        //        eventType.Active
        //            ? $"{MaintenanceOptions.EventType} \"{eventType.Name}\" successfully removed from list."
        //            : $"{MaintenanceOptions.EventType} \"{eventType.Name}\" successfully restored.");

        //    return RedirectToPage("./Index");
        //}
    }
}
