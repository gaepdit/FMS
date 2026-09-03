using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.EventType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditAATModel : PageModel
    {
        private readonly IAllowedActionTakenRepository _repository;
        private readonly IEventTypeRepository _eventTypeRepository;
        private readonly IAllowedActionTakenHelper _allowedActionTakenHelper;
        public EditAATModel(
            IAllowedActionTakenRepository repository,
            IEventTypeRepository eventTypeRepository,
            IAllowedActionTakenHelper allowedActionTakenHelper)
        {
            _repository = repository;
            _eventTypeRepository = eventTypeRepository;
            _allowedActionTakenHelper = allowedActionTakenHelper;
        }
        public IList<AllowedActionTakenSpec> AllowedActionsTakenList { get; set; } = new List<AllowedActionTakenSpec>();

        public DisplayMessage DisplayMessage { get; private set; }

        public string EventTypeName { get; private set; }

        [BindProperty]
        public AllowedActionTakenSpec AllowedActionTakenSpec { get; set; }

        [FromRoute]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            EventTypeName = await _eventTypeRepository.GetEventTypeNameAsync(Id);
            AllowedActionTakenSpec = new AllowedActionTakenSpec();

            if (EventTypeName == null)
            {
                return NotFound();
            }

            AllowedActionsTakenList = await _allowedActionTakenHelper.GetAllowedActionTakenListByEventIdAsync(Id);

            DisplayMessage = TempData?.GetDisplayMessage();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid actionTakenId)
        {
            EventTypeName = await _eventTypeRepository.GetEventTypeNameAsync(Id);

            AllowedActionsTakenList = await _allowedActionTakenHelper.GetAllowedActionTakenListByEventIdAsync(Id);

            if (AllowedActionsTakenList.Any(e => e.ActionTakenId == actionTakenId))
            {
                AllowedActionTakenSpec = AllowedActionsTakenList.First(e => e.ActionTakenId == actionTakenId);
            }
            else
            {
                AllowedActionTakenSpec = new AllowedActionTakenSpec()
                {
                    EventTypeId = Id,
                    ActionTakenId = actionTakenId,
                    Active = true
                };
            }

            if (!await _repository.AllowedActionTakenExistsAsync(AllowedActionTakenSpec.EventTypeId, AllowedActionTakenSpec.ActionTakenId))
            {
                if(AllowedActionTakenSpec.EventTypeId == Guid.Empty)
                {
                    AllowedActionTakenSpec.EventTypeId = Id;
                }
                await _repository.CreateAllowedActionTakenAsync(AllowedActionTakenSpec);
            }
            else
            {
                try
                {
                    await _repository.DeleteAllowedActionTakenAsync(AllowedActionTakenSpec.Id);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _repository.AllowedActionTakenExistsAsync(AllowedActionTakenSpec.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }

            AllowedActionsTakenList = await _repository.GetAllowedActionTakenListAsync(Id);

            TempData?.SetDisplayMessage(Context.Success,
                AllowedActionTakenSpec.Active
                    ? $"{MaintenanceOptions.AllowedActionTaken} \"{AllowedActionTakenSpec.ActionTakenName}\" successfully Deleted."
                    : $"{MaintenanceOptions.AllowedActionTaken} \"{AllowedActionTakenSpec.ActionTakenName}\" successfully Added.");

            return RedirectToPage("EditAAT");   //, new { id = Id }
        }
    }
}
