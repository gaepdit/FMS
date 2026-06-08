using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FMS.Platform.Authentication;

public static class AuthenticationServices
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var authenticationBuilder = services
            .ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = ".FMS.Identity";
                options.Cookie.Path = "/";
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            })
            .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        if (configuration.LoginProviderNames().Contains(LoginProviders.DuoScheme))
        {
            // Requires a Duo account
            authenticationBuilder.AddOpenIdConnect(authenticationScheme: LoginProviders.DuoScheme,
                displayName: "Duo SSO",
                configureOptions: options =>
                {
                    var configSection = configuration.GetSection("DuoSSO");

                    options.Authority = configSection["Authority"];
                    options.ClientId = configSection["ClientId"];
                    options.ClientSecret = configSection["ClientSecret"];
                    // (Each OIDC provider must have a unique callback path.)
                    options.CallbackPath = configSection["CallbackPath"];

                    options.Scope.Add("profile");
                    options.Scope.Add("email");

                    // `SignInScheme = null` is mandatory.
                    // See https://github.com/AzureAD/microsoft-identity-web/issues/133#issuecomment-739550416
                    options.SignInScheme = null;
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.MapInboundClaims = false;
                });
        }

        if (configuration.LoginProviderNames().Contains(LoginProviders.EntraIdScheme))
        {
            // Requires an Entra ID account
            authenticationBuilder.AddMicrosoftIdentityWebApp(configuration,
                openIdConnectScheme: LoginProviders.EntraIdScheme, cookieScheme: null);
            // Note: `cookieScheme: null` is mandatory. See https://github.com/AzureAD/microsoft-identity-web/issues/133#issuecomment-739550416
        }

        return services;
    }
}
