using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class FacilityRepository : IFacilityRepository
    {
        private readonly FmsDbContext _context;

        public FacilityRepository(FmsDbContext context) => _context = context;

        public async Task<bool> FacilityExistsAsync(Guid id)
        {
            return await _context.Facilities.AnyAsync(e => e.Id == id);
        }

        public async Task<FacilityDetailDto> GetFacilityAsync(Guid id)
        {
            var facility = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .SingleOrDefaultAsync(e => e.Id == id);

            return new FacilityDetailDto(facility);
        }

        public Task<int> CountAsync(FacilitySpec spec)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<FacilitySummaryDto>> GetFacilityListAsync(FacilitySpec spec)
        {
            return await _context.Facilities.AsNoTracking()
                .Where(e => string.IsNullOrEmpty(spec.Name) || e.Name.Contains(spec.Name))
                .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
                .Where(e => !spec.Active.HasValue || e.Active == spec.Active.Value)
                .Select(e => new FacilitySummaryDto(e))
                .ToListAsync();
        }

        public Task<bool> CreateFacilityAsync(FacilityCreateDto facility)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateFacilityAsync(Guid id, FacilityEditDto facilityUpdates)
        {
            var facility = await _context.Facilities.FindAsync(id);
            if (facility == null)
            {
                throw new ArgumentException("Facility ID not found", nameof(id));
            }

            facility.Active = facilityUpdates.Active;
            facility.CountyId = facilityUpdates.CountyId;
            facility.FacilityNumber = facilityUpdates.FacilityNumber;
            facility.Name = facilityUpdates.Name;

            await _context.SaveChangesAsync();
        }
    }
}
