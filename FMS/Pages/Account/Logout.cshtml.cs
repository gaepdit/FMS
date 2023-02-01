using FMS.Domain.Entities.Users;
using FMS.Platform.Extensions.DevHelpers;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
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
        public IActionResult OnGet() => RedirectToPage("/Index");

#pragma warning disable 618
        public async Task<IActionResult> OnPostAsync(
            [FromServices] SignInManager<ApplicationUser> signInManager,
            [FromServices] IWebHostEnvironment environment)
        {
            if (!environment.IsLocalEnv())
            {
                return SignOut(IdentityConstants.ApplicationScheme, IdentityConstants.ExternalScheme,
                    AzureADDefaults.OpenIdScheme);
            }

            // If "test" users is enabled, sign out locally and redirect to home page.
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
#pragma warning restore 618
    }
}
