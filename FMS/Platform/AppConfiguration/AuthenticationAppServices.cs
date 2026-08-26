using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Infrastructure.Services;
using FMS.Platform.Authentication;
using Microsoft.AspNetCore.Authentication;

namespace FMS.Platform.AppConfiguration;

public static class AuthenticationAppServices
{
    public static void AddAuthenticationAppServices(this IServiceCollection services) => services
        .AddScoped<IClaimsTransformation, AppClaimsTransformation>()
        .AddScoped<IAuthenticationManager, AuthenticationManager>()
        .AddScoped<IUserService, UserService>();
}
