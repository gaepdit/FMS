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
    public class EnvironmentalInterestRepository : IEnvironmentalInterestRepository
    {
        private readonly FmsDbContext _context;

        public EnvironmentalInterestRepository(FmsDbContext context) => _context = context;

        public Task<int> CountAsync(EnvironmentalInterestSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateEnvironmentalInterestAsync(EnvironmentalInterestCreateDto environmentalInterest)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EnvironmentalInterestExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<EnvironmentalInterestDetailDto> GetEnvironmentalInterestAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<EnvironmentalInterestSummaryDto>> GetEnvironmentalInterestListAsync()
        {
            return await _context.EnvironmentalInterests.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new EnvironmentalInterestSummaryDto(e))
                .ToListAsync();
        }

        public Task UpdateEnvironmentalInterestAsync(Guid id, EnvironmentalInterestEditDto environmentalInterestUpdates)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects)
                    _context.Dispose();
                }

                // free unmanaged resources (unmanaged objects) and override finalizer
                // set large fields to null
                _disposedValue = true;
            }
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~EnvironmentalInterestRepository()
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
