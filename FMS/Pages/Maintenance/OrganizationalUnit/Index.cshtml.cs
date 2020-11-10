using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.OrganizationalUnit
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IOrganizationalUnitRepository _repository;
        public IndexModel(IOrganizationalUnitRepository repository) => _repository = repository;

        public IReadOnlyList<OrganizationalUnitSummaryDto> OrganizationalUnits { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            OrganizationalUnits = await _repository.GetOrganizationalUnitListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}