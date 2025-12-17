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

namespace FMS.Pages.Maintenance.ActionTaken
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IActionTakenRepository _repository;
        public EditModel(IActionTakenRepository repository) => _repository = repository;

        [BindProperty]
        public ActionTakenEditDto ActionTaken { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            ActionTaken = await _repository.GetActionTakenAsync(id.Value);

            if (ActionTaken == null)
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

            ActionTaken.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.ActionTakenNameExistsAsync(ActionTaken.Name, Id))
            {
                ModelState.AddModelError("ActionTaken.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateActionTakenAsync(Id, ActionTaken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.ActionTakenExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success,
                $"{MaintenanceOptions.ActionTaken} \"{ActionTaken.Name}\" successfully updated.");

            return RedirectToPage("./Index");
        }
    }
}
