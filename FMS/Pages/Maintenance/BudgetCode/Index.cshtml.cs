using System.Collections.Generic;
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
    }
}