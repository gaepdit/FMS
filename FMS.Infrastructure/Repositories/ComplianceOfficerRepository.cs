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
    public class ComplianceOfficerRepository : IComplianceOfficerRepository
    {
        private readonly FmsDbContext _context;

        public ComplianceOfficerRepository(FmsDbContext context) => _context = context;
        

        public Task<bool> ComplianceOfficerExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> CountAsync(ComplianceOfficerSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer)
        {
            throw new NotImplementedException();
        }

        public Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync()
        {
            return await _context.ComplianceOfficers.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new ComplianceOfficerSummaryDto(e))
                .ToListAsync();
        }

        public Task UpdateComplianceOfficerAsync(Guid id, ComplianceOfficerEditDto complianceOfficerUpdates)
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
        ~ComplianceOfficerRepository()
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
