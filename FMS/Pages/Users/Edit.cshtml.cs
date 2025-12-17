using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Users
{
    [Authorize(Roles = UserRoles.UserMaintenance)]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        public EditModel(IUserService userService) => _userService = userService;

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

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            UserId = id.Value;
            if (!await GetUserDetails()) return NotFound();
            await GetUserRoles();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

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
    }
}