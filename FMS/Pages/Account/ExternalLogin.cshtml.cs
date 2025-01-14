using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Platform.Extensions;
using FMS.Platform.Extensions.DevHelpers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IWebHostEnvironment environment,
        IComplianceOfficerRepository repository,
        ILogger<ExternalLoginModel> logger) : PageModel
    {
        public ApplicationUser DisplayFailedUser { get; set; }
        public string ReturnUrl { get; private set; }

        // Don't call the page directly
        public RedirectToPageResult OnGet() => RedirectToPage("./Login");

        // This Post method is called by the Login page
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            // If a local user is enabled, create user information and sign in locally.
            if (environment.IsLocalEnv())
            {
                return await SignInAsLocalUser();
            }

            // Request a redirect to the external login provider.
            const string provider = OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        private async Task<IActionResult> SignInAsLocalUser()
        {
            logger.LogInformation("Local user signin attempted");

            var user = new ApplicationUser();
            configuration.Bind("LocalUser", user);

            var userExists = await userManager.FindByEmailAsync(user.Email);
            if (userExists == null)
            {
                await userManager.CreateAsync(user);
                foreach (var role in UserRoles.AllRoles) await userManager.AddToRoleAsync(user, role);
                // Add user to Compliance Officers list.
                await CreateComplianceOfficeAsync(user);
            }

            await signInManager.SignInAsync(userExists ?? user, false);
            return LocalRedirectOrHome();
        }

        // This callback method is called by the external login provider.
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            ReturnUrl = returnUrl;

            // Handle errors returned from the external provider.
            if (remoteError is not null)
            {
                return RedirectToLoginPageWithError($"Error from work account provider: {remoteError}");
            }

            // Get information about the user from the external provider.
            var externalLoginInfo = await signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo?.Principal is null)
            {
                return RedirectToLoginPageWithError("Error loading work account information.");
            }

            var userTenant = externalLoginInfo.Principal.GetTenantId();
            var userEmail = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            if (userEmail is null || userTenant is null)
            {
                return RedirectToLoginPageWithError("Error loading detailed work account information.");
            }

            if (!userEmail.IsValidEmailDomain())
            {
                logger.LogWarning("User {Email} with invalid email domain attempted signin", MaskEmail(userEmail));
                return RedirectToPage("./Unavailable");
            }

            logger.LogInformation("User {Email} in tenant {TenantID} successfully authenticated", MaskEmail(userEmail), userTenant);

            // Determine if a user account already exists with the Object ID.
            // If not, then determine if a user account already exists with the given username.
            var user = await userManager.Users
                .SingleOrDefaultAsync(u => u.ObjectId == externalLoginInfo.Principal.GetObjectId()) ??
                  await userManager.FindByNameAsync(userEmail);

            // If the user does not have an account yet, then create one and sign in.
            if (user is null)
            {
                return await CreateUserAndSignInAsync(externalLoginInfo);
            }

            // Try to sign in the user locally with the external provider key.
            var signInResult = await signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, true, true);

            if (signInResult.IsLockedOut || signInResult.IsNotAllowed || signInResult.RequiresTwoFactor)
            {
                await signInManager.SignOutAsync();
                return RedirectToPage("./Unavailable");
            }

            user.MostRecentLogin = DateTimeOffset.Now;
            await userManager.UpdateAsync(user);

            // If ExternalLoginInfo was successfully returned from the external login provider and the user exists, but
            // ExternalLoginSignInAsync (signInResult) failed, then add the external login provider to the user and sign in.
            return signInResult.Succeeded
                ? await RefreshUserInfoAndSignInAsync(user, externalLoginInfo)
                : await AddLoginProviderAndSignInAsync(user, externalLoginInfo);
        }

        // Redirect to Login page with error message.
        private RedirectToPageResult RedirectToLoginPageWithError(string message)
        {
            logger.LogWarning("External login error: {Message}", message);
            TempData.SetDisplayMessage(Context.Danger, message);
            return RedirectToPage("./Login", new { ReturnUrl });
        }

        // Create a new user account and sign in.
        private async Task<IActionResult> CreateUserAndSignInAsync(ExternalLoginInfo info)
        {
            var user = new ApplicationUser
            {
                UserName = info.Principal.GetDisplayName(),
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty,
                FamilyName = info.Principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
                ObjectId = info.Principal.GetObjectId(),
                MostRecentLogin = DateTimeOffset.Now,
            };

            // Create the user in the backing store.
            var createUserResult = await userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
            {
                logger.LogWarning("Failed to create new user {Email}", MaskEmail(user.Email));
                return await FailedLogin(createUserResult, user);
            }

            logger.LogInformation("Created new user {Email} with object ID {ObjectId}", MaskEmail(user.Email), user.ObjectId);

            // Add user to Compliance Officers list.
            await CreateComplianceOfficeAsync(user);

            // Optionally add user to selected Roles.
            var seedAdminUsers = configuration.GetSection("SeedAdminUsers")
                .Get<string[]>().AsEnumerable();
            if (seedAdminUsers.Contains(user.Email, StringComparer.InvariantCultureIgnoreCase))
            {
                logger.LogInformation("Seeding roles for new user {Email}", MaskEmail(user.Email));
                await userManager.AddToRoleAsync(user, UserRoles.UserMaintenance);
                await userManager.AddToRoleAsync(user, UserRoles.SiteMaintenance);
                await userManager.AddToRoleAsync(user, UserRoles.FileEditor);
            }

            // Add the external provider info to the user and sign in.
            return await AddLoginProviderAndSignInAsync(user, info);
        }

        private async Task CreateComplianceOfficeAsync(ApplicationUser user)
        {
            var complianceOfficer = new ComplianceOfficerCreateDto
            {
                Email = user.Email,
                FamilyName = user.FamilyName,
                GivenName = user.GivenName,
            };
            var coId = await repository.TryCreateComplianceOfficerAsync(complianceOfficer);
            logger.LogInformation("Created new compliance officer {Id}", coId);
        }

        // Update local store with from external provider. 
        private async Task<IActionResult> RefreshUserInfoAndSignInAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            logger.LogInformation("Existing user {Email} logged in with {LoginProvider} provider", MaskEmail(user.Email), info.LoginProvider);

            var externalValues = new ApplicationUser
            {
                UserName = info.Principal.GetDisplayName(),
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                FamilyName = info.Principal.FindFirstValue(ClaimTypes.Surname),
            };

            if (user.UserName != externalValues.UserName || user.Email != externalValues.Email ||
                user.GivenName != externalValues.GivenName || user.FamilyName != externalValues.FamilyName)
            {
                user.UserName = externalValues.UserName;
                user.Email = externalValues.Email;
                user.GivenName = externalValues.GivenName;
                user.FamilyName = externalValues.FamilyName;
                user.ProfileUpdatedAt = DateTimeOffset.Now;
                await userManager.UpdateAsync(user);
            }

            await signInManager.RefreshSignInAsync(user);
            return LocalRedirectOrHome();
        }

        // Add external login provider to user account and sign in user.
        private async Task<IActionResult> AddLoginProviderAndSignInAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            var addLoginResult = await userManager.AddLoginAsync(user, info);

            if (!addLoginResult.Succeeded)
            {
                logger.LogWarning("Failed to add login provider {LoginProvider} for user {Email}", info.LoginProvider, MaskEmail(user.Email));
                return await FailedLogin(addLoginResult, user);
            }

            logger.LogInformation("Login provider {LoginProvider} added for user {Email} with object ID {ObjectId}", info.LoginProvider, MaskEmail(user.Email), user.ObjectId);

            // Include the access token in the properties.
            var props = new AuthenticationProperties();
            if (info.AuthenticationTokens is not null)
            {
                props.StoreTokens(info.AuthenticationTokens);
            }

            props.IsPersistent = true;

            await signInManager.SignInAsync(user, props, info.LoginProvider);
            return LocalRedirectOrHome();
        }

        // Add error info and return this Page.
        private async Task<PageResult> FailedLogin(IdentityResult result, ApplicationUser user)
        {
            DisplayFailedUser = user;
            await signInManager.SignOutAsync();
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }
        private IActionResult LocalRedirectOrHome() =>
            ReturnUrl is null ? RedirectToPage("/Index") : LocalRedirect(ReturnUrl);

        public static string MaskEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return string.Empty;
            }

            var atIndex = email.IndexOf('@');
            if (atIndex <= 1) return email;

            var maskedEmail = Regex.Replace(email[..atIndex], ".(?=.{2})", "*", RegexOptions.None, TimeSpan.FromMilliseconds(100));
            return string.Concat(maskedEmail, email.AsSpan(atIndex));
        }
    }
}
