using FMS.Infrastructure.Contexts;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static void SeedTestData(this FmsDbContext context)
        {
            context.BudgetCodes.AddRange(GetBudgetCodes());
            context.ComplianceOfficers.AddRange(GetComplianceOfficers());
            context.EnvironmentalInterests.AddRange(GetEnvironmentalInterests());
            context.Facilities.AddRange(GetFacilities());
            context.FacilityStatuses.AddRange(GetFacilityStatuses());
            context.FacilityTypes.AddRange(GetFacilityTypes());
            context.FileCabinets.AddRange(GetFileCabinets());
            context.Files.AddRange(GetFiles());
            context.OrganizationalUnits.AddRange(GetOrganizationalUnits());
            context.RetentionRecords.AddRange(GetRetentionRecords());
            context.SaveChanges();
        }
    }
}
