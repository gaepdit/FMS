using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Maintenance.FacilityType
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IFacilityTypeRepository _repository;
        public IndexModel(IFacilityTypeRepository repository) => _repository = repository;

        public IReadOnlyList<FacilityTypeSummaryDto> FacilityTypes { get; private set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            FacilityTypes = await _repository.GetFacilityTypeListAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}