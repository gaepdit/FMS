using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Users
{
    [Authorize(Roles = UserConstants.AdminRole)]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        public EditModel(IUserService userService) => _userService = userService;

        [BindProperty]
        [HiddenInput]
        public Guid UserId { get; set; }

        [BindProperty]
        [DisplayName("Administrator Role")]
        public bool HasAdminRole { get; set; }

        [BindProperty]
        [DisplayName("Creator Role")]
        public bool HasCreatorRole { get; set; }

        [BindProperty]
        [DisplayName("Editor Role")]
        public bool HasEditorRole { get; set; }

        public string DisplayName { get; private set; }
        public string Email { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(id.Value);
            if (user == null)
            {
                return NotFound();
            }

            UserId = user.Id;
            DisplayName = user.DisplayName;
            Email = user.Email;

            var roles = await _userService.GetUserRolesAsync(UserId);
            HasAdminRole = roles.Contains(UserConstants.AdminRole);
            HasCreatorRole = roles.Contains(UserConstants.CreatorRole);
            HasEditorRole = roles.Contains(UserConstants.EditorRole);

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
                {UserConstants.AdminRole, HasAdminRole},
                {UserConstants.CreatorRole, HasCreatorRole},
                {UserConstants.EditorRole, HasEditorRole},
            };

            var result = await _userService.UpdateUserRolesAsync(UserId, roleSettings);

            if (result.Succeeded)
            {
                TempData?.SetDisplayMessage(Context.Success, "User account successfully updated.");
                return RedirectToPage("./Details", new {id = UserId});
            }

            var user = await _userService.GetUserByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }

            DisplayName = user.DisplayName;
            Email = user.Email;

            var roles = await _userService.GetUserRolesAsync(UserId);

            HasAdminRole = roles.Contains(UserConstants.AdminRole);
            HasCreatorRole = roles.Contains(UserConstants.CreatorRole);
            HasEditorRole = roles.Contains(UserConstants.EditorRole);

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return Page();
        }
    }
}