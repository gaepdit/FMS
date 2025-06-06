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
            if (!await context.ContactTitles.AnyAsync(token))
                await context.ContactTitles.AddRangeAsync(GetContactTitles(), token);
            if (!await context.ContactTypes.AnyAsync(token))
                await context.ContactTypes.AddRangeAsync(GetContactTypes(), token);
            if (!await context.ActionsTaken.AnyAsync(token))
                await context.ActionsTaken.AddRangeAsync(GetActionsTaken(), token);
            if (!await context.ContactTypes.AnyAsync(token))
                await context.ContactTypes.AddRangeAsync(GetContactTypes(), token);
            if (!await context.EventTypes.AnyAsync(token))
                await context.EventTypes.AddRangeAsync(GetEventTypes(), token);
            if (!await context.FundingSources.AnyAsync(token))
                await context.FundingSources.AddRangeAsync(GetFundingSources(), token);
            if (!await context.ParcelTypes.AnyAsync(token))
                await context.ParcelTypes.AddRangeAsync(GetParcelTypes(), token);
            if (!await context.ContactTitles.AnyAsync(token))
                await context.ContactTitles.AddRangeAsync(GetContactTitles(), token);
            if (!await context.OverallStatuses.AnyAsync(token))
                await context.OverallStatuses.AddRangeAsync(GetOverallStatuses(), token);
            if (!await context.SoilStatuses.AnyAsync(token))
                await context.SoilStatuses.AddRangeAsync(GetSoilStatuses(), token);
            if (!await context.SourceStatuses.AnyAsync(token))
                await context.SourceStatuses.AddRangeAsync(GetSourceStatuses(), token);
            if (!await context.AllowedActionsTaken.AnyAsync(token))
                await context.AllowedActionsTaken.AddRangeAsync(GetAllowedActionsTaken(), token);
            if (!await context.Facilities.AnyAsync(token))
                await context.Facilities.AddRangeAsync(GetFacilities(), token);
            if (!await context.RetentionRecords.AnyAsync(token))
                await context.RetentionRecords.AddRangeAsync(GetRetentionRecords(), token);
            if (!await context.HsrpFacilityProperties.AnyAsync(token))
                await context.HsrpFacilityProperties.AddRangeAsync(GetHsrpFacilityProperties(), token);
            if (!await context.Contacts.AnyAsync(token))
                await context.Contacts.AddRangeAsync(GetContacts(), token);
            if (!await context.Phones.AnyAsync(token))
                await context.Phones.AddRangeAsync(GetPhones(), token);
            if (!await context.Locations.AnyAsync(token))
                await context.Locations.AddRangeAsync(GetLocations(), token);
            if (!await context.Parcels.AnyAsync(token))
                await context.Parcels.AddRangeAsync(GetParcels(), token);
            if (!await context.Scores.AnyAsync(token))
                await context.Scores.AddRangeAsync(GetScores(), token);
            if (!await context.OnSiteScores.AnyAsync(token))
                await context.OnSiteScores.AddRangeAsync(GetOnSiteScores(), token);
            if (!await context.GroundwaterStatuses.AnyAsync(token))
                await context.GroundwaterStatuses.AddRangeAsync(GetGroundwaterStatuses(), token);
            if (!await context.GroundwaterScores.AnyAsync(token))
                await context.GroundwaterScores.AddRangeAsync(GetGroundwaterScores(), token);
            if (!await context.Substances.AnyAsync(token))
                await context.Substances.AddRangeAsync(GetSubstances(), token);
            if (!await context.Events.AnyAsync(token))
                await context.Events.AddRangeAsync(GetEvents(), token);


            await context.SaveChangesAsync(token);
        }
    }
}