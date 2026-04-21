using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService) => _userService = userService;

        public UserView CurrentUser { get; private set; }
        public IList<string> Roles { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            CurrentUser = await _userService.GetCurrentUserAsync()
                ?? throw new Exception("Current user not found");
            Roles = await _userService.GetCurrentUserRolesAsync();
            return Page();
        }
    }
}