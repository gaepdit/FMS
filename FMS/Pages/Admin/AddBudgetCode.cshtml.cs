using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddBudgetCodeModel : PageModel
    {
        [BindProperty]
        public BudgetCodeCreateDto BudgetCode { get; private set; }

        [BindProperty]
        public Guid Id { get; private set; }

        private readonly IBudgetCodeRepository _budgetCodeRepository;
        public AddBudgetCodeModel(IBudgetCodeRepository budgetCodeRepository) => _budgetCodeRepository = budgetCodeRepository;

        public IActionResult OnGet()
        {
            BudgetCode = new BudgetCodeCreateDto { Active = true };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BudgetCode.TrimAll();

            // When adding a new Budget Code, make sure the number doesn't already exist before trying to save.
            if (await _budgetCodeRepository.BudgetCodeCodeExistsAsync(BudgetCode.Code))
            {
                ModelState.AddModelError("BudgetCode.Code", "Code entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _budgetCodeRepository.CreateBudgetCodeAsync(BudgetCode);

            return RedirectToPage("./Index");
        }
    }
}
