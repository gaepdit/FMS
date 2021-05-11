using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FMS.Pages.Maintenance.BudgetCode
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IBudgetCodeRepository _repository;
        public IndexModel(IBudgetCodeRepository repository) => _repository = repository;

        public IReadOnlyList<BudgetCodeSummaryDto> BudgetCodes { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            BudgetCodes = await _repository.GetBudgetCodeListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? itemId)
        {
            if (itemId == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var budgetCode = await _repository.GetBudgetCodeAsync(itemId.Value);

            if (budgetCode == null)
            {
                return NotFound();
            }

            try
            {
                await _repository.UpdateBudgetCodeStatusAsync(itemId.Value, !budgetCode.Active);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.BudgetCodeExistsAsync(itemId.Value))
                {
                    return NotFound();
                }

                throw;
            }

            BudgetCodes = await _repository.GetBudgetCodeListAsync();

            TempData?.SetDisplayMessage(Context.Success,
                budgetCode.Active
                    ? $"{MaintenanceOptions.BudgetCode} \"{budgetCode.Code}\" successfully removed from list."
                    : $"{MaintenanceOptions.BudgetCode} \"{budgetCode.Code}\" successfully restored.");

            return RedirectToPage("./Index");
        }
    }
}