using System.Security.Claims;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using ZLogger;

namespace FMS.Platform.Authentication;

public interface IAuthenticationManager : IDisposable
{
    public Task<IdentityResult> LogInUsingExternalProviderAsync();
    public Task<IdentityResult> LogInAsTestUserAsync();
}

public sealed class AuthenticationManager(
    SignInManager<ApplicationUser> signInManager,
    UserManager<ApplicationUser> userManager,
    IConfiguration configuration,
    IComplianceOfficerRepository repository,
    ILogger<AuthenticationManager> logger)
    : IAuthenticationManager
{
    public async Task<IdentityResult> LogInUsingExternalProviderAsync()
    {
        // Get information about the user from the external provider.
        var externalLoginInfo = await signInManager.GetExternalLoginInfoAsync();
        if (externalLoginInfo?.Principal is null)
            return MissingExternalLoginInfo();

        var loginProvider = externalLoginInfo.LoginProvider;
        var identityProviderId = externalLoginInfo.Principal.GetIdentityProviderId() ?? string.Empty;
        var userEmail = externalLoginInfo.Principal.GetEmail();
        var providerKey = externalLoginInfo.ProviderKey;

        if (userEmail is null) return MissingExternalLoginInfo();

        if (!configuration.ValidateLoginProviderId(loginProvider, identityProviderId))
            return InvalidLoginProvider(loginProvider, identityProviderId);

        logger.ZLogInformation($"User with ID {providerKey} in provider {loginProvider} successfully authenticated");

        // Find a user account using the external login provider.
        // If none, then find an account with the given username.
        var user = await userManager.FindByLoginAsync(loginProvider, providerKey) ??
                   await userManager.FindByNameAsync(userEmail);

        // If the user is not found, then create a new account.
        if (user is null)
            return await CreateUserAndSignInAsync(externalLoginInfo);

        // If the user has been marked as inactive, don't sign in.
        if (!user.Active)
            return InactiveUser(providerKey);

        // Try to sign in the user locally with the external provider key.
        var signInResult = await signInManager.ExternalLoginSignInAsync(loginProvider, providerKey, isPersistent: true);

        if (signInResult.IsLockedOut || signInResult.IsNotAllowed || signInResult.RequiresTwoFactor)
            return UserNotAllowed(providerKey);

        if (signInResult.Succeeded)
            return await RefreshUserInfoAndSignInAsync(user, externalLoginInfo);

        // If the ExternalLoginInfo successfully returned from the external provider, and the user account already
        // exists, but ExternalLoginSignInAsync failed (`Succeeded == false`), then the user is likely using a new
        // external provider. Add the new provider info to the user account.
        return await AddLoginProviderAndSignInAsync(user, externalLoginInfo);
    }

    public async Task<IdentityResult> LogInAsTestUserAsync()
    {
        var userId = new Guid("00000001-0000-0000-0000-000000000000");
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            const string email = "test.user@example.com";
            user = new ApplicationUser
            {
                Id = userId,
                GivenName = "Test",
                FamilyName = "User",
                Email = email,
                UserName = email.ToLowerInvariant(),
                NormalizedEmail = email.ToUpperInvariant(),
                NormalizedUserName = email.ToUpperInvariant(),
            };

            await userManager.CreateAsync(user);
            foreach (var role in UserRoles.AllRoles) await userManager.AddToRoleAsync(user, role);

            // Add user to Compliance Officers list.
            await CreateComplianceOfficeAsync(user);
        }

        logger.ZLogInformation($"Local user with ID {userId} signed in");

        await signInManager.SignInWithClaimsAsync(user, isPersistent: false,
            additionalClaims: [new Claim(ClaimTypes.AuthenticationMethod, LoginProviders.TestUserScheme)]);
        return IdentityResult.Success;
    }

    private async Task<IdentityResult> CreateUserAndSignInAsync(ExternalLoginInfo info)
    {
        var user = new ApplicationUser
        {
            UserName = info.Principal.GetEmail(),
            Email = info.Principal.GetEmail(),
            GivenName = info.Principal.GetGivenName(),
            FamilyName = info.Principal.GetFamilyName(),
            AccountCreatedAt = DateTimeOffset.Now,
            MostRecentLogin = DateTimeOffset.Now,
        };

        // Create the user in the backing store.
        var createUserResult = await userManager.CreateAsync(user);
        if (!createUserResult.Succeeded)
            return UnableToCreateUser(info.ProviderKey);

        await SeedRolesAsync(user);

        logger.ZLogInformation($"Created new user with ID {info.ProviderKey}");

        // Add user to Compliance Officers list.
        await CreateComplianceOfficeAsync(user);

        return await AddLoginProviderAndSignInAsync(user, info);
    }

    private async Task CreateComplianceOfficeAsync(ApplicationUser user)
    {
        var complianceOfficer = new ComplianceOfficerCreateDto
        {
            //UserId = user.Id,
            Email = user.Email,
            FamilyName = user.FamilyName,
            GivenName = user.GivenName,
        };
        var coId = await repository.TryCreateComplianceOfficerAsync(complianceOfficer);

        logger.ZLogInformation($"Created new compliance officer {coId}");
    }

    private async Task SeedRolesAsync(ApplicationUser user)
    {
        // Add the new user to application admin Roles if seeded in AppSettings.
        var seedAdminUsers = configuration.GetSection("SeedAdminUsers")
            .Get<string[]>().AsEnumerable();
        if (seedAdminUsers.Contains(user.Email, StringComparer.InvariantCultureIgnoreCase))
        {
            logger.ZLogInformation($"Seeding roles for new user with ID {user.Id}");
            await userManager.AddToRoleAsync(user, UserRoles.UserMaintenance);
            await userManager.AddToRoleAsync(user, UserRoles.SiteMaintenance);
            await userManager.AddToRoleAsync(user, UserRoles.FileEditor);
        }
    }

    private async Task<IdentityResult> AddLoginProviderAndSignInAsync(ApplicationUser user, ExternalLoginInfo info)
    {
        // Add the external provider info to the user and sign in.
        var addLoginResult = await userManager.AddLoginAsync(user, info);

        if (!addLoginResult.Succeeded)
            return UnableToAddLoginProvider(info.LoginProvider, info.ProviderKey);

        logger.ZLogInformation($"Login provider {info.LoginProvider} added for user with ID {info.ProviderKey}");

        // Update auditing info.
        user.MostRecentLogin = DateTimeOffset.Now;
        user.AccountUpdatedAt = DateTimeOffset.Now;
        await userManager.UpdateAsync(user);

        return await FinalSignInAsync(user, info);
    }

    private async Task<IdentityResult> FinalSignInAsync(ApplicationUser user, ExternalLoginInfo info)
    {
        // Include the access token in the properties.
        var props = new AuthenticationProperties();
        if (info.AuthenticationTokens is not null) props.StoreTokens(info.AuthenticationTokens);
        props.IsPersistent = true;
        await signInManager.SignInAsync(user, props, info.LoginProvider);
        return IdentityResult.Success;
    }

    private async Task<IdentityResult> RefreshUserInfoAndSignInAsync(ApplicationUser user, ExternalLoginInfo info)
    {
        logger.ZLogInformation(
            $"Existing user with ID {info.ProviderKey} logged in with {info.LoginProvider} provider");

        var previousValues = new ApplicationUser
        {
            UserName = user.UserName,
            Email = user.Email,
            GivenName = user.GivenName,
            FamilyName = user.FamilyName,
        };

        user.UserName = info.Principal.GetEmail();
        user.Email = info.Principal.GetEmail();
        user.GivenName = info.Principal.GetGivenName();
        user.FamilyName = info.Principal.GetFamilyName();

        if (user.UserName != previousValues.UserName || user.Email != previousValues.Email ||
            user.GivenName != previousValues.GivenName || user.FamilyName != previousValues.FamilyName)
        {
            user.AccountUpdatedAt = DateTimeOffset.Now;
        }

        user.MostRecentLogin = DateTimeOffset.Now;
        await userManager.UpdateAsync(user);

        return await FinalSignInAsync(user, info);
    }

    // Identity Manager errors

    private IdentityResult MissingExternalLoginInfo()
    {
        const string description = "Error retrieving external account information";
        var error = new IdentityError
        {
            Code = nameof(MissingExternalLoginInfo),
            Description = $"{description}.",
        };
        logger.ZLogWarning($"{description}");
        return IdentityResult.Failed(error);
    }

    private IdentityResult InvalidLoginProvider(string loginProvider, string identityProviderId)
    {
        var error = new IdentityError
        {
            Code = nameof(InvalidLoginProvider),
            Description = $"Invalid login provider '{loginProvider}' with ID '{identityProviderId}'.",
        };
        logger.ZLogWarning($"Invalid login provider '{loginProvider}' with ID '{identityProviderId}'");
        return IdentityResult.Failed(error);
    }

    private IdentityResult UnableToCreateUser(string subjectId)
    {
        var error = new IdentityError
        {
            Code = nameof(UnableToCreateUser),
            Description = $"Failed to create new user with subject ID {subjectId}.",
        };
        logger.ZLogWarning($"Failed to create new user with subject ID {subjectId}");
        return IdentityResult.Failed(error);
    }

    private IdentityResult UnableToAddLoginProvider(string loginProvider, string providerKey)
    {
        var error = new IdentityError
        {
            Code = nameof(UnableToAddLoginProvider),
            Description = $"Failed to add login provider {loginProvider} for user with ID {providerKey}.",
        };
        logger.ZLogWarning($"Failed to add login provider {loginProvider} for user with ID {providerKey}");
        return IdentityResult.Failed(error);
    }

    private IdentityResult InactiveUser(string subjectId)
    {
        var error = new IdentityError
        {
            Code = nameof(InactiveUser),
            Description = $"Inactive user with subject ID {subjectId}.",
        };
        logger.ZLogWarning($"Inactive user with subject ID {subjectId}");
        return IdentityResult.Failed(error);
    }

    private IdentityResult UserNotAllowed(string subjectId)
    {
        var error = new IdentityError
        {
            Code = nameof(UserNotAllowed),
            Description = $"User with subject ID {subjectId} is not allowed.",
        };
        logger.ZLogWarning($"User with subject ID {subjectId} is not allowed");
        return IdentityResult.Failed(error);
    }

    public void Dispose() => userManager.Dispose();
}
