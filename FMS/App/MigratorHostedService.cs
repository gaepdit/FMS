﻿using System;
using System.Threading;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.SeedData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FMS.App
{
    /// <summary>
    /// Set up the database 
    /// ref: https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-3/
    /// </summary>
    public class MigratorHostedService : IHostedService
    {
        // We need to inject the IServiceProvider so we can create the DbContext scoped service
        private readonly IServiceProvider _serviceProvider;
        public MigratorHostedService(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Create a new scope to retrieve scoped services
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<FmsDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            // Initialize database
            if (Environment.GetEnvironmentVariable("RECREATE_DB") == "true")
            {
                // Using "TempDb" launch profile causes the database to be recreated on launch.
                await context.Database.EnsureDeletedAsync(cancellationToken);
                await context.Database.EnsureCreatedAsync(cancellationToken);
            }
            else
            {
                // Otherwise, the database is set up using EF migrations and preserved between restarts.
                await context.Database.MigrateAsync(cancellationToken: cancellationToken);
            }

            if (env.IsDevelopment())
            {
                // Test data: will not run in production
                Infrastructure.SeedData.DevSeedData.SeedTestData(context);
                Infrastructure.Procs.StoredProcedures.CreateStoredProcedures(context);
            }

            // Initialize Administrator role           
            if (!await context.Roles.AnyAsync(e => e.Name == UserConstants.AdminRole,
                cancellationToken: cancellationToken))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>(UserConstants.AdminRole));
            }
        }

        // noop
        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}