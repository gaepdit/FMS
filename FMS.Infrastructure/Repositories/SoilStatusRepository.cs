using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class SoilStatusRepository : ISoilStatusRepository
    {
        private readonly FmsDbContext _context;
        public SoilStatusRepository(FmsDbContext context) => _context = context;

        public Task<bool> SoilStatusExistsAsync(Guid id) =>
            _context.SoilStatuses.AnyAsync(e => e.Id == id);

        public Task<bool> SoilStatusNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.SoilStatuses.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public Task<bool> SoilStatusDescriptionExistsAsync(string description, Guid? ignoreId = null) =>
            _context.SoilStatuses.AnyAsync(e =>
                e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<SoilStatusEditDto> GetSoilStatusAsync(Guid id)
        {
            var soilStatus = await _context.SoilStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
            return soilStatus == null ? null : new SoilStatusEditDto(soilStatus);
        }

        public async Task<SoilStatusEditDto> GetSoilStatusByNameAsync(string name)
        {
            var soilStatus = await _context.SoilStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Name == name);
            return soilStatus == null ? null : new SoilStatusEditDto(soilStatus);
        }

        public async Task<List<SoilStatusSummaryDto>> GetSoilStatusListAsync() =>
            await _context.SoilStatuses.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new SoilStatusSummaryDto(e))
            .ToListAsync();

        public Task<bool> CreateSoilStatusAsync(SoilStatusCreateDto soilStatus)
        {
            Prevent.Null(soilStatus, nameof(soilStatus));
            Prevent.NullOrEmpty(soilStatus.Name, nameof(soilStatus.Name));

            return CreateSoilStatusInternalAsync(soilStatus);
        }

        private async Task<bool> CreateSoilStatusInternalAsync(SoilStatusCreateDto soilStatus)
        {
            if (await SoilStatusNameExistsAsync(soilStatus.Name))
            {
                throw new ArgumentException($"The Soil Status Name: '{soilStatus.Name}' is invalid.");
            }

            var newSoilStatus = new SoilStatus
            {
                Id = Guid.NewGuid(),
                Name = soilStatus.Name,
                Description = soilStatus.Description,
            };

            _context.SoilStatuses.Add(newSoilStatus);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task UpdateSoilStatusAsync(Guid id, SoilStatusEditDto soilStatusUpdates)
        {
            Prevent.Null(soilStatusUpdates, nameof(soilStatusUpdates));
            Prevent.NullOrEmpty(soilStatusUpdates.Name, nameof(soilStatusUpdates.Name));

            return UpdateSoilStatusInternalAsync(id, soilStatusUpdates);
        }

        private async Task UpdateSoilStatusInternalAsync(Guid id, SoilStatusEditDto soilStatusUpdates)
        {
            var soilStatus = await _context.SoilStatuses.FindAsync(id);

            if (soilStatus == null)
            {
                throw new KeyNotFoundException($"Soil Status with ID {id} not found.");
            }

            if (await SoilStatusNameExistsAsync(soilStatusUpdates.Name, id))
            {
                throw new ArgumentException($"The Soil Status Name: '{soilStatusUpdates.Name}' already exists.");
            }

            if (await SoilStatusDescriptionExistsAsync(soilStatusUpdates.Description, id))
            {
                throw new ArgumentException($"The Soil Status Description: '{soilStatusUpdates.Description}' already exists.");
            }

            soilStatus.Name = soilStatusUpdates.Name;
            soilStatus.Description = soilStatusUpdates.Description;

            _context.SoilStatuses.Update(soilStatus);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSoilStatusStatusAsync(Guid id, bool active)
        {
            var soilStatus = await _context.SoilStatuses.FindAsync(id);

            if (soilStatus == null)
            {
                throw new KeyNotFoundException($"Soil Status with ID {id} not found.");
            }

            soilStatus.Active = active;

            _context.SoilStatuses.Update(soilStatus);
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
        ~SoilStatusRepository()
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
