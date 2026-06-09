using System.Reflection;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Infrastructure.Contexts;
using FMS.Infrastructure.Repositories;
using FMS.Infrastructure.Services;
using FMS.Platform.Authentication;
using FMS.Platform.Extensions.DevHelpers;
using FMS.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

            // Configure authentication and authorization.
            services.ConfigureAuthentication(Configuration).AddAuthorizationPolicies();

            // Persist data protection keys
            services.AddDataProtection();

            // Configure Razor pages 
            services.AddRazorPages();

            // Configure HSTS
            services.AddHsts(opts => { opts.MaxAge = TimeSpan.FromDays(365 * 2); });

            // Configure dependencies
            services.AddScoped<IClaimsTransformation, AppClaimsTransformation>();
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
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
            services.AddScoped<IEventContractorRepository, EventContractorRepository>();
            services.AddScoped<IFundingSourceRepository, FundingSourceRepository>();
            services.AddScoped<IGroundwaterStatusRepository, GroundwaterStatusRepository>();
            services.AddScoped<ILocationClassRepository, LocationClassRepository>();
            services.AddScoped<IOverallStatusRepository, OverallStatusRepository>();
            services.AddScoped<IParcelTypeRepository, ParcelTypeRepository>();
            services.AddScoped<ISoilStatusRepository, SoilStatusRepository>();
            services.AddScoped<ISourceStatusRepository, SourceStatusRepository>();
            services.AddScoped<IChemicalRepository, ChemicalRepository>();
            services.AddScoped<IContactTypeRepository, ContactTypeRepository>();
            services.AddScoped<IItemsListRepository, ItemsListRepository>();
            services.AddScoped<ISelectListHelper, SelectListHelper>();
            services.AddScoped<IAllowedActionTakenHelper, AllowedActionTakenHelper>();
            services.AddScoped<IAbandonedInactiveRepository, AbandonedInactiveRepository>();
            services.AddScoped<IGapsAssessmentRepository, GapsAssessmentRepository>();
            services.AddScoped<IHsrpFacilityPropertiesRepository, HsrpFacilityPropertiesRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<IParcelRepository, ParcelRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IPhoneRepository, PhoneRepository>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IGroundwaterScoreRepository, GroundwaterScoreRepository>();
            services.AddScoped<IOnsiteScoreRepository, OnsiteScoreRepository>();
            services.AddScoped<ISubstanceRepository, SubstanceRepository>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IReportingRepository, ReportingRepository>();
            services.AddScoped<IUserPositionRepository, UserPositionRepository>();
            //services.AddScoped<IUserRoleRepository, UserRoleRepository>();
            services.AddScoped<IUserProgramRepository, UserProgramRepository>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();

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
            }
            else
            {
                // Staging & Production web servers
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePages();
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseWebOptimizer();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages().RequireAuthorization();
                endpoints.MapControllers();
            });
        }
    }
}
