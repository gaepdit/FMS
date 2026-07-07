using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account;

[AllowAnonymous]
public class LogoutModel(SignInManager<ApplicationUser> signInManager) : PageModel
{
    public Task<SignOutResult> OnGetAsync() => SignOut();
    public Task<SignOutResult> OnPostAsync(string returnUrl = null) => SignOut(returnUrl);

    private async Task<SignOutResult> SignOut(string returnUrl = null)
    {
        var authenticationProperties = new AuthenticationProperties { RedirectUri = returnUrl ?? "../" };
        await signInManager.SignOutAsync();
        return SignOut(authenticationProperties);
    }
}
