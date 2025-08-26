using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class ParcelRepository : IParcelRepository
    {
        private readonly FmsDbContext _context;
        public ParcelRepository(FmsDbContext context) => _context = context;

        public Task<bool> ParcelExistsAsync(Guid id) =>
            _context.Parcels.AnyAsync(e => e.Id == id);

        public async Task<ParcelEditDto> GetParcelByIdAsync(Guid id)
            {
            var parcel = await _context.Parcels.AsNoTracking()
                .Include(e => e.ParcelType)
                .SingleOrDefaultAsync(e => e.Id == id);

            return parcel == null ? null : new ParcelEditDto(parcel);
        }

        public async Task<IReadOnlyList<ParcelSummaryDto>> GetParcelListAsync(Guid facilityId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));

            if (facilityId == Guid.Empty)
                throw new ArgumentException("Facility ID cannot be empty.", nameof(facilityId));

            return await _context.Parcels.AsNoTracking()
                .OrderByDescending(e => e.Active)
                .ThenBy(e => e.ListDate)
                .ThenBy(e => e.DeListDate)
                .Include(e => e.ParcelType)
                .Where(e => e.FacilityId == facilityId)
                .Select(e => new ParcelSummaryDto(e))
                .ToListAsync();
        }

        public Task<Guid> CreateParcelAsync(ParcelCreateDto parcelCreate)
        {
            Prevent.Null(parcelCreate, nameof(parcelCreate));
            Prevent.NullOrEmpty(parcelCreate.ParcelNumber, nameof(parcelCreate.ParcelNumber));

            return CreateParcelInternalAsync(parcelCreate);
        }

        public async Task<Guid> CreateParcelInternalAsync(ParcelCreateDto parcelCreate)
        {
            var newParcel = new Parcel
            {
                Id = Guid.NewGuid(),
                Active = true,
                FacilityId = parcelCreate.FacilityId,
                ParcelNumber = parcelCreate.ParcelNumber,
                Acres = parcelCreate.Acres,
                ParcelTypeId = parcelCreate.ParcelTypeId,
                ListDate = parcelCreate.ListDate,
                DeListDate = parcelCreate.DeListDate,
                SubListParcelName = parcelCreate.SubListParcelName
            };

            _context.Parcels.Add(newParcel);
            await _context.SaveChangesAsync();
            return newParcel.Id;
        }

        public async Task UpdateParcelAsync(Guid id, ParcelEditDto parcelUpdates)
        {
            Prevent.Null(parcelUpdates, nameof(parcelUpdates));
            Prevent.NullOrEmpty(id, nameof(id));

            await UpdateParcelInternalAsync(id, parcelUpdates);
        }

        public async Task UpdateParcelInternalAsync(Guid id, ParcelEditDto parcelUpdates)
        {
            var existingParcel = await _context.Parcels.FindAsync(id);

            if (existingParcel == null) throw new InvalidOperationException($"Parcel with ID {id} not found.");

            existingParcel.Active = parcelUpdates.Active;
            existingParcel.FacilityId = parcelUpdates.FacilityId;
            existingParcel.ParcelNumber = parcelUpdates.ParcelNumber;
            existingParcel.Acres = parcelUpdates.Acres;
            existingParcel.ParcelTypeId = parcelUpdates.ParcelTypeId;
            existingParcel.ListDate = parcelUpdates.ListDate;
            existingParcel.DeListDate = parcelUpdates.DeListDate;
            existingParcel.SubListParcelName = parcelUpdates.SubListParcelName;

            _context.Parcels.Update(existingParcel);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateParcelStatusAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));

            var existingParcel = await _context.Parcels.FindAsync(id);

            if (existingParcel == null) throw new InvalidOperationException($"Parcel with ID {id} not found.");

            existingParcel.Active = active;

            _context.Parcels.Update(existingParcel);
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
        ~ParcelRepository()
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
