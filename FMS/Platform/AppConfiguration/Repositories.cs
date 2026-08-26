using FMS.Domain.Repositories;
using FMS.Infrastructure.Repositories;

namespace FMS.Platform.AppConfiguration;

internal static class Repositories
{
    public static void AddEntityFrameworkRepositories(this IServiceCollection services) => services
        .AddScoped<IFacilityRepository, FacilityRepository>()
        .AddScoped<IFileRepository, FileRepository>()
        .AddScoped<IBudgetCodeRepository, BudgetCodeRepository>()
        .AddScoped<IComplianceOfficerRepository, ComplianceOfficerRepository>()
        .AddScoped<IFacilityStatusRepository, FacilityStatusRepository>()
        .AddScoped<IFacilityTypeRepository, FacilityTypeRepository>()
        .AddScoped<IOrganizationalUnitRepository, OrganizationalUnitRepository>()
        .AddScoped<ICabinetRepository, CabinetRepository>()
        .AddScoped<IActionTakenRepository, ActionTakenRepository>()
        .AddScoped<IAllowedActionTakenRepository, AllowedActionTakenRepository>()
        .AddScoped<IEventTypeRepository, EventTypeRepository>()
        .AddScoped<IEventContractorRepository, EventContractorRepository>()
        .AddScoped<IFundingSourceRepository, FundingSourceRepository>()
        .AddScoped<IGroundwaterStatusRepository, GroundwaterStatusRepository>()
        .AddScoped<ILocationClassRepository, LocationClassRepository>()
        .AddScoped<IOverallStatusRepository, OverallStatusRepository>()
        .AddScoped<IParcelTypeRepository, ParcelTypeRepository>()
        .AddScoped<ISoilStatusRepository, SoilStatusRepository>()
        .AddScoped<ISourceStatusRepository, SourceStatusRepository>()
        .AddScoped<IChemicalRepository, ChemicalRepository>()
        .AddScoped<IContactTypeRepository, ContactTypeRepository>()
        .AddScoped<IItemsListRepository, ItemsListRepository>()
        .AddScoped<ISelectListHelper, SelectListHelper>()
        .AddScoped<IAllowedActionTakenHelper, AllowedActionTakenHelper>()
        .AddScoped<IAbandonedInactiveRepository, AbandonedInactiveRepository>()
        .AddScoped<IGapsAssessmentRepository, GapsAssessmentRepository>()
        .AddScoped<IHsrpFacilityPropertiesRepository, HsrpFacilityPropertiesRepository>()
        .AddScoped<ILocationRepository, LocationRepository>()
        .AddScoped<IParcelRepository, ParcelRepository>()
        .AddScoped<IContactRepository, ContactRepository>()
        .AddScoped<IPhoneRepository, PhoneRepository>()
        .AddScoped<IScoreRepository, ScoreRepository>()
        .AddScoped<IGroundwaterScoreRepository, GroundwaterScoreRepository>()
        .AddScoped<IOnsiteScoreRepository, OnsiteScoreRepository>()
        .AddScoped<ISubstanceRepository, SubstanceRepository>()
        .AddScoped<IStatusRepository, StatusRepository>()
        .AddScoped<IEventRepository, EventRepository>()
        .AddScoped<IReportingRepository, ReportingRepository>()
        .AddScoped<IUserPositionRepository, UserPositionRepository>()
        .AddScoped<IUserProgramRepository, UserProgramRepository>()
        .AddScoped<IDashboardRepository, DashboardRepository>()
        .AddScoped<IAllowedEventTypeRepository, AllowedEventTypeRepository>()
        .AddScoped<IAllowedEventTypeHelper, AllowedEventTypeHelper>();
}
