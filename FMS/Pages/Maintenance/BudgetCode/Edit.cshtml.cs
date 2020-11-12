using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.BudgetCode
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IBudgetCodeRepository _repository;
        public EditModel(IBudgetCodeRepository repository) => _repository = repository;

        [BindProperty]
        public BudgetCodeEditDto BudgetCode { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            BudgetCode = await _repository.GetBudgetCodeAsync(id.Value);

            if (BudgetCode == null)
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

            BudgetCode.TrimAll();

            // If editing Code, make sure the new Code doesn't already exist before trying to save.
            if (await _repository.BudgetCodeCodeExistsAsync(BudgetCode.Code, Id))
            {
                ModelState.AddModelError("BudgetCode.Code", "Code entered already exists.");
            }

            if (await _repository.BudgetCodeNameExistsAsync(BudgetCode.Name, Id))
            {
                ModelState.AddModelError("BudgetCode.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _repository.UpdateBudgetCodeAsync(Id, BudgetCode);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.BudgetCodeExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, $"{MaintenanceOptions.BudgetCode} \"{BudgetCode.Code}\" successfully updated.");
            return RedirectToPage("./Index");
        }
    }
}