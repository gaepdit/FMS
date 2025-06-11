using System;
using System.Threading;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.DbScripts;
using FMS.Platform.Extensions.DevHelpers;
using FMS.TestData.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FMS.Services
{
    /// <summary>
    /// Set up the database 
    /// ref: https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-3/
    /// </summary>
    public class MigratorHostedService : IHostedService
    {
        // We need to inject the IServiceProvider so we can create the DbContext scoped service.
        private readonly IServiceProvider _serviceProvider;
        public MigratorHostedService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services.
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FmsDbContext>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            // Initialize database.
            if (env.IsLocalEnv())
            {
                // Delete and re-create database as currently defined.
                await context.Database.EnsureDeletedAsync(cancellationToken);
                await context.Database.EnsureCreatedAsync(cancellationToken);
                await context.CreateStoredProceduresAsync();

                // Seed data only in local environment.
                await context.SeedDataAsync(cancellationToken);
            }
            else
            {
                // Run database migrations if not local.
                await context.Database.MigrateAsync(cancellationToken);
            }

            // Initialize any new roles.
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            foreach (var role in UserRoles.AllRoles)
                if (!await context.Roles.AnyAsync(e => e.Name == role, cancellationToken))
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }

        // noop
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
