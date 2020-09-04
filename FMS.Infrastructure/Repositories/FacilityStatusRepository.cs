using FMS.Domain.Dto;
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

        public Task<int> CountAsync(FacilityStatusSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateFacilityStatusAsync(FacilityStatusCreateDto facilityStatus)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FacilityStatusExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<FacilityStatusDetailDto> GetFacilityStatusAsync(Guid id)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        #region IDisposable Support

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
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
