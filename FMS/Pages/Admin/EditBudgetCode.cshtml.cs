using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Admin
{
    public class EditBudgetCodeModel : PageModel
    {
        [BindProperty]
        public BudgetCodeEditDto BudgetCode { get; private set; }

        [BindProperty]
        public Guid Id { get; private set; }

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

            // If Code is provided, make sure it exists
            if (!string.IsNullOrWhiteSpace(BudgetCode.Code) &&
                !await _budgetCodeRepository.BudgetCodeCodeExistsAsync(BudgetCode.Code))
            {
                ModelState.AddModelError("BudgetCode.Code", "Code entered does not exist.");
            }

            // If editing Code, make sure the new number doesn't already exist before trying to save.
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
                else
                {
                    throw;
                }
            }

            return Page();
        }
    }
}
