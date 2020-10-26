using FMS.Infrastructure.Contexts;
using System.Linq;

namespace FMS.Infrastructure.SeedData
{
    public static partial class DevSeedData
    {
        public static void SeedTestData(this FmsDbContext context)
        {
            if (!context.BudgetCodes.Any()) context.BudgetCodes.AddRange(GetBudgetCodes());
            if (!context.ComplianceOfficers.Any()) context.ComplianceOfficers.AddRange(GetComplianceOfficers());
            if (!context.Facilities.Any()) context.Facilities.AddRange(GetFacilities());
            if (!context.FacilityStatuses.Any()) context.FacilityStatuses.AddRange(GetFacilityStatuses());
            if (!context.FacilityTypes.Any()) context.FacilityTypes.AddRange(GetFacilityTypes());
            if (!context.Cabinets.Any()) context.Cabinets.AddRange(GetCabinets());
            if (!context.Files.Any()) context.Files.AddRange(GetFiles());
            if (!context.OrganizationalUnits.Any()) context.OrganizationalUnits.AddRange(GetOrganizationalUnits());
            //if (!context.RetentionRecords.Any()) context.RetentionRecords.AddRange(GetRetentionRecords());
            if (!context.CabinetFileJoin.Any()) context.CabinetFileJoin.AddRange(GetCabinetFiles());

            context.SaveChanges();
        }
    }
}
