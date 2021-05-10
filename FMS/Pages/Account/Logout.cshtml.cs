using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
#pragma warning disable 618
        public IActionResult OnGet() =>
            SignOut("Identity.Application", "Identity.External", AzureADDefaults.OpenIdScheme);

        public IActionResult OnPost() =>
            SignOut("Identity.Application", "Identity.External", AzureADDefaults.OpenIdScheme);
#pragma warning restore 618
    }
}