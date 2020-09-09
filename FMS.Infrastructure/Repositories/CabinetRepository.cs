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
    public class CabinetRepository : ICabinetRepository
    {
        private readonly FmsDbContext _context;

        public CabinetRepository(FmsDbContext context) => _context = context;

        public async Task<bool> CabinetExistsAsync(Guid id)
        {
            return await _context.Cabinets.AnyAsync(e => e.Id == id);
        }

        public async Task<CabinetDetailDto> GetCabinetAsync(Guid id)
        {
            var cabinet = await _context.Cabinets.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (cabinet == null)
            {
                return null;
            }

            return new CabinetDetailDto(cabinet);
        }

        public async Task<IReadOnlyList<CabinetDetailDto>> GetCabinetListAsync()
        {
            return await _context.Cabinets.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new CabinetDetailDto(e))
                .ToListAsync();
        }

        public Task<Guid> CreateCabinetAsync(CabinetCreateDto cabinet)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinet)
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
        ~CabinetRepository()
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
