using FMS.Domain.Entities.Users;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.DbScripts;
using FMS.TestData.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FMS.Platform.AppConfiguration;

internal static class DataPersistence
{
    public static async Task ConfigureDataPersistenceAsync(this IHostApplicationBuilder builder)
    {
        // Configure database
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("No connection string found.");

        builder.Services.AddDbContext<FmsDbContext>(db => db
            .UseSqlServer(connectionString, optionsBuilder =>
                optionsBuilder.EnableRetryOnFailure().MigrationsAssembly("FMS.Infrastructure")));

        // Configure Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<FmsDbContext>();

        // Configure Repositories
        builder.Services.AddEntityFrameworkRepositories();

        // Initialize database
        var dbContextOptions = new DbContextOptionsBuilder<FmsDbContext>()
            .UseSqlServer(connectionString, opts =>
                opts.MigrationsAssembly("FMS.Infrastructure")).Options;

        await using var context = new FmsDbContext(dbContextOptions, null);

        if (builder.Environment.IsEnvironment("Local"))
        {
            // Delete and re-create database as currently defined.
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();
            await context.CreateStoredProceduresAsync();

            // Seed data only in local environment.
            await context.SeedDataAsync();
        }
        else
        {
            // Run database migrations if not local.
            await context.Database.MigrateAsync();
        }

        // Initialize any new roles.
        var roleManager = builder.Services.BuildServiceProvider().GetRequiredService<RoleManager<IdentityRole<Guid>>>();
        foreach (var role in UserRoles.AllRoles)
            if (!await context.Roles.AnyAsync(e => e.Name == role))
                await roleManager.CreateAsync(new IdentityRole<Guid>(role));
    }
}
