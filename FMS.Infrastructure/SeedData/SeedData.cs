using System.Linq;
using FMS.Infrastructure.Contexts;

namespace FMS.Infrastructure.SeedData
{
    public static partial class ProdData
    {
        public static void SeedData(this FmsDbContext context)
        {
            if (!context.FacilityStatuses.Any()) context.FacilityStatuses.AddRange(GetFacilityStatuses());
            if (!context.FacilityTypes.Any()) context.FacilityTypes.AddRange(GetFacilityTypes());
            if (!context.OrganizationalUnits.Any()) context.OrganizationalUnits.AddRange(GetOrganizationalUnits());

            context.SaveChanges();
        }
    }
}