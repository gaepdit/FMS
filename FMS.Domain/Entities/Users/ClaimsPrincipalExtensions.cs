using System.Security.Claims;

namespace FMS.Domain.Entities.Users;

public static class ClaimsPrincipalExtensions
{
    // Identity Provider claim types
    private const string IdentityProviderId = "idp";
    private const string TenantId = "http://schemas.microsoft.com/identity/claims/tenantid";
    private const string Email = "email";
    private const string GivenName = "given_name";
    private const string FamilyName = "family_name";

    extension(ClaimsPrincipal principal)
    {
        public string GetIdentityProviderId() => principal.FindFirstOfMany(IdentityProviderId, TenantId);
        public string GetEmail() => principal.FindFirstOfMany(ClaimTypes.Email, Email);
        public string GetGivenName() => principal.FindFirstOfMany(ClaimTypes.GivenName, GivenName) ?? string.Empty;
        public string GetFamilyName() => principal.FindFirstOfMany(ClaimTypes.Surname, FamilyName) ?? string.Empty;
        public string GetUserId() => principal.FindFirstValue(ClaimTypes.NameIdentifier);

        public bool IsActive() => principal.HasClaim(AppClaimTypes.ActiveUser, true.ToString());

        private string FindFirstOfMany(params string[] claimNames) => claimNames
            .Select(principal.FindFirstValue)
            .FirstOrDefault(currentValue => !string.IsNullOrEmpty(currentValue));
    }
}
