using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using FMS.Domain.Entities.Users;    

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static async Task SeedDataAsync(this FmsDbContext context, CancellationToken token = default)
        {
            await SeedIfEmpty(context.FacilityStatuses, GetFacilityStatuses());
            await SeedIfEmpty(context.FacilityTypes, GetFacilityTypes());
            await SeedIfEmpty(context.UserPositions, GetUserPositions());
            await SeedIfEmpty(context.UserPrograms, GetUserPrograms());
            await SeedIfEmpty(context.OrganizationalUnits, GetOrganizationalUnits());
            await SeedIfEmpty(context.BudgetCodes, GetBudgetCodes());
            await SeedIfEmpty(context.ComplianceOfficers, GetComplianceOfficers());
            await SeedIfEmpty(context.Cabinets, GetCabinets());
            await SeedIfEmpty(context.Files, GetFiles());
            await SeedIfEmpty(context.Chemicals, GetChemicals());
            await SeedIfEmpty(context.ContactTypes, GetContactTypes());
            await SeedIfEmpty(context.ActionsTaken, GetActionsTaken());
            await SeedIfEmpty(context.AbandonedInactives, GetAbandonedInactives());
            await SeedIfEmpty(context.EventTypes, GetEventTypes());
            await SeedIfEmpty(context.EventContractors, GetEventContractors());
            await SeedIfEmpty(context.FundingSources, GetFundingSources());
            await SeedIfEmpty(context.LocationClasses, GetLocationClasses());
            await SeedIfEmpty(context.ParcelTypes, GetParcelTypes());
            await SeedIfEmpty(context.OverallStatuses, GetOverallStatuses());
            await SeedIfEmpty(context.SoilStatuses, GetSoilStatuses());
            await SeedIfEmpty(context.SourceStatuses, GetSourceStatuses());
            await SeedIfEmpty(context.AllowedActionsTaken, GetAllowedActionsTaken());
            await SeedIfEmpty(context.GapsAssessments, GetGapsAssessments());
            await SeedIfEmpty(context.Facilities, GetFacilities());
            await SeedIfEmpty(context.RetentionRecords, GetRetentionRecords());
            await SeedIfEmpty(context.HsrpFacilityProperties, GetHsrpFacilityProperties());
            await SeedIfEmpty(context.Contacts, GetContacts());
            await SeedIfEmpty(context.Phones, GetPhones());
            await SeedIfEmpty(context.Locations, GetLocations());
            await SeedIfEmpty(context.Parcels, GetParcels());
            await SeedIfEmpty(context.Scores, GetScores());
            await SeedIfEmpty(context.OnsiteScores, GetOnSiteScores());
            await SeedIfEmpty(context.GroundwaterStatuses, GetGroundwaterStatuses());
            await SeedIfEmpty(context.GroundwaterScores, GetGroundwaterScores());
            await SeedIfEmpty(context.Substances, GetSubstances());
            await SeedIfEmpty(context.Statuses, GetStatuses());
            await SeedIfEmpty(context.Events, GetEvents());


            await context.SaveChangesAsync(token);
            return;

            async Task SeedIfEmpty<T>(DbSet<T> table, List<T> data) where T : class
            {
                if (!await table.AnyAsync(token)) await table.AddRangeAsync(data, token);
            }
        }
    }
}
