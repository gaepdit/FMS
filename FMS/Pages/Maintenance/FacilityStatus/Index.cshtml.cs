using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.FacilityStatus
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IFacilityStatusRepository _repository;
        public IndexModel(IFacilityStatusRepository repository) => _repository = repository;

        public IReadOnlyList<FacilityStatusSummaryDto> FacilityStatuses { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FacilityStatuses = await _repository.GetFacilityStatusListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}