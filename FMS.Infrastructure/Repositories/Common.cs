using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public static class Common
    {
        public static async Task<IReadOnlyList<CabinetSummaryDto>> GetCabinetListAsync(
            this FmsDbContext context, bool includeInactive = true)
        {
            var cabinets = await context.Cabinets.AsNoTracking()
                .Where(e => e.Active || includeInactive)
                .OrderBy(e => e.FirstFileLabel)
                .ThenBy(e => e.Name)
                .Select(e => new CabinetSummaryDto(e)).ToListAsync();

            // loop through all the cabinets except the last one and set last file label
            for (var i = 0; i < cabinets.Count - 1; i++)
            {
                cabinets[i].LastFileLabel = cabinets[i + 1].FirstFileLabel;
            }

            return cabinets;
        }

        public static async Task<HsrpFacilityProperties> GetHsrpPropertiesAsync(this FmsDbContext context, Guid facilityId)
        {
            return  await context.HsrpFacilityProperties
                    .AsNoTracking()
                    .Include(e => e.OrganizationalUnit)
                    .Include(e => e.ComplianceOfficer)
                    .Where(e => e.FacilityId == facilityId)
                    .FirstOrDefaultAsync();

        }
    }
}