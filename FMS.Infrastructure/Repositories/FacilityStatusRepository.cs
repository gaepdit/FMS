using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
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

        public async Task<int> CountAsync(FacilityStatusSpec spec)
        {
            return await _context.FacilityStatuses.AsNoTracking().CountAsync();
        }

        public async Task<Guid> CreateFacilityStatusAsync(FacilityStatusCreateDto facilityStatus)
        {
            if (facilityStatus == null)
            {
                throw new ArgumentException("Values required for new Facility Status.");
            }

            if (string.IsNullOrWhiteSpace(facilityStatus.Status))
            {
                throw new ArgumentException("New Name for Facility Status is required.");
            }

            return await CreatefacilityStatusInternalAsync(facilityStatus);
        }

        public async Task<Guid> CreatefacilityStatusInternalAsync(FacilityStatusCreateDto facilityStatus)
        {
            if (await FacilityStatusExistsAsync(facilityStatus.Status))
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

        public async Task<bool> FacilityStatusExistsAsync(string status) =>
            await _context.FacilityStatuses.AnyAsync(e => e.Status == status);

        public async Task<bool> FacilityStatusStatusExistsAsync(string facilityStatusStatus, Guid? ignoreId = null) => await _context.FacilityStatuses.AnyAsync(e => e.Status == facilityStatusStatus && (!ignoreId.HasValue || e.Id != ignoreId.Value));

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

        public async Task<IReadOnlyList<FacilityStatusSummaryDto>> GetFacilityStatusListAsync()
        {
            return await _context.FacilityStatuses.AsNoTracking()
                .OrderBy(e => e.Status)
                .Select(e => new FacilityStatusSummaryDto(e))
                .ToListAsync();
        }

        public Task UpdateFacilityStatusAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates)
        {
            if (string.IsNullOrWhiteSpace(facilityStatusUpdates.Status))
            {
                throw new ArgumentException("Facility Status Name is required.");
            }
            
            return UpdateFacilityStatusUpdatesInternalAsync(id, facilityStatusUpdates);
        }

        public async Task UpdateFacilityStatusUpdatesInternalAsync(Guid id, FacilityStatusEditDto facilityStatusUpdates)
        {
            var facilityStatus = await _context.FacilityStatuses.FindAsync(id);

            if (facilityStatus == null)
            {
                throw new ArgumentException("Facility Status ID not found.", nameof(id));
            }

            if (await FacilityStatusStatusExistsAsync(facilityStatus.Status, id))
            {
                throw new ArgumentException($"Facility Status '{facilityStatus.Status}' already exists.");
            }

            facilityStatus.Status = facilityStatusUpdates.Status;
            facilityStatus.Active = true;

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
