using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace FMS.Domain.Entities.Users;

public static class AppClaimTypes
{
    public const string ActiveUser = nameof(ActiveUser);
}

public class AppClaimsTransformation(UserManager<ApplicationUser> userManager) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var id = principal.GetUserId();
        if (!Guid.TryParse(id, out _)) return principal;

        var applicationUser = await userManager.GetUserAsync(principal);
        if (applicationUser is null) return principal;

        var claimsIdentity = new ClaimsIdentity();
        AddNewClaim(claimsIdentity, principal, AppClaimTypes.ActiveUser, applicationUser.Active.ToString());

        principal.AddIdentity(claimsIdentity);
        return principal;
    }

    private static void AddNewClaim(ClaimsIdentity claimsIdentity, ClaimsPrincipal principal,
        string type, string value)
    {
        if (value != null && !principal.HasClaim(type, value)) claimsIdentity.AddClaim(new Claim(type, value));
    }
}
