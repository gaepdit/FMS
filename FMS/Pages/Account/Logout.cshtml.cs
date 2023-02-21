using FMS.Domain.Entities.Users;
using FMS.Platform.Extensions.DevHelpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IWebHostEnvironment _environment;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment)
        {
            _signInManager = signInManager;
            _environment = environment;
        }

        public Task<IActionResult> OnGetAsync() => LogOutAndRedirectToIndex();
        public Task<IActionResult> OnPostAsync() => LogOutAndRedirectToIndex();

        private async Task<IActionResult> LogOutAndRedirectToIndex()
        {
            // If Azure AD is enabled, sign out all authentication schemes.
            if (!_environment.IsLocalEnv())
            {
                return SignOut(new AuthenticationProperties { RedirectUri = "/Index" },
                    IdentityConstants.ApplicationScheme,
                    OpenIdConnectDefaults.AuthenticationScheme);
            }

            // If a local user is enabled instead, sign out locally and redirect to home page.
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
