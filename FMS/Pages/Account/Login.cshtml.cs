using FMS.Domain.Entities.Users;
using FMS.Platform.Authentication;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Account;

[AllowAnonymous]
public class LoginModel(
    SignInManager<ApplicationUser> signInManager,
    IAuthenticationManager authenticationManager,
    IConfiguration configuration
) : PageModel
{
    public string ReturnUrl { get; private set; }
    public IEnumerable<string> LoginProviderNames { get; private set; } = null!;
    public bool DisplayFailedLogin { get; private set; }
    public bool TestUserEnabled { get; private set; }
    public EntraIdPhaseOut EntraIdPhaseOut { get; } = new();

    public IActionResult OnGet(string returnUrl = null)
    {
        ReturnUrl = returnUrl;
        ConfigurePageVariables();
        if (User.Identity is not { IsAuthenticated: true }) return Page();
        return User.IsActive() ? LocalRedirectOrHome() : RedirectToPage("Logout");
    }

    public async Task<IActionResult> OnPostAsync(string scheme, string returnUrl = null)
    {
        if (User.Identity is { IsAuthenticated: true }) return RedirectToPage("Logout");
        if (!configuration.ValidateLoginProvider(scheme))
            throw new ArgumentException("Invalid scheme", nameof(scheme));

        if (scheme == LoginProviders.TestUserScheme) return await LogInAsTestUserAsync(returnUrl);

        // Request a redirect to the external login provider.
        var redirectUrl = Url.Page("Login", pageHandler: "Callback", values: new { returnUrl });
        var properties = signInManager.ConfigureExternalAuthenticationProperties(scheme, redirectUrl);
        return Challenge(properties, scheme);
    }

    public async Task<IActionResult> LogInAsTestUserAsync(string returnUrl = null)
    {
        if (!configuration.TestUserEnabled()) return BadRequest();
        ReturnUrl = returnUrl;
        await authenticationManager.LogInAsTestUserAsync();
        return LocalRedirectOrHome();
    }

    // The callback method is called by the external login provider.
    public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
    {
        ReturnUrl = returnUrl;
        if (remoteError is not null)
            return LoginPageWithError($"Error from account provider: {remoteError}");
        var result = await authenticationManager.LogInUsingExternalProviderAsync();
        return result.Succeeded ? LocalRedirectOrHome() : await FailedLoginAsync(result);
    }

    private RedirectToPageResult LoginPageWithError(string message)
    {
        TempData.SetDisplayMessage(Context.Danger, message);
        return RedirectToPage("Login", new { ReturnUrl });
    }

    private async Task<PageResult> FailedLoginAsync(IdentityResult result)
    {
        await signInManager.SignOutAsync();
        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);
        DisplayFailedLogin = true;
        ConfigurePageVariables();
        return Page();
    }

    private void ConfigurePageVariables()
    {
        LoginProviderNames = configuration.LoginProviderNames();
        TestUserEnabled = configuration.TestUserEnabled();
        configuration.GetSection(nameof(EntraIdPhaseOut)).Bind(EntraIdPhaseOut);
    }

    private IActionResult LocalRedirectOrHome() =>
        ReturnUrl is null ? RedirectToPage("/Index") : LocalRedirect(ReturnUrl);
}

public record EntraIdPhaseOut
{
    public bool Enabled { get; init; }
    public DateOnly EndDate { get; init; }
}
