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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FMS.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        public ApplicationUser DisplayFailedUser { get; set; }
        public string ReturnUrl { get; private set; } = "/";

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;
        private readonly IComplianceOfficerRepository _repository;
        private readonly ILogger<ExternalLoginModel> _logger;

        public ExternalLoginModel(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IWebHostEnvironment environment,
            IComplianceOfficerRepository repository,
            ILogger<ExternalLoginModel> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _environment = environment;
            _repository = repository;
            _logger = logger;
        }

        // Don't call the page directly
        public IActionResult OnGetAsync() => RedirectToPage("./Login");

        // This Post method is called by the Login page
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? "/";

            // If a local user is enabled, create user information and sign in locally.
            if (_environment.IsLocalEnv())
            {
                return await SignInAsLocalUser();
            }

            // Request a redirect to the external login provider.
            const string provider = OpenIdConnectDefaults.AuthenticationScheme;
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        private async Task<IActionResult> SignInAsLocalUser()
        {
            _logger.LogInformation("Local user signin attempted");

            var user = new ApplicationUser();
            _configuration.Bind("LocalUser", user);

            var userExists = await _userManager.FindByEmailAsync(user.Email);
            if (userExists == null)
            {
                await _userManager.CreateAsync(user);
                foreach (var role in UserRoles.AllRoles) await _userManager.AddToRoleAsync(user, role);
                // Add user to Compliance Officers list.
                await CreateComplianceOfficeAsync(user);
            }

            await _signInManager.SignInAsync(userExists ?? user, false);
            return LocalRedirect(ReturnUrl);
        }

        // This callback method is called by the external login provider.
        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            ReturnUrl = returnUrl ?? "/";

            // Handle errors returned from the external provider.
            if (remoteError is not null)
            {
                return RedirectToLoginPageWithError($"Error from work account provider: {remoteError}");
            }

            // Get information about the user from the external provider.
            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo?.Principal is null)
            {
                return RedirectToLoginPageWithError("Error loading work account information.");
            }

            var preferredUserName = externalLoginInfo.Principal.FindFirstValue(ClaimConstants.PreferredUserName);
            if (preferredUserName is null)
            {
                return RedirectToLoginPageWithError("Error loading detailed work account information.");
            }

            if (!preferredUserName.IsValidEmailDomain())
            {
                _logger.LogWarning("User {UserName} with invalid email domain attempted signin", preferredUserName);
                return RedirectToPage("./Unavailable");
            }

            // Determine if a user account already exists.
            var user = await _userManager.FindByNameAsync(preferredUserName);

            // If the user does not have an account yet, then create one and sign in.
            if (user is null)
            {
                return await CreateUserAndSignInAsync(externalLoginInfo);
            }

            // Sign in the user locally with the external provider key.
            var signInResult = await _signInManager.ExternalLoginSignInAsync(externalLoginInfo.LoginProvider,
                externalLoginInfo.ProviderKey, true, true);

            if (signInResult.Succeeded)
            {
                return await RefreshUserInfoAndSignInAsync(user, externalLoginInfo);
            }

            if (signInResult.IsLockedOut || signInResult.IsNotAllowed || signInResult.RequiresTwoFactor)
            {
                return RedirectToPage("./Unavailable");
            }

            // If ExternalLoginInfo successfully returned from external provider, and the user exists, but
            // ExternalLoginSignInAsync failed, then add the external provider info to the user and sign in.
            return await AddLoginProviderAndSignInAsync(user, externalLoginInfo);
        }

        // Redirect to Login page with error message.
        private IActionResult RedirectToLoginPageWithError(string message)
        {
            _logger.LogWarning("External login error: {Message}", message);
            TempData.SetDisplayMessage(Context.Danger, message);
            return RedirectToPage("./Login", new { ReturnUrl });
        }

        // Create a new user account and sign in.
        private async Task<IActionResult> CreateUserAndSignInAsync(ExternalLoginInfo info)
        {
            var user = new ApplicationUser
            {
                Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                UserName = info.Principal.FindFirstValue(ClaimConstants.PreferredUserName),
                ObjectId = info.Principal.FindFirstValue(ClaimConstants.ObjectId),
                GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName),
                FamilyName = info.Principal.FindFirstValue(ClaimTypes.Surname),
            };

            // Create the user in the backing store.
            var createUserResult = await _userManager.CreateAsync(user);
            if (!createUserResult.Succeeded)
            {
                _logger.LogWarning("Failed to create new user {UserName}", user.UserName);
                return FailedLogin(createUserResult, user);
            }

            _logger.LogInformation("Created new user {UserName}", user.UserName);

            // Add user to Compliance Officers list.
            await CreateComplianceOfficeAsync(user);

            // Optionally add user to selected Roles.
            var seedAdminUsers = _configuration.GetSection("SeedAdminUsers")
                .Get<string[]>().AsEnumerable();
            if (seedAdminUsers != null &&
                seedAdminUsers.Contains(user.Email, StringComparer.InvariantCultureIgnoreCase))
            {
                _logger.LogInformation("Seeding roles for new user {UserName}", user.UserName);
                await _userManager.AddToRoleAsync(user, UserRoles.UserMaintenance);
                await _userManager.AddToRoleAsync(user, UserRoles.SiteMaintenance);
                await _userManager.AddToRoleAsync(user, UserRoles.FileEditor);
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
            var coId = await _repository.TryCreateComplianceOfficerAsync(complianceOfficer);
            _logger.LogInformation("Created new compliance officer {Id}", coId);
        }

        // Update local store with from external provider. 
        private async Task<IActionResult> RefreshUserInfoAndSignInAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            _logger.LogInformation("Existing user {UserName} logged in with {LoginProvider} provider",
                user.UserName, info.LoginProvider);
            user.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
            user.GivenName = info.Principal.FindFirstValue(ClaimTypes.GivenName);
            user.FamilyName = info.Principal.FindFirstValue(ClaimTypes.Surname);
            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            return LocalRedirect(ReturnUrl);
        }

        // Add external login provider to user account and sign in user.
        private async Task<IActionResult> AddLoginProviderAndSignInAsync(ApplicationUser user, ExternalLoginInfo info)
        {
            var addLoginResult = await _userManager.AddLoginAsync(user, info);

            if (!addLoginResult.Succeeded)
            {
                _logger.LogWarning("Failed to add login provider {LoginProvider} for user {UserName}",
                    info.LoginProvider, user.UserName);
                return FailedLogin(addLoginResult, user);
            }

            _logger.LogInformation("Login provider {LoginProvider} added for user {UserName}",
                info.LoginProvider, user.UserName);

            // Include the access token in the properties.
            var props = new AuthenticationProperties();
            props.StoreTokens(info.AuthenticationTokens);
            props.IsPersistent = true;

            await _signInManager.SignInAsync(user, props, info.LoginProvider);
            return LocalRedirect(ReturnUrl);
        }

        // Add error info and return this Page.
        private IActionResult FailedLogin(IdentityResult result, ApplicationUser user)
        {
            DisplayFailedUser = user;
            foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
            return Page();
        }
    }
}
