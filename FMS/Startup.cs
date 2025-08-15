using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.Services;
using FMS.Platform.Extensions.DevHelpers;
using FMS.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Identity.Web;
using Mindscape.Raygun4Net;
using Mindscape.Raygun4Net.AspNetCore;
using System;
using System.Reflection;

namespace FMS
{
    public class Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
        private IConfiguration Configuration { get; } = configuration;
        private IWebHostEnvironment WebHostEnvironment { get; } = webHostEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Configure database
            services.AddDbContext<FmsDbContext>(opts =>
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    x => x.EnableRetryOnFailure().MigrationsAssembly("FMS.Infrastructure")));

            // Configure Identity
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>()
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<FmsDbContext>();

            // Configure authentication
            // An Azure AD app must be registered and configured in the settings file.
            services.AddAuthentication().AddMicrosoftIdentityWebApp(Configuration, cookieScheme: null);
            // Note: `cookieScheme: null` is mandatory. See https://github.com/AzureAD/microsoft-identity-web/issues/133#issuecomment-739550416

            // Persist data protection keys
            services.AddDataProtection();

            // Configure Razor pages 
            services.AddRazorPages();

            // Configure HSTS
            services.AddHsts(opts => { opts.MaxAge = TimeSpan.FromDays(365 * 2); });

            // Configure Raygun
            services.AddSingleton(provider =>
            {
                var client = new RaygunClient(provider.GetService<RaygunSettings>()!,
                    provider.GetService<IRaygunUserProvider>()!);
                client.SendingMessage += (_, eventArgs) =>
                    eventArgs.Message.Details.Tags.Add(WebHostEnvironment.EnvironmentName);
                return client;
            });
            services.AddRaygun(opts =>
            {
                opts.ApiKey = Configuration["RaygunSettings:ApiKey"];
                opts.ApplicationVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString(3);
                opts.ExcludeErrorsFromLocal = true;
                opts.IgnoreFormFieldNames = ["*Password"];
            });
            services.AddRaygunUserProvider();

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
            services.AddScoped<IActionTakenRepository, ActionTakenRepository>();
            services.AddScoped<IAllowedActionTakenRepository, AllowedActionTakenRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<IFundingSourceRepository, FundingSourceRepository>();
            services.AddScoped<IGroundwaterStatusRepository, GroundwaterStatusRepository>();
            services.AddScoped<IOverallStatusRepository, OverallStatusRepository>();
            services.AddScoped<IParcelTypeRepository, ParcelTypeRepository>();
            services.AddScoped<ISoilStatusRepository, SoilStatusRepository>();
            services.AddScoped<ISourceStatusRepository, SourceStatusRepository>();
            services.AddScoped<IChemicalRepository, ChemicalRepository>();
            services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
            services.AddScoped<IContactTitleRepository, ContactTitleRepository>();
            services.AddScoped<IItemsListRepository, ItemsListRepository>();
            services.AddScoped<ISelectListHelper, SelectListHelper>();
            services.AddScoped<IAllowedActionTakenHelper, AllowedActionTakenHelper>();
            services.AddScoped<IAbandonSitesRepository, AbandonSitesRepository>();
            services.AddScoped<IGapsAssessmentRepository, GapsAssessmentRepository>();
            services.AddScoped<IHsrpFacilityPropertiesRepository, HsrpFacilityPropertiesRepository>();

            // Set up database
            services.AddHostedService<MigratorHostedService>();

            // Configure bundling and minification.
            services.AddWebOptimizer();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsLocalEnv())
            {
                // Local development environment
                app.UseDeveloperExceptionPage();
            }

            if (env.IsDevelopment())
            {
                // Dev web server
                app.UseDeveloperExceptionPage();
                app.UseRaygun();
            }
            else
            {
                // Staging & Production web servers
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePages();
                app.UseRaygun();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseWebOptimizer();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapRazorPages().RequireAuthorization());
        }
    }
}
