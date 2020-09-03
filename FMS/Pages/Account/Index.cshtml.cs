using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService) => _userService = userService;

        public string DisplayName { get; set; }
        public string Email { get; set; }
        public IList<string> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userService.GetCurrentUserAsync()
                ?? throw new Exception("Current user not found");

            DisplayName = currentUser.DisplayName;
            Email = currentUser.Email;
            Roles = await _userService.GetCurrentUserRolesAsync();

            return Page();
        }
    }
}
