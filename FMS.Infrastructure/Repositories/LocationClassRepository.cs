using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class LocationClassRepository : ILocationClassRepository
    {
        private readonly FmsDbContext _context;
        public LocationClassRepository(FmsDbContext context) => _context = context;

        public Task<bool> LocationClassExistsAsync(Guid id) =>
                   _context.LocationClasses.AnyAsync(e => e.Id == id);

        public Task<bool> LocationClassNameExistsAsync(string name, Guid? ignoreId = null)
            => _context.LocationClasses.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<LocationClassEditDto> GetLocationClassAsync(Guid id)
            {
            var locationClass = await _context.LocationClasses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (locationClass == null)
            {
                return null;
            }
            return new LocationClassEditDto(locationClass);
        }

        public async Task<string> GetLocationClassNameAsync(Guid? id)
        {
            if (!id.HasValue) return null;
            var locationClass = await _context.LocationClasses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id.Value);
            return locationClass?.Name;
        }

        public async Task<IReadOnlyList<LocationClassSummaryDto>> GetLocationClassListAsync() =>
            await _context.LocationClasses.AsNoTracking()
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.Name)
                .Select(e => new LocationClassSummaryDto(e))
                .ToListAsync();

        public async Task<Guid> CreateLocationClassAsync(LocationClassCreateDto locationClass)
            {
            Prevent.Null(locationClass, nameof(locationClass));
            var entity = new LocationClass
            {
                Id = Guid.NewGuid(),
                Name = locationClass.Name,
                Description = locationClass.Description,
                Active = locationClass.Active
            };
            _context.LocationClasses.Add(entity);
            await _context.SaveChangesAsync();
            return entity.Id;
        }

        public async Task UpdateLocationClassAsync(Guid id, LocationClassEditDto locationClassUpdates)
        {
            Prevent.Null(locationClassUpdates, nameof(locationClassUpdates));
            var entity = await _context.LocationClasses
                .SingleOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new ArgumentException($"Location Class with Id '{id}' not found.", nameof(id));
            }
            entity.Name = locationClassUpdates.Name;
            entity.Description = locationClassUpdates.Description;
            entity.Active = locationClassUpdates.Active;
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLocationClassStatusAsync(Guid id, bool active)
        {
            var entity = await _context.LocationClasses
                .SingleOrDefaultAsync(e => e.Id == id);
            if (entity == null)
            {
                throw new ArgumentException($"Location Class with Id '{id}' not found.", nameof(id));
            }
            entity.Active = active;
            await _context.SaveChangesAsync();
        }


        #region IDisposable Support

        private bool _disposedValue; // Corrected: 'private' modifier now precedes the member type

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~LocationClassRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
