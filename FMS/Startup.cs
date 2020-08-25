using FMS.Domain.Entities.Users;
using FMS.Domain.Services;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure database
            services.AddDbContext<FmsDbContext>(opts =>
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");

                if (_env.IsDevelopment())
                {
                    if (Environment.GetEnvironmentVariable("RECREATE_DB") == "true")
                    {
                        // The "IISX-TempDb" launch profile must use LocalDB
                        connectionString = "Server=(localdb)\\mssqllocaldb;Database=fms-temp;Trusted_Connection=True;MultipleActiveResultSets=true";
                    }
                    else
                    {
                        // In dev environment, use LocalDB if no connection string specified.
                        // (In prod environment, connection string is required.)
                        connectionString ??= "Server=(localdb)\\mssqllocaldb;Database=fms-local;Trusted_Connection=True;MultipleActiveResultSets=true";
                    }
                }

                opts.UseSqlServer(connectionString);
            });

            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FmsDbContext>();

            // Configure cookies
            services.Configure<CookiePolicyOptions>(opts => opts.MinimumSameSitePolicy = SameSiteMode.None);

            // Configure authentication
            services.AddAuthentication()
                .AddAzureAD(opts => Configuration.Bind(AzureADDefaults.AuthenticationScheme, opts));

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, opts =>
            {
                opts.Authority += "/v2.0/";
                opts.TokenValidationParameters.ValidateIssuer = true;
                opts.UsePkce = true;
            });

            // Configure Razor pages 

            services.AddRazorPages(opts =>
            {
                opts.Conventions.AuthorizeFolder("/Users");
            });

            // Configure dependencies
            services.AddTransient<IUserService, UserService>();


            services.AddScoped(typeof(IFacilityRepository), typeof(FacilityRepository));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, FmsDbContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapRazorPages());

            // Initialize database
            if (Environment.GetEnvironmentVariable("RECREATE_DB") == "true")
            {
                // Using "IISX-TempDb" launch profile causes the database to be recreated on launch 
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
            else
            {
                context.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                Infrastructure.SeedData.DevSeedData.SeedTestData(context);
            }
        }
    }
}
