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
    public class FacilityTypeRepository : IFacilityTypeRepository
    {
        private readonly FmsDbContext _context;
        public FacilityTypeRepository(FmsDbContext context) => _context = context;

        public async Task<bool> FacilityTypeExistsAsync(Guid id) =>
            await _context.FacilityTypes.AnyAsync(e => e.Id == id);

        public async Task<bool> FacilityTypeNameExistsAsync(string name, Guid? ignoreId = null) =>
            await _context.FacilityTypes.AnyAsync(e =>
                e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<bool> FacilityTypeDescriptionExistsAsync(string description, Guid? ignoreId = null) =>
            await _context.FacilityTypes.AnyAsync(e =>
                e.Description == description && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<FacilityTypeEditDto> GetFacilityTypeAsync(Guid id)
        {
            var facilityType = await _context.FacilityTypes.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return facilityType == null ? null : new FacilityTypeEditDto(facilityType);
        }

        public async Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync() =>
            await _context.FacilityTypes.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new FacilityTypeSummaryDto(e))
                .ToListAsync();

        public Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityType)
        {
            Prevent.Null(facilityType, nameof(facilityType));
            Prevent.NullOrEmpty(facilityType.Name, nameof(facilityType.Name));

            if (!FacilityType.IsValidFacilityTypeName(facilityType.Name))
            {
                throw new ArgumentException("The Facility Type Name is invalid.");
            }

            return CreateFacilityTypeInternalAsync(facilityType);
        }

        private async Task<Guid> CreateFacilityTypeInternalAsync(FacilityTypeCreateDto facilityType)
        {
            if (await FacilityTypeNameExistsAsync(facilityType.Name))
            {
                throw new ArgumentException($"Facility Type '{facilityType.Name}' already exists.");
            }

            if (await FacilityTypeDescriptionExistsAsync(facilityType.Description))
            {
                throw new ArgumentException($"Facility Type description '{facilityType.Description}' already exists.");
            }

            var newFacilityType = new FacilityType(facilityType);

            await _context.FacilityTypes.AddAsync(newFacilityType);
            await _context.SaveChangesAsync();

            return newFacilityType.Id;
        }

        public Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates)
        {
            Prevent.NullOrWhiteSpace(facilityTypeUpdates.Name, nameof(facilityTypeUpdates.Name));
            return UpdateFacilityTypeInternalAsync(id, facilityTypeUpdates);
        }

        private async Task UpdateFacilityTypeInternalAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates)
        {
            var facilityType = await _context.FacilityTypes.FindAsync(id);

            if (facilityType == null)
            {
                throw new ArgumentException("Facility Type ID not found.", nameof(id));
            }

            if (await FacilityTypeNameExistsAsync(facilityTypeUpdates.Name, id))
            {
                throw new ArgumentException($"Facility Type '{facilityTypeUpdates.Name}' already exists.");
            }

            if (await FacilityTypeDescriptionExistsAsync(facilityTypeUpdates.Description, id))
            {
                throw new ArgumentException(
                    $"Facility Type description '{facilityTypeUpdates.Description}' already exists.");
            }

            facilityType.Name = facilityTypeUpdates.Name;
            facilityType.Description = facilityTypeUpdates.Description;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateFacilityTypeStatusAsync(Guid id, bool active)
        {
            var facilityType = await _context.FacilityTypes.FindAsync(id);

            if (facilityType == null)
            {
                throw new ArgumentException("Facility Type ID not found");
            }

            facilityType.Active = active;

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
        ~FacilityTypeRepository()
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