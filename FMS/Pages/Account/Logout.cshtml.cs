using System;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        public IActionResult OnGet() => RedirectToPage("/Index");

#pragma warning disable 618
        public async Task<IActionResult> OnPostAsync([FromServices] SignInManager<ApplicationUser> signInManager)
        {
            if (Environment.GetEnvironmentVariable("ENABLE_TEST_USER") != "true")
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