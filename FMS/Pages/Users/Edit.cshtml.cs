using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace FMS.Pages.Users
{
    [Authorize(Roles = UserRoles.UserMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly ISelectListHelper _listHelper;
        public EditModel(IUserService userService, ISelectListHelper listHelper)
        {
            _userService = userService;
            _listHelper = listHelper;
        }

        [BindProperty]
        [HiddenInput]
        public Guid UserId { get; set; }

        [BindProperty]
        public bool HasUserAdminRole { get; set; }

        [BindProperty]
        public bool HasSiteMaintenanceRole { get; set; }

        [BindProperty]
        public bool HasFileCreatorRole { get; set; }

        [BindProperty]
        public bool HasFileEditorRole { get; set; }

        [BindProperty]
        public bool HasComplianceOfficerRole { get; set; }

        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        [BindProperty]
        public Guid Id { get; set; }
        public UserView CurrentUser { get; private set; }
        public IList<string> Roles { get; private set; }

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

        public DisplayMessage Message { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id != null)
            {
                CurrentUser = await _userService.GetUserByIdAsync(id.Value);
                if(CurrentUser == null)
                {
                    return NotFound();
                }
                Id = CurrentUser.Id;
            }
            else
            {
                CurrentUser = await _userService.GetCurrentUserAsync();
                if(CurrentUser == null)
                {
                    return NotFound();
                }
                Id = CurrentUser.Id;
            }

            UserId = id.Value;
            if (!await GetUserDetails()) return NotFound();
            await GetUserRoles();

            Roles = await _userService.GetCurrentUserRolesAsync();
            UserProgramId = CurrentUser.UserProgram?.Id;
            UserUnitId = CurrentUser.UserUnit?.Id;
            UserPositionId = CurrentUser.UserPosition?.Id;
            await PopulateSelectsAsync();
            Message = TempData?.GetDisplayMessage();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id != Guid.Empty)
            {
                CurrentUser = await _userService.GetUserByIdAsync(Id)
                 ?? throw new Exception("User not found");
            }
            else
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Unable to find user with Id: {Id.ToString()}");
                await PopulateSelectsAsync();
                return Page();
            }

            Roles = await _userService.GetCurrentUserRolesAsync();
            try
            {
                await _userService.UpdateUserDesignationsAsync(CurrentUser.Id,
                    UserProgramId,
                    UserUnitId,
                    UserPositionId);
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Unable to update user designations: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"User designations successfully updated.");

            var roleSettings = new Dictionary<string, bool>()
            {
                {UserRoles.UserMaintenance, HasUserAdminRole},
                {UserRoles.SiteMaintenance, HasSiteMaintenanceRole},
                {UserRoles.FileCreator, HasFileCreatorRole},
                {UserRoles.FileEditor, HasFileEditorRole},
                {UserRoles.ComplianceOfficer, HasComplianceOfficerRole}
            };
            var result = await _userService.UpdateUserRolesAsync(UserId, roleSettings);

            if (result.Succeeded)
            {
                TempData?.SetDisplayMessage(Context.Success, "User roles successfully updated.");
                return RedirectToPage("./Details", new {id = UserId});
            }

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, string.Concat(err.Code, ": ", err.Description));
            }

            if (!await GetUserDetails()) return NotFound();
            await GetUserRoles();
            return Page();
        }

        private async Task<bool> GetUserDetails()
        {
            var user = await _userService.GetUserByIdAsync(UserId);

            if (user == null)
            {
                return false;
            }

            DisplayName = user.DisplayName;
            Email = user.Email;
            return true;
        }

        private async Task GetUserRoles()
        {
            var roles = await _userService.GetUserRolesAsync(UserId);
            HasUserAdminRole = roles.Contains(UserRoles.UserMaintenance);
            HasSiteMaintenanceRole = roles.Contains(UserRoles.SiteMaintenance);
            HasFileCreatorRole = roles.Contains(UserRoles.FileCreator);
            HasFileEditorRole = roles.Contains(UserRoles.FileEditor);
            HasComplianceOfficerRole = roles.Contains(UserRoles.ComplianceOfficer);
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