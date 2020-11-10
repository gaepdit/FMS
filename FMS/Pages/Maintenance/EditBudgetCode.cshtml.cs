using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class EditBudgetCodeModel : PageModel
    {
        [BindProperty]
        public BudgetCodeEditDto BudgetCode { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        private readonly IBudgetCodeRepository _budgetCodeRepository;
        public EditBudgetCodeModel(IBudgetCodeRepository budgetCodeRepository) => _budgetCodeRepository = budgetCodeRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            BudgetCode = await _budgetCodeRepository.GetBudgetCodeAsync(id.Value);

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
            if (await _budgetCodeRepository.BudgetCodeCodeExistsAsync(BudgetCode.Code, Id))
            {
                ModelState.AddModelError("BudgetCode.Code", "Code entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _budgetCodeRepository.UpdateBudgetCodeAsync(Id, BudgetCode);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _budgetCodeRepository.BudgetCodeExistsAsync(Id))
                {
                    return NotFound();
                }

                throw;
            }
            TempData?.SetDisplayMessage(Context.Success, $"Budget Code {BudgetCode.Code} successfully updated.");

            return RedirectToPage("./Index", "select", new {MaintenanceSelection = MaintenanceOptions.BudgetCode});
        }
    }
}
