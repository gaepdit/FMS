using System.Threading;
using System.Threading.Tasks;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        public static async Task SeedDataAsync(this FmsDbContext context, CancellationToken token)
        {
            if (!await context.FacilityStatuses.AnyAsync(token))
                await context.FacilityStatuses.AddRangeAsync(GetFacilityStatuses(), token);
            if (!await context.FacilityTypes.AnyAsync(token))
                await context.FacilityTypes.AddRangeAsync(GetFacilityTypes(), token);
            if (!await context.OrganizationalUnits.AnyAsync(token))
                await context.OrganizationalUnits.AddRangeAsync(GetOrganizationalUnits(), token);
            if (!await context.BudgetCodes.AnyAsync(token))
                await context.BudgetCodes.AddRangeAsync(GetBudgetCodes(), token);
            if (!await context.ComplianceOfficers.AnyAsync(token))
                await context.ComplianceOfficers.AddRangeAsync(GetComplianceOfficers(), token);
            if (!await context.Cabinets.AnyAsync(token))
                await context.Cabinets.AddRangeAsync(GetCabinets(), token);
            if (!await context.Files.AnyAsync(token))
                await context.Files.AddRangeAsync(GetFiles(), token);
            if (!await context.Chemicals.AnyAsync(token))
                await context.Chemicals.AddRangeAsync(GetChemicals(), token);
            if (!await context.Facilities.AnyAsync(token))
                await context.Facilities.AddRangeAsync(GetFacilities(), token);
            if (!await context.RetentionRecords.AnyAsync(token))
                await context.RetentionRecords.AddRangeAsync(GetRetentionRecords(), token);

            await context.SaveChangesAsync(token);
        }
    }
}