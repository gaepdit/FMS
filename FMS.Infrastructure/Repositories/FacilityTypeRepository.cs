﻿using FMS.Domain.Dto;
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

        public Task<int> CountAsync(FacilityTypeSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateFacilityTypeAsync(FacilityTypeCreateDto facilityStatus)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FacilityTypeExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FacilityTypeDetailDto> GetFacilityTypeAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<FacilityTypeSummaryDto>> GetFacilityTypeListAsync()
        {
            return await _context.FacilityTypes.AsNoTracking()
                .OrderBy(e => e.Code)
                .Select(e => new FacilityTypeSummaryDto(e))
                .ToListAsync();
        }

        public Task UpdateFacilityTypeAsync(Guid id, FacilityTypeEditDto facilityTypeUpdates)
        {
            throw new NotImplementedException();
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
