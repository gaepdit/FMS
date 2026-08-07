using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.EventType
{
    public class EditAATReqModel : PageModel
    {
        private readonly IAllowedActionTakenRepository _repository;

        public EditAATReqModel(IAllowedActionTakenRepository repository) => _repository = repository;

        public string ActionTakenName { get; private set; }

        public string EventTypeName { get; private set; }

        [BindProperty]
        public AllowedActionTakenSpec AllowedActionTakenSpec { get; set; }

        [BindProperty]
        public AllowedActionTakenSpec AllowedActionTaken { get; set; }

        [FromRoute]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            AllowedActionTakenSpec = await _repository.GetAllowedActionTakenByAATIdAsync(Id);
            ActionTakenName = AllowedActionTakenSpec.ActionTakenName;
            EventTypeName = AllowedActionTakenSpec.EventTypeName;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                AllowedActionTaken = await _repository.GetAllowedActionTakenByAATIdAsync(Id);
                ActionTakenName = AllowedActionTakenSpec.ActionTakenName;
                EventTypeName = AllowedActionTakenSpec.EventTypeName;

                // Update the properties of the allowedActionTaken entity
                AllowedActionTaken.StartDateRequired = AllowedActionTakenSpec.StartDateRequired;
                AllowedActionTaken.DueDateRequired = AllowedActionTakenSpec.DueDateRequired;
                AllowedActionTaken.CompletionDateRequired = AllowedActionTakenSpec.CompletionDateRequired;

                // Save changes to the database
                await _repository.UpdateAllowedActionTakenAsync(AllowedActionTaken);

                TempData?.SetDisplayMessage(Context.Success,
                    $"Allowed Action Taken \"{ActionTakenName}\" date requirements successfully updated for Event Type \"{EventTypeName}\".");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.AllowedActionTakenExistsAsync(Id))
                {
                    TempData?.SetDisplayMessage(Context.Danger,
                     $"Allowed Action Taken \"{ActionTakenName}\" could not be updated for Event Type \"{EventTypeName}\" due to a concurrency issue.");
                }
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger,
                    $"An error occurred while updating Allowed Action Taken \"{ActionTakenName}\" date requirements for Event Type \"{EventTypeName}\".");
            }

            return RedirectToPage("EditAAT", new { id = AllowedActionTakenSpec.EventTypeId });
        }

    }
}
