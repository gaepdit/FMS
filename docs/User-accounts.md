# FMS User accounts

FMS uses the Georgia Technology Authority Azure AD for account setup and login. 
See the [Azure Portal](https://portal.azure.com/#blade/Microsoft_AAD_RegisteredApps/ApplicationMenuBlade/Overview/appId/8a4c7829-92fa-4363-b257-c97e38f7711f/isMSAApp/) 
for application registration info.

[Permissions](https://docs.microsoft.com/en-us/azure/active-directory/develop/v2-permissions-and-consent) 
requested from Azure AD are:

* openid (included by default)
* profile (included by default)

SubjectId and ObjectId claims are stored to identify the user. (SubjectId 
uniquely identifies the user within the application; ObjectId uniquely 
identifies the user across applications. See 
[Microsoft identity platform ID tokens](https://docs.microsoft.com/en-us/azure/active-directory/develop/id-tokens#claims-in-an-id_token).)

Email address and name are stored for display/search within the application.

## Configuration

Add the following section to `appsettings.{Environment}.json`:

```json
  "AzureAd": {
    "Instance": "https://login.microsoftonline.com/",
    "Domain": "[Enter domain].onmicrosoft.com",
    "TenantId": "[Enter the Directory/Tenant ID from the Azure portal]",
    "ClientId": "[Enter the Application/Client ID from the Azure portal]",
    "ClientSecret": "[Enter a Client Secret from the Azure portal]",
    "CallbackPath": "/signin-oidc",
    "SignedOutCallbackPath ": "/signout-oidc",
    "CookieSchemeName": "Identity.External"
  },
```

## External login flow diagram

![](img/login-flow.svg)

## Future work

Eventually, [`AddAzureAD()`](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.AzureAD.UI) 
will be replaced by [Microsoft Identity Web](https://aka.ms/ms-identity-web). 
(See this [migration tutorial](https://docs.microsoft.com/en-us/azure/active-directory/develop/scenario-web-app-sign-user-app-configuration?tabs=aspnetcore).)
Before that can happen, though, [this bug](https://github.com/AzureAD/microsoft-identity-web/issues/133) 
must be fixed at a minimum.
