using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FMS.Platform.Extensions.DevHelpers
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool IsLocalDev(this IWebHostEnvironment environment) => 
            environment.IsEnvironment("Local");
    }
}