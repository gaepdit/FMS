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
    public class FileCabinetRepository : IFileCabinetRepository
    {
        private readonly FmsDbContext _context;

        public FileCabinetRepository(FmsDbContext context) => _context = context;

        public Task<int> CountAsync(FileCabinetSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateFileCabinetAsync(FileCabinetCreateDto fileCabinet)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FileCabinetExistsAsync(Guid id)
        {
            return await _context.FileCabinets.AnyAsync(e => e.Id == id);
        }

        public async Task<FileCabinetDetailDto> GetFileCabinetAsync(Guid id)
        {
            var fileCabinet = await _context.FileCabinets.AsNoTracking()
                .Include(e => e.StartCounty)
                .Include(e => e.EndCounty)
                .SingleOrDefaultAsync(e => e.Id == id);

            if (fileCabinet == null)
            {
                return null;
            }

            return new FileCabinetDetailDto(fileCabinet);
        }

        public async Task<IReadOnlyList<FileCabinetSummaryDto>> GetFileCabinetListAsync()
        {
            return await _context.FileCabinets.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new FileCabinetSummaryDto(e))
                .ToListAsync();
        }

        public Task UpdateFileCabinetAsync(Guid id, FileCabinetEditDto fileCabinetUpdates)
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
        ~FileCabinetRepository()
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
