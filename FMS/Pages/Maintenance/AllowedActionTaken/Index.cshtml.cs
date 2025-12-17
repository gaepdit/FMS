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
    }
}
