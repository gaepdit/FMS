using System.Linq;
using FMS.Infrastructure.Contexts;

namespace FMS.Infrastructure.SeedData.TestData
{
    public static partial class TestData
    {
        public static void SeedTestData(this FmsDbContext context)
        {
            if (!context.BudgetCodes.Any()) context.BudgetCodes.AddRange(GetBudgetCodes());
            if (!context.ComplianceOfficers.Any()) context.ComplianceOfficers.AddRange(GetComplianceOfficers());
            if (!context.Cabinets.Any()) context.Cabinets.AddRange(GetCabinets());
            if (!context.Files.Any()) context.Files.AddRange(GetFiles());
            if (!context.Facilities.Any()) context.Facilities.AddRange(GetFacilities());
            if (!context.RetentionRecords.Any()) context.RetentionRecords.AddRange(GetRetentionRecords());

            context.SaveChanges();
        }
    }
}
