using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        public string ReturnUrl { get; private set; }
        public DisplayMessage Message { get; private set; }

        public IActionResult OnGet(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect("~/");
            }

            Message = TempData?.GetDisplayMessage();
            ReturnUrl = returnUrl ?? Url.Content("~/");
            return Page();
        }
    }
}