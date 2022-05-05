﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace FMS.Platform.Extensions.DevHelpers
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool IsLocalEnv(this IWebHostEnvironment environment) => 
            environment.IsEnvironment("Local");
    }
}