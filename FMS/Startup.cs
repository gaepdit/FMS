using System;
using System.IO;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.Services;
using FMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mindscape.Raygun4Net.AspNetCore;

namespace FMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
            CreateFolders();
        }

        private IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _env;
        private string _dataProtectionKeysFolder;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure database
            services.AddDbContext<FmsDbContext>(opts =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");

                if (_env.IsDevelopment())
                {
                    if (Environment.GetEnvironmentVariable("RECREATE_DB") == "true")
                    {
                        // The "TempDb" launch profiles must use LocalDB
                        connectionString =
                            "Server=(localdb)\\mssqllocaldb;Database=fms-temp;Trusted_Connection=True;MultipleActiveResultSets=true";
                    }
                    else
                    {
                        // In dev environment, use connection string if specified; otherwise, use LocalDB.
                        // (In prod environment, connection string is required.)
                        connectionString ??=
                            "Server=(localdb)\\mssqllocaldb;Database=fms-local;Trusted_Connection=True;MultipleActiveResultSets=true";
                    }
                }

                opts.UseSqlServer(connectionString);
            });

            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FmsDbContext>();

            // Configure cookies
            // SameSiteMode.None is needed to get single sign-out to work
            services.Configure<CookiePolicyOptions>(opts => opts.MinimumSameSitePolicy = SameSiteMode.None);

            // Configure authentication
            // (AddAzureAD is marked as obsolete and will be removed in a future version, but
            // the replacement, Microsoft Identity Web, is net yet compatible with RoleManager.)
            // Follow along at https://github.com/AzureAD/microsoft-identity-web/issues/1091
#pragma warning disable 618
            services.AddAuthentication().AddAzureAD(opts =>
            {
                Configuration.Bind(AzureADDefaults.AuthenticationScheme, opts);
                opts.CallbackPath = "/signin-oidc";
                opts.CookieSchemeName = "Identity.External";
            });
            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, opts =>
            {
                opts.Authority += "/v2.0/";
                opts.TokenValidationParameters.ValidateIssuer = true;
                opts.UsePkce = true;
            });
#pragma warning restore 618

            services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(_dataProtectionKeysFolder));

            // Configure Razor pages 
            services.AddRazorPages();

            // Configure HSTS
            services.AddHsts(opts => { opts.MaxAge = TimeSpan.FromDays(365 * 2); });

            // Configure Raygun
            services.AddRaygun(Configuration, new RaygunMiddlewareSettings
            {
                ClientProvider = new RaygunClientProvider()
            });

            // Configure authorization policies 
            services.AddAuthorization(opts =>
            {
                opts.AddPolicy(UserPolicies.FileCreatorOrEditor, policy =>
                    policy.RequireRole(UserRoles.FileCreator, UserRoles.FileEditor));
            });

            // Configure dependencies
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IFileRepository, FileRepository>();
            services.AddScoped<IBudgetCodeRepository, BudgetCodeRepository>();
            services.AddScoped<IComplianceOfficerRepository, ComplianceOfficerRepository>();
            services.AddScoped<IFacilityStatusRepository, FacilityStatusRepository>();
            services.AddScoped<IFacilityTypeRepository, FacilityTypeRepository>();
            services.AddScoped<IOrganizationalUnitRepository, OrganizationalUnitRepository>();
            services.AddScoped<ICabinetRepository, CabinetRepository>();
            services.AddScoped<IItemsListRepository, ItemsListRepository>();
            services.AddScoped<ISelectListHelper, SelectListHelper>();

            // Set up database
            services.AddHostedService<MigratorHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePages();

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRaygun();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapRazorPages().RequireAuthorization());
        }

        private void CreateFolders()
        {
            // Base path for persisted files
            var basePath = string.IsNullOrWhiteSpace(Configuration["PersistedFilesBasePath"])
                ? "../../_GeneratedFiles"
                : Configuration["PersistedFilesBasePath"].ForceToString();

            // Data protection keys folder
            _dataProtectionKeysFolder = Path.Combine(basePath, "DataProtectionKeys");
            Directory.CreateDirectory(_dataProtectionKeysFolder);
        }
    }
}