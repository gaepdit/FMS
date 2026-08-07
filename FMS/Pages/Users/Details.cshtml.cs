using FMS.Domain.Services;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NUglify.JavaScript.Syntax;

namespace FMS.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;
        public DetailsModel(IUserService userService) => _userService = userService;

        public string Id { get; private set; }

        public UserView CurrentUser { get; private set; }

        public string DisplayName { get; private set; }
        public string Email { get; private set; }
        public IList<string> Roles { get; private set; }
        public DisplayMessage Message { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id != null)
            {
                CurrentUser = await _userService.GetUserByIdAsync(id.Value);
                if(CurrentUser == null)
                {
                    return NotFound();
                }
                Id = CurrentUser.Id.ToString();
            }
            else
            {
                CurrentUser = await _userService.GetCurrentUserAsync();
                if(CurrentUser == null)
                {
                    return NotFound();
                }
                Id = CurrentUser.Id.ToString();
            }

            DisplayName = CurrentUser.DisplayName;
            Email = CurrentUser.Email;
            Roles = await _userService.GetUserRolesAsync(CurrentUser.Id);

            Message = TempData?.GetDisplayMessage();
            return Page();
        }
    }
}