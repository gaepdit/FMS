using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FMS.Pages.Users
{
    public class IndexModel(ISelectListHelper listHelper) : PageModel
    {
        private readonly ISelectListHelper _listHelper = listHelper;

        public string Name { get; init; }

        [EmailAddress]
        public string Email { get; init; }

        public string Role { get; init; }

        public IEnumerable<SelectListItem> RoleItems { get; } =
            UserRoles.AllRoles.Select(d => new SelectListItem(UserRoles.DisplayName(d), d));

        [BindProperty]
        [Display(Name = "Program")]
        public Guid? UserProgramId { get; set; }

        [BindProperty]
        [Display(Name = "Unit")]
        public Guid? UserUnitId { get; set; }

        [BindProperty]
        [Display(Name = "Position")]
        public Guid? UserPositionId { get; set; }

        public SelectList UserUnits { get; private set; }

        public SelectList UserPrograms { get; private set; }

        public SelectList UserPositions { get; private set; }

        public bool ShowResults { get; private set; }

        public List<UserView> SearchResults { get; private set; }

        public async Task OnGet()
        {
            await PopulateSelectsAsync();
        }

        public async Task<IActionResult> OnGetSearchAsync([FromServices] IUserService userService,
            string name, string email, string role, Guid? userProgramId, Guid? userUnitId, Guid? userPositionId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SearchResults = await userService.GetUsersAsync(name, email, role, userProgramId, userUnitId, userPositionId);
            ShowResults = true;
            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            UserPrograms = await _listHelper.UserProgramsSelectListAsync();
            UserUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
            UserPositions = await _listHelper.UserPositionsSelectListAsync();
            // false, ["Remedial Sites 1", "Remedial Sites 2", "Remedial Sites 3", "DOD Facilities", "NPL Unit", "Treatment & Storage", "SW Env. Monitoring Compliance", "Voluntary Remediation"]
        }
    }
}