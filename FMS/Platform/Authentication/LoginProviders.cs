namespace FMS.Platform.Authentication;

public static class LoginProviderValidation
{
    private static IEnumerable<LoginProvider> _loginProviders;
    private static IEnumerable<string> _loginProviderNames;
    private const string EnabledLoginProviders = nameof(EnabledLoginProviders);

    extension(IConfiguration configuration)
    {
        private IEnumerable<LoginProvider> GetLoginProviders()
        {
            _loginProviders ??= configuration.GetSection(EnabledLoginProviders).Get<LoginProvider[]>() ?? [];
            return _loginProviders;
        }

        public IEnumerable<string> LoginProviderNames()
        {
            _loginProviderNames ??= configuration.GetLoginProviders().Select(lp => lp.Name);
            return _loginProviderNames;
        }

        public bool ValidateLoginProviderId(string loginProvider, string identityProviderId) =>
            !string.IsNullOrEmpty(loginProvider) &&
            configuration.GetLoginProviders().Contains(new LoginProvider(loginProvider, identityProviderId));

        public bool ValidateLoginProvider(string loginProvider) =>
            !string.IsNullOrEmpty(loginProvider) &&
            configuration.LoginProviderNames().Contains(loginProvider);

        public bool TestUserEnabled() =>
            configuration.LoginProviderNames().Contains(LoginProviders.TestUserScheme);
    }
}

public record LoginProvider(string Name, string Id);

public static class LoginProviders
{
    public const string DuoScheme = "DuoSSO";
    public const string EntraIdScheme = "EntraId";
    public const string TestUserScheme = "TestUser";
}
