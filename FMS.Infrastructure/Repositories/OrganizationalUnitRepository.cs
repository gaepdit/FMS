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
    public class OrganizationalUnitRepository : IOrganizationalUnitRepository
    {
        private readonly FmsDbContext _context;

        public OrganizationalUnitRepository(FmsDbContext context) => _context = context;

        public Task<int> CountAsync(OrganizationalUnitSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateOrganizationalUnitAsync(OrganizationalUnitCreateDto organizationalUnit)
        {
            throw new NotImplementedException();
        }

        public Task<OrganizationalUnitDetailDto> GetOrganizationalUnitAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<OrganizationalUnitSummaryDto>> GetOrganizationalUnitListAsync()
        {
            return await _context.OrganizationalUnits.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new OrganizationalUnitSummaryDto(e))
                .ToListAsync();
        }

        public Task<bool> OrganizationalUnitExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrganizationalUnitAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates)
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
        ~OrganizationalUnitRepository()
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
