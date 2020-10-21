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
    public class FacilityTypeRepository : IFacilityTypeRepository
    {
        private readonly FmsDbContext _context;

        public FacilityTypeRepository(FmsDbContext context) => _context = context;

        public async Task<bool> FacilityTypeExistsAsync(Guid id) =>
            await _context.FacilityTypes.AnyAsync(e => e.Id == id);

        public async Task<bool> FacilityTypeExistsAsync(int code) =>
            await _context.FacilityTypes.AnyAsync(e => e.Code == code);

        public async Task<bool> FacilityTypeCodeExistsAsync(int facilityTypeCode, Guid? ignoreId = null) => await _context.FacilityTypes.AnyAsync(e => e.Code == facilityTypeCode && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<int> CountAsync(FacilityTypeSpec spec)
        {
            return await _context.FacilityTypes.AsNoTracking().CountAsync();
        }

        public async Task<FacilityTypeEditDto> GetFacilityTypeAsync(Guid id)
        {
            var facilityType = await _context.FacilityTypes.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (facilityType == null)
            {
                return null;
            }

            return new FacilityTypeEditDto(facilityType);
        }

        public async Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync()
        {
            return await _context.FacilityTypes.AsNoTracking()
                .OrderBy(e => e.Code)
                .Select(e => new FacilityTypeSummaryDto(e))
                .ToListAsync();
        }

        public async Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityType)
        {
            if (facilityType == null)
            {
                throw new ArgumentException("Values required for new Facility Type.");
            }

            if (facilityType.Code < 1)
            {
                throw new ArgumentException("New Code for Facility Type is required.");
            }

            if (string.IsNullOrWhiteSpace(facilityType.Name))
            {
                throw new ArgumentException("New Name for Budget Code is required.");
            }

            return await CreateFacilityTypeInternalAsync(facilityType);
        }

        public async Task<Guid> CreateFacilityTypeInternalAsync(FacilityTypeCreateDto facilityType)
        {
            if (await FacilityTypeExistsAsync(facilityType.Code))
            {
                throw new ArgumentException($"Budget Code {facilityType.Code} Already Exists.");
            }

            var newFT = new FacilityType(facilityType);

            await _context.FacilityTypes.AddAsync(newFT);
            await _context.SaveChangesAsync();

            return newFT.Id;
        }

        public Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates)
        {
            if (facilityTypeUpdates.Code < 1)
            {
                throw new ArgumentException("Budget Code Code is required.");
            }
            if (string.IsNullOrWhiteSpace(facilityTypeUpdates.Name))
            {
                throw new ArgumentException("Budget Code Name is required.");
            }
            return UpdateFacilityTypeInternalAsync(id, facilityTypeUpdates);
        }

        public async Task UpdateFacilityTypeInternalAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates)
        {
            var facilityType = await _context.FacilityTypes.FindAsync(id);

            if (facilityType == null)
            {
                throw new ArgumentException("Facility Type ID not found.", nameof(id));
            }

            if (await FacilityTypeCodeExistsAsync(facilityType.Code, id))
            {
                throw new ArgumentException($"Facility Type Code '{facilityType.Code}' already exists.");
            }

            facilityType.Code = facilityTypeUpdates.Code;
            facilityType.Name = facilityTypeUpdates.Name;
            facilityType.Active = true;

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
