using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.AspNetCore;

namespace FMS.Services
{
    public class RaygunClientProvider : DefaultRaygunAspNetCoreClientProvider
    {
        public override RaygunClient GetClient(RaygunSettings settings, HttpContext context)
        {
            var client = base.GetClient(settings, context);
            client.IgnoreFormFieldNames("*Password");
            client.ApplicationVersion = typeof(Program).Assembly.GetName().Version?.ToString(3);
            client.SendingMessage += (_, args) =>
            {
                args.Message.Details.Tags ??= new List<string>();
                args.Message.Details.Tags.Add(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
            };

            var identity = context?.User?.Identity as ClaimsIdentity;
            if (identity?.IsAuthenticated == true)
            {
                client.UserInfo = new RaygunIdentifierMessage(identity.Name) {IsAnonymous = false};
            }

            return client;
        }
    }
}