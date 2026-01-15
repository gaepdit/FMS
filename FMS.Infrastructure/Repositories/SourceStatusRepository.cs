using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class SourceStatusRepository : ISourceStatusRepository
    {
        private readonly FmsDbContext _context;
        public SourceStatusRepository(FmsDbContext context) => _context = context;

        public Task<bool> SourceStatusExistsAsync(Guid id) =>
            _context.SourceStatuses.AnyAsync(e => e.Id == id);

        public Task<bool> SourceStatusNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.SourceStatuses.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> SourceStatusDescriptionExistsAsync(string description, Guid? ignoreId = null) =>
            _context.SourceStatuses.AnyAsync(e =>
                e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<SourceStatusEditDto> GetSourceStatusAsync(Guid id)
        {
            var sourceStatus = await _context.SourceStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
            return sourceStatus == null ? null : new SourceStatusEditDto(sourceStatus);
        }

        public async Task<SourceStatusEditDto> GetSourceStatusByNameAsync(string name)
        {
            var sourceStatus = await _context.SourceStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Name == name);
            return sourceStatus == null ? null : new SourceStatusEditDto(sourceStatus);
        }

        public async Task<IReadOnlyList<SourceStatusSummaryDto>> GetSourceStatusListAsync() =>
            await _context.SourceStatuses.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new SourceStatusSummaryDto(e))
            .ToListAsync();

        public Task<bool> CreateSourceStatusAsync(SourceStatusCreateDto sourceStatus)
        {
            Prevent.Null(sourceStatus, nameof(sourceStatus));
            Prevent.NullOrEmpty(sourceStatus.Name, nameof(sourceStatus.Name));

            return CreateSourceStatusInternalAsync(sourceStatus);
        }

        private async Task<bool> CreateSourceStatusInternalAsync(SourceStatusCreateDto sourceStatus)
        {
            if (await SourceStatusNameExistsAsync(sourceStatus.Name))
            {
                throw new ArgumentException($"The Source Status Name: '{sourceStatus.Name}' is invalid.");
            }

            var newSourceStatus = new SourceStatus
            {
                Id = Guid.NewGuid(),
                Name = sourceStatus.Name,
                Description = sourceStatus.Description
            };

            _context.SourceStatuses.Add(newSourceStatus);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task UpdateSourceStatusAsync(Guid id, SourceStatusEditDto sourceStatusUpdates)
        {
            Prevent.Null(sourceStatusUpdates, nameof(sourceStatusUpdates));
            Prevent.NullOrEmpty(sourceStatusUpdates.Name, nameof(sourceStatusUpdates.Name));

            return UpdateSourceStatusInternalAsync(id, sourceStatusUpdates);
        }

        private async Task UpdateSourceStatusInternalAsync(Guid id, SourceStatusEditDto sourceStatusUpdates)
        {
            var sourceStatus = await _context.SourceStatuses.FindAsync(id);

            if (sourceStatus == null)
            {
                throw new KeyNotFoundException($"Source Status with ID {id} not found.");
            }

            if (await SourceStatusNameExistsAsync(sourceStatus.Name, id))
            {
                throw new ArgumentException($"The Source Status Name: '{sourceStatus.Name}' already exists.");
            }

            if (await SourceStatusDescriptionExistsAsync(sourceStatus.Description, id))
            {
                throw new ArgumentException($"The Source Status Description: '{sourceStatus.Description}' already exists.");
            }

            sourceStatus.Name = sourceStatusUpdates.Name;
            sourceStatus.Description = sourceStatusUpdates.Description;

            _context.SourceStatuses.Update(sourceStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSourceStatusStatusAsync(Guid id, bool active)
        {
            var sourceStatus = await _context.SourceStatuses.FindAsync(id);

            if (sourceStatus == null)
            {
                throw new KeyNotFoundException($"Source Status with ID {id} not found.");
            }

            sourceStatus.Active = active;

            _context.SourceStatuses.Update(sourceStatus);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support

        private bool _disposedValue;

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
        ~SourceStatusRepository()
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
