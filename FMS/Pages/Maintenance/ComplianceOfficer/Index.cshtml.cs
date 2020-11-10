using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.ComplianceOfficer
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IComplianceOfficerRepository _repository;
        public IndexModel(IComplianceOfficerRepository repository) => _repository = repository;

        public IReadOnlyList<ComplianceOfficerSummaryDto> ComplianceOfficers { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ComplianceOfficers = await _repository.GetComplianceOfficerListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}