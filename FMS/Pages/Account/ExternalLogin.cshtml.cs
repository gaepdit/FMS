using FMS.App;
using FMS.Domain.Entities.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public string ReturnUrl { get; set; }

        [BindProperty]
        public ApplicationUser DisplayUser { get; set; }

        // Don't call the page directly
        public IActionResult OnGetAsync() => RedirectToPage("./Login");

        // This Post method is called by the Login page
        public IActionResult OnPost(string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var provider = AzureADDefaults.AuthenticationScheme;
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        // This method is called by the external login provider
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Error from work account provider: {remoteError}");
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null
                || !info.Principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier)
                || !info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                TempData?.SetDisplayMessage(Context.Danger, "Error loading work account information.");
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with the external login provider if the user already has a login.
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (signInResult.Succeeded)
            {
                // TODO: Compare incoming claims with stored claims and update if needed.
                // (Use user service for this.)
                return LocalRedirect(returnUrl);
            }
            if (signInResult.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }

            // If the user does not have an account, then create one.
            string userName = info.Principal.FindFirstValue(ClaimConstants.PreferredUserName)
                ?? info.Principal.FindFirstValue(ClaimTypes.Email);

            var user = new ApplicationUser
            {
                SubjectId = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
                ObjectId = info.Principal.FindFirstValue(ClaimConstants.ObjectId),
                UserName = userName,
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                FamilyName = info.Principal.FindFirstValue(ClaimConstants.FamilyName)
            };

            // TODO: Move account creation to user service.
            // Create the user in the backing store.
            var userResult = await _userManager.CreateAsync(user);
            if (userResult.Succeeded)
            {
                // Add external provider login info to the user.
                userResult = await _userManager.AddLoginAsync(user, info);
                if (userResult.Succeeded)
                {
                    // Include the access token in the properties.
                    var props = new AuthenticationProperties();
                    props.StoreTokens(info.AuthenticationTokens);
                    props.IsPersistent = true;

                    // Sign in the user.
                    await _signInManager.SignInAsync(user, isPersistent: true);

                    // TODO: Add user to Compliance Officers list.

                    return LocalRedirect(returnUrl);
                }
            }

            foreach (var error in userResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            DisplayUser = user;
            return Page();
        }
    }
}
