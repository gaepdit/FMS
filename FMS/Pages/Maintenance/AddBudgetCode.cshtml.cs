using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddBudgetCodeModel : PageModel
    {
        [BindProperty]
        public BudgetCodeCreateDto BudgetCode { get; set; }

        private readonly IBudgetCodeRepository _budgetCodeRepository;

        public AddBudgetCodeModel(IBudgetCodeRepository budgetCodeRepository) =>
            _budgetCodeRepository = budgetCodeRepository;

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            BudgetCode.TrimAll();

            // When adding a new Budget Code, make sure the code and number don't already exist before trying to save.
            if (await _budgetCodeRepository.BudgetCodeCodeExistsAsync(BudgetCode.Code))
            {
                ModelState.AddModelError("BudgetCode.Code", "Code entered already exists.");
            }

            if (await _budgetCodeRepository.BudgetCodeNameExistsAsync(BudgetCode.Name))
            {
                ModelState.AddModelError("BudgetCode.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _budgetCodeRepository.CreateBudgetCodeAsync(BudgetCode);

            TempData?.SetDisplayMessage(Context.Success, $"Budget Code {BudgetCode.Code} successfully created.");

            return RedirectToPage("./Index", "select", new {MaintenanceSelection = MaintenanceOptions.BudgetCode});
        }
    }
}