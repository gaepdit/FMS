using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class AllowedEventTypeRepository : IAllowedEventTypeRepository
    {
        public readonly FmsDbContext _context;
        public AllowedEventTypeRepository(FmsDbContext context) => _context = context;

        public Task<bool> AllowedEventTypeExistsAsync(Guid id) =>
            _context.AllowedEventTypes.AnyAsync(e => e.Id == id);

        public Task<bool> AllowedEventTypeExistsAsync(Guid FacilityTypeId, Guid eventTypeId) =>
            _context.AllowedEventTypes.AnyAsync(e => e.FacilityTypeId == FacilityTypeId && e.EventTypeId == eventTypeId);

        public async Task<AllowedEventTypeSpec> GetAllowedEventTypeByAETIdAsync(Guid? id)
        {
            if (!id.HasValue)
            {
                return null;
            }
            if (!await AllowedEventTypeExistsAsync(id.Value))
            {
                return null;
            }
            AllowedEventType allowedEventType = await _context.AllowedEventTypes
                .AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.EventType)
                .SingleOrDefaultAsync(e => e.Id == id.Value);
            return new AllowedEventTypeSpec(allowedEventType);
        }

        public async Task<AllowedEventTypeSpec> GetAllowedEventTypeByFacilityTypeAndEventTypeAsync(Guid FacilityTypeId, Guid eventTypeId)
        {
            if (!await AllowedEventTypeExistsAsync(FacilityTypeId, eventTypeId))
            {
                return null;
            }
            AllowedEventType allowedEventType = await _context.AllowedEventTypes
                .AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.EventType)
                .SingleOrDefaultAsync(e => e.FacilityTypeId == FacilityTypeId && e.EventTypeId == eventTypeId);
            return new AllowedEventTypeSpec(allowedEventType);
        }

        public async Task<IList<AllowedEventTypeSpec>> GetAllowedEventTypeListAsync(Guid facilityTypeId)
        {
            return await _context.AllowedEventTypes.AsNoTracking()
               .Include(e => e.EventType)
               .Include(e => e.FacilityType)
               .Where(e => e.FacilityTypeId == facilityTypeId)
               .OrderByDescending(e => e.Active)
               .ThenBy(e => e.FacilityType.Name)
               .Select(e => new AllowedEventTypeSpec()
               {
                   Id = e.Id,
                   FacilityTypeId = e.FacilityTypeId,
                   FacilityTypeName = e.FacilityType.Name,
                   FacilityTypeActive = e.FacilityType.Active,
                   EventTypeId = e.EventTypeId,
                   EventTypeName = e.EventType.Name,
                   EventTypeActive = e.EventType.Active,
                   Active = e.Active
               })
               .ToListAsync();
        }

        public async Task<Guid> CreateAllowedEventTypeAsync(AllowedEventTypeSpec allowedEventType)
        {
            Prevent.Null(allowedEventType, nameof(allowedEventType));
            Prevent.NullOrEmpty(allowedEventType.EventTypeId, nameof(allowedEventType.EventTypeId));
            Prevent.NullOrEmpty(allowedEventType.FacilityTypeId, nameof(allowedEventType.FacilityTypeId));

            if (await AllowedEventTypeExistsAsync(allowedEventType.FacilityTypeId, allowedEventType.EventTypeId))
            {
                throw new ArgumentException($"Allowed Event Type already exists.");
            }

            var newAllowedEventType = new AllowedEventType
            {
                FacilityTypeId = allowedEventType.FacilityTypeId,
                EventTypeId = allowedEventType.EventTypeId,
                Active = true
            };
            await _context.AllowedEventTypes.AddAsync(newAllowedEventType);
            await _context.SaveChangesAsync();
            return newAllowedEventType.Id;
        }

        public async Task<Guid> UpdateAllowedEventTypeAsync(AllowedEventTypeSpec allowedEventType)
        {
            var entity = await _context.AllowedEventTypes.FindAsync(allowedEventType.Id);
            if (entity == null)
            {
                return Guid.Empty;
            }
            entity.FacilityTypeId = allowedEventType.FacilityTypeId;
            entity.EventTypeId = allowedEventType.EventTypeId;
            entity.Active = allowedEventType.Active;

            _context.AllowedEventTypes.Update(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<Guid> DeleteAllowedEventTypeAsync(Guid? id)
        {
            if (!await AllowedEventTypeExistsAsync(id.Value))
            {
                throw new ArgumentException("No Allowed Event Type records found.");
            }
            else
            {
                if (await GetAllowedEventTypeByAETIdAsync(id.Value) != null)
                {
                    var existingAllowedEventType = await _context.AllowedEventTypes.FindAsync(id.Value);
                    _context.AllowedEventTypes.Remove(existingAllowedEventType);
                    await _context.SaveChangesAsync();
                    return id.Value;
                }
                else
                {
                    throw new ArgumentException($"Allowed Event Type with Id {id.Value} does not exist.");
                }
            }
        }
    }
}
