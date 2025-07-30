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
    public class ParcelTypeRepository : IParcelTypeRepository
    {
        private readonly FmsDbContext _context;
        public ParcelTypeRepository(FmsDbContext context) => _context = context;

        public Task<bool> ParcelTypeExistsAsync(Guid id) =>
            _context.ParcelTypes.AnyAsync(e => e.Id == id);

        public Task<bool> ParcelTypeNameExistsAsync(string name, Guid? ignoreId = null) => _context.ParcelTypes.AnyAsync(e => e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<ParcelTypeEditDto> GetParcelTypeAsync(Guid id)
        {
            var parcelType = await _context.ParcelTypes.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return parcelType == null ? null : new ParcelTypeEditDto(parcelType);
        }

        public async Task<IReadOnlyList<ParcelTypeSummaryDto>> GetParcelTypeListAsync() =>
            await _context.ParcelTypes.AsNoTracking()
            .OrderByDescending(e => e.Active)
            .ThenBy(e => e.Name)
            .Select(e => new ParcelTypeSummaryDto(e))
            .ToListAsync();

        public Task<Guid> CreateParcelTypeAsync(ParcelTypeCreateDto parcelType)
        {
            Prevent.Null(parcelType, nameof(parcelType));
            Prevent.NullOrEmpty(parcelType.Name, nameof(parcelType.Name));

            return CreateParcelTypeInternalAsync(parcelType);
        }

        private async Task<Guid> CreateParcelTypeInternalAsync(ParcelTypeCreateDto parcelType)
        {
            if (await ParcelTypeNameExistsAsync(parcelType.Name))
            {
                throw new ArgumentException($"Parcel Type Name: {parcelType.Name} Already Exists.");
            }

            var newParcelType = new ParcelType(parcelType);

            await _context.ParcelTypes.AddAsync(newParcelType);
            await _context.SaveChangesAsync();
            return newParcelType.Id;
        }

        public async Task UpdateParcelTypeAsync(Guid Id, ParcelTypeEditDto parcelTypeUpdates)
        {
            Prevent.Null(parcelTypeUpdates, nameof(parcelTypeUpdates));
            Prevent.NullOrEmpty(parcelTypeUpdates.Name, nameof(parcelTypeUpdates.Name));

            if (!await ParcelTypeExistsAsync(Id))
            {
                throw new ArgumentException($"Parcel Type with Id {Id} does not exist.");
            }

            if (await ParcelTypeNameExistsAsync(parcelTypeUpdates.Name, Id))
            {
                throw new ArgumentException($"Parcel Type Name: {parcelTypeUpdates.Name} Already Exists.");
            }
            var existingParcelType = await _context.ParcelTypes.FindAsync(Id);

            existingParcelType.Name = parcelTypeUpdates.Name;

            await _context.SaveChangesAsync();
        }
        public async Task UpdateParcelTypeStatusAsync(Guid id, bool active)
        {
            if (!await ParcelTypeExistsAsync(id))
            {
                throw new ArgumentException($"Parcel Type with Id {id} does not exist.");
            }
            var existingParcelType = await _context.ParcelTypes.FindAsync(id);

            existingParcelType.Active = active;

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
        ~ParcelTypeRepository()
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
