using FMS.Infrastructure.Contexts;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static void SeedTestData(this FmsDbContext context)
        {
            context.AddRange(GetBudgetCodes());
            context.AddRange(GetComplianceOfficers());
            context.AddRange(GetCounties());
            context.AddRange(GetEnvironmentalInterests());
            context.AddRange(GetFacilities());
            context.AddRange(GetFacilityStatuses());
            context.AddRange(GetFacilityTypes());
            context.AddRange(GetFileCabinets());
            context.AddRange(GetFiles());
            context.AddRange(GetOrganizationalUnits());
            context.AddRange(GetRetentionRecords());
            //context.SaveChanges();
        }
    }
}
