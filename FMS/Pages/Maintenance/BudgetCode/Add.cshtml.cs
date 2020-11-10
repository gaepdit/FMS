using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.BudgetCode
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class AddModel : PageModel
    {
        private readonly IBudgetCodeRepository _repository;
        public AddModel(IBudgetCodeRepository repository) => _repository = repository;

        [BindProperty]
        public BudgetCodeCreateDto BudgetCode { get; set; }

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
            if (await _repository.BudgetCodeCodeExistsAsync(BudgetCode.Code))
            {
                ModelState.AddModelError("BudgetCode.Code", "Code entered already exists.");
            }

            if (await _repository.BudgetCodeNameExistsAsync(BudgetCode.Name))
            {
                ModelState.AddModelError("BudgetCode.Name", "Name entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.CreateBudgetCodeAsync(BudgetCode);

            TempData?.SetDisplayMessage(Context.Success, $"Budget Code {BudgetCode.Code} successfully created.");
            return RedirectToPage("./Index");
        }
    }
}