using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace FMS.Pages.Users
{
    // TODO #38: Add authorize attribute in production 
    //[Authorize(Roles = UserConstants.EditorRole)]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;
        public EditModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        [DisplayName("Editor Role")]
        public bool HasEditorRole { get; set; }

        public string DisplayName { get; set; }
        public string Email { get; set; }

        public IEnumerable<IdentityError> Errors { get; set; }

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

            Id = id.Value;
            DisplayName = user.DisplayName;
            Email = user.Email;

            IList<string> roles = await _userService.GetUserRolesAsync(Id);
            HasEditorRole = roles.Contains(UserConstants.EditorRole);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.UpdateUserRoleAsync(Id, UserConstants.EditorRole, HasEditorRole);

            if (result.Succeeded)
            {
                TempData?.SetDisplayMessage(Context.Success, "User account successfully updated.");
                return RedirectToPage("./Details", new { id = Id });
            }

            if (!await _userService.UserExistsAsync(Id))
            {
                return NotFound();
            }

            var user = await _userService.GetUserByIdAsync(Id);
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
