using FMS.Domain.Entities;
using FMS.Domain.Services;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ISelectListHelper _listHelper;
        public IndexModel(IUserService userService, ISelectListHelper listHelper)
        {
            _userService = userService;
            _listHelper = listHelper;
        }

        public UserView CurrentUser { get; private set; }
        public IList<string> Roles { get; private set; }

        public SelectList UserUnits { get; private set; }

        public SelectList UserPrograms { get; private set; }

        public SelectList UserPositions { get; private set; }

        public DisplayMessage Message { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userService.GetCurrentUserAsync()
                ?? throw new Exception("Current user not found");
            Roles = await _userService.GetCurrentUserRolesAsync();
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<ActionResult> OnGetUserDesignationAsync()
        {
            CurrentUser = await _userService.GetCurrentUserAsync()
                ?? throw new Exception("Current user not found");
            try
            {
                await _userService.UpdateUserDesignationsAsync(CurrentUser.Id, CurrentUser.UserProgram, CurrentUser.UserUnit, CurrentUser.UserPosition);
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Unable to update user designations: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"User designations successfully updated.");
            return Page();
        }   

        private async Task PopulateSelectsAsync()
        {
            UserPrograms = await _listHelper.UserProgramsSelectListAsync();
            UserUnits = await _listHelper.OrganizationalUnitsSelectListAsync(false, ["Remedial Sites 1", "Remedial Sites 2", "Remedial Sites 3", "DOD Facilities", "NPL Unit", "Treatment & Storage", "SW Env. Monitoring Compliance", "Voluntary Remediation"]);
            UserPositions = await _listHelper.UserPositionsSelectListAsync();
        }
    }
}