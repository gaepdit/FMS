using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IComplianceOfficerRepository _repository;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IComplianceOfficerRepository repository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _repository = repository;
        }

        [BindProperty]
        public ApplicationUser DisplayFailedUser { get; set; }

        // Don't call the page directly
        public IActionResult OnGetAsync() => RedirectToPage("./Login");

        // This Post method is called by the Login page
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // If "test" users is enabled, create user information and sign in locally.
            if (Environment.GetEnvironmentVariable("ENABLE_TEST_USER") == "true") return await SignInAsTestUser();

            // Request a redirect to the external login provider.
#pragma warning disable 618
            const string provider = AzureADDefaults.AuthenticationScheme;
#pragma warning restore 618
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new {returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);

            async Task<IActionResult> SignInAsTestUser()
            {
                var user = new ApplicationUser();
                _configuration.Bind("FakeUser", user);

                var userExists = await _userManager.FindByEmailAsync(user.Email);
                if (userExists == null)
                {
                    await _userManager.CreateAsync(user);
                    foreach (var role in UserRoles.AllRoles) await _userManager.AddToRoleAsync(user, role);
                }

                await _signInManager.SignInAsync(userExists ?? user, false);
                return LocalRedirect(returnUrl);
            }
        }

        // This method is called by the external login provider
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Error from work account provider: {remoteError}");
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl});
            }

            // Get information about the user from the external provider.
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();

            if (externalLoginInfo == null
                || !externalLoginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.NameIdentifier)
                || !externalLoginInfo.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
            {
                TempData?.SetDisplayMessage(Context.Danger, "Error loading work account information.");
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl});
            }

            // Sign in the user with the external provider.
            var signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, true, true);

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }

            if (signInResult.IsLockedOut || signInResult.IsNotAllowed || signInResult.RequiresTwoFactor)
            {
                return RedirectToPage("./Unavailable");
            }

            // If ExternalLoginSignInAsync failed, see if user account exists.
            var userEmail = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var existingUser = await _userManager.FindByNameAsync(userEmail);

            if (existingUser != null)
            {
                // Add external provider info to the user and sign in.
                var addLoginResult = await _userManager.AddLoginAsync(existingUser, externalLoginInfo);
                if (!addLoginResult.Succeeded)
                {
                    foreach (var error in addLoginResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    DisplayFailedUser = existingUser;
                    return Page();
                }

                // Include the access token in the properties.
                var props = new AuthenticationProperties();
                props.StoreTokens(externalLoginInfo.AuthenticationTokens);
                props.IsPersistent = true;

                // Sign in the user.
                await _signInManager.SignInAsync(existingUser, true);
                return LocalRedirect(returnUrl);
            }

            // If the user does not have an account, then create one.
            var newUser = new ApplicationUser
            {
                Email = userEmail,
                UserName = externalLoginInfo.Principal.FindFirstValue(ClaimConstants.PreferredUserName) ?? userEmail,
                SubjectId = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.NameIdentifier),
                ObjectId = externalLoginInfo.Principal.FindFirstValue(ClaimConstants.ObjectId),
                GivenName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.GivenName),
                FamilyName = externalLoginInfo.Principal.FindFirstValue(ClaimConstants.FamilyName)
            };

            // Create the user in the backing store.
            var userResult = await _userManager.CreateAsync(newUser);
            if (userResult.Succeeded)
            {
                // Optionally add user to selected Roles.
                var seedAdminUsers = _configuration.GetSection("SeedAdminUsers")
                    .Get<string[]>().AsEnumerable();
                if (seedAdminUsers != null &&
                    seedAdminUsers.Contains(newUser.Email, StringComparer.InvariantCultureIgnoreCase))
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.UserMaintenance);
                    await _userManager.AddToRoleAsync(newUser, UserRoles.SiteMaintenance);
                    await _userManager.AddToRoleAsync(newUser, UserRoles.FileEditor);
                }

                // Add external provider login info to the user.
                userResult = await _userManager.AddLoginAsync(newUser, externalLoginInfo);
                if (userResult.Succeeded)
                {
                    // Include the access token in the properties.
                    var props = new AuthenticationProperties();
                    props.StoreTokens(externalLoginInfo.AuthenticationTokens);
                    props.IsPersistent = true;

                    // Sign in the user.
                    await _signInManager.SignInAsync(newUser, true);

                    // Add user to Compliance Officers list.
                    var complianceOfficer = new ComplianceOfficerCreateDto()
                    {
                        Email = newUser.Email,
                        FamilyName = newUser.FamilyName,
                        GivenName = newUser.GivenName
                    };
                    await _repository.TryCreateComplianceOfficerAsync(complianceOfficer);

                    return LocalRedirect(returnUrl);
                }
            }

            foreach (var error in userResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            DisplayFailedUser = newUser;
            return Page();
        }
    }
}