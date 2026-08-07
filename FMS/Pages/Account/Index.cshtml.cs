using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        public IndexModel(IUserService userService) => _userService = userService;

        [BindProperty]
        public Guid Id { get; set; }
        public UserView CurrentUser { get; private set; }
        public IList<string> Roles { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id != null)
            {
                CurrentUser = await _userService.GetUserByIdAsync(id.Value)
                 ?? throw new Exception("User not found");
                Id = CurrentUser.Id;
            }
            else
            {
                CurrentUser = await _userService.GetCurrentUserAsync()
                    ?? throw new Exception("Current user not found");
                Id = CurrentUser.Id;
            }
            Roles = await _userService.GetCurrentUserRolesAsync();
            return Page();
        }
    }
}