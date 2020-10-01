using System;
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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.UpdateUserRoleAsync(UserId, UserConstants.AdminRole, HasAdminRole);

            if (result.Succeeded)
            {
                TempData?.SetDisplayMessage(Context.Success, "User account successfully updated.");
                return RedirectToPage("./Details", new {id = UserId});
            }

            if (!await _userService.UserExistsAsync(UserId))
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(UserId);
            if (user == null)
            {
                return NotFound();
            }

            DisplayName = user.DisplayName;
            Email = user.Email;

            foreach (var err in result.Errors)
            {
                ModelState.AddModelError(string.Empty, err.Description);
            }

            return Page();
        }
    }
}