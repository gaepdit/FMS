using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FMS.Domain.Entities.Users
{
    /// <summary>
    /// Authorization Policies for the application.
    /// The policies are configured in Startup.
    /// </summary>
    public static class UserPolicies
    {
        public const string FileCreatorOrEditor = nameof(FileCreatorOrEditor);
        public const string FileCreator = nameof(FileCreator);
        public const string FileEditor = nameof(FileEditor);
        public const string SiteMaintenance = nameof(SiteMaintenance);
        public const string UserMaintenance = nameof(UserMaintenance);
        public const string ComplianceOfficer = nameof(ComplianceOfficer);
        public const string FileEditorOrComplianceOfficer = nameof(FileEditorOrComplianceOfficer);

        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            services.AddAuthorizationBuilder()
                .AddPolicy(nameof(FileCreatorOrEditor), FileCreatorOrEditorPolicy)
                .AddPolicy(nameof(FileCreator), FileCreatorPolicy)
                .AddPolicy(nameof(FileEditor), FileEditorPolicy)
                .AddPolicy(nameof(SiteMaintenance), SiteMaintenancePolicy)
                .AddPolicy(nameof(UserMaintenance), UserMaintenancePolicy)
                .AddPolicy(nameof(ComplianceOfficer), ComplianceOfficerPolicy)
                .AddPolicy(nameof(FileEditorOrComplianceOfficer), FileEditorOrComplianceOfficerPolicy);
        }

        // Default policy builder
        private static AuthorizationPolicyBuilder ActiveUserPolicyBuilder => new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(AppClaimTypes.ActiveUser, true.ToString());

        private static AuthorizationPolicy FileCreatorOrEditorPolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.FileCreator, UserRoles.FileEditor).Build();

        private static AuthorizationPolicy FileCreatorPolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.FileCreator).Build();

        private static AuthorizationPolicy FileEditorPolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.FileEditor).Build();

        private static AuthorizationPolicy SiteMaintenancePolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.SiteMaintenance).Build();

        private static AuthorizationPolicy UserMaintenancePolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.UserMaintenance).Build();

        private static AuthorizationPolicy ComplianceOfficerPolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.ComplianceOfficer).Build();

        private static AuthorizationPolicy FileEditorOrComplianceOfficerPolicy { get; } = ActiveUserPolicyBuilder
            .RequireRole(UserRoles.FileEditor, UserRoles.ComplianceOfficer).Build();
    }
}
