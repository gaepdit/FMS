using System;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class DeleteBudgetCodeModel : PageModel
    {
        [BindProperty]
        public bool Delete { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string Code { get; set; }

        [BindProperty]
        public bool ShowChange { get; set; }

        private readonly IBudgetCodeRepository _budgetCodeRepository;
        public DeleteBudgetCodeModel(IBudgetCodeRepository budgetCodeRepository) => _budgetCodeRepository = budgetCodeRepository;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var budgetCode = await _budgetCodeRepository.GetBudgetCodeAsync(id.Value);

            if (budgetCode == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Delete = !budgetCode.Active;
            Code = budgetCode.Code;
            ShowChange = false;
            
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Code = (await _budgetCodeRepository.GetBudgetCodeAsync(Id)).Code;
                return Page();
            }

            try
            {
                await _budgetCodeRepository.UpdateBudgetCodeStatusAsync(Id, !Delete);
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
            Code = (await _budgetCodeRepository.GetBudgetCodeAsync(Id)).Code;
            ShowChange = true;

            return Page();
        }
    }
}
