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
    public class FacilityStatusRepository : IFacilityStatusRepository
    {
        private readonly FmsDbContext _context;
        public FacilityStatusRepository(FmsDbContext context) => _context = context;

        public Task<Guid> CreateFacilityStatusAsync(FacilityStatusCreateDto facilityStatus)
        {
            Prevent.Null(facilityStatus, nameof(facilityStatus));

            if (string.IsNullOrWhiteSpace(facilityStatus.Status))
            {
                throw new ArgumentException("New Name for Facility Status is required.");
            }

            return CreateFacilityStatusInternalAsync(facilityStatus);
        }

        private async Task<Guid> CreateFacilityStatusInternalAsync(FacilityStatusCreateDto facilityStatus)
        {
            if (await FacilityStatusStatusExistsAsync(facilityStatus.Status))
            {
                throw new ArgumentException($"Facility Status {facilityStatus.Status} Already Exists.");
            }

            var newFS = new FacilityStatus(facilityStatus);

            await _context.FacilityStatuses.AddAsync(newFS);
            await _context.SaveChangesAsync();

            return newFS.Id;
        }

        public async Task<bool> FacilityStatusExistsAsync(Guid id) =>
            await _context.FacilityStatuses.AnyAsync(e => e.Id == id);

        public async Task<bool> FacilityStatusStatusExistsAsync(string status, Guid? ignoreId = null) =>
            await _context.FacilityStatuses.AnyAsync(e =>
                e.Status == status && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<FacilityStatusEditDto> GetFacilityStatusAsync(Guid id)
        {
            var facilityStatus = await _context.FacilityStatuses.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (facilityStatus == null)
            {
                return null;
            }

            return new FacilityStatusEditDto(facilityStatus);
        }

        public async Task<IReadOnlyList<FacilityStatusSummaryDto>> GetFacilityStatusListAsync() =>
            await _context.FacilityStatuses.AsNoTracking()
                .OrderBy(e => e.Status)
                .Select(e => new FacilityStatusSummaryDto(e))
                .ToListAsync();

        public Task UpdateFacilityStatusAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates)
        {
            if (string.IsNullOrWhiteSpace(facilityStatusUpdates.Status))
            {
                throw new ArgumentException("Facility Status Name is required.");
            }

            return UpdateFacilityStatusInternalAsync(id, facilityStatusUpdates);
        }

        private async Task UpdateFacilityStatusInternalAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates)
        {
            var facilityStatus = await _context.FacilityStatuses.FindAsync(id);

            if (facilityStatus == null)
            {
                throw new ArgumentException("Facility Status ID not found.", nameof(id));
            }

            if (await FacilityStatusStatusExistsAsync(facilityStatusUpdates.Status, id))
            {
                throw new ArgumentException($"Facility Status '{facilityStatusUpdates.Status}' already exists.");
            }

            facilityStatus.Status = facilityStatusUpdates.Status;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateFacilityStatusStatusAsync(Guid id, bool active)
        {
            var facilityStatus = await _context.FacilityStatuses.FindAsync(id);

            if (facilityStatus == null)
            {
                throw new ArgumentException("Facility Status ID not found");
            }

            facilityStatus.Active = active;

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
        ~FacilityStatusRepository()
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