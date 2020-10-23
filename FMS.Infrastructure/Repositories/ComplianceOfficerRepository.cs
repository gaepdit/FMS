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

        public async Task<bool> ComplianceOfficerIdExistsAsync(Guid id) =>
            await _context.ComplianceOfficers.AnyAsync(e => e.Id == id);

        public async Task<bool> ComplianceOfficerNameExistsAsync(string name) =>
            await _context.ComplianceOfficers.AnyAsync(m => name.Contains(m.GivenName)
                    && name.Contains(m.FamilyName));

        public async Task<int> CountAsync(ComplianceOfficerSpec spec)
        {
            return await _context.ComplianceOfficers.AsNoTracking().CountAsync();
        }

        public async Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id)
        {
            var complianceOfficer = await _context.ComplianceOfficers.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (complianceOfficer == null || complianceOfficer.Id == Guid.Empty)
            {
                return null;
            }

            return new ComplianceOfficerDetailDto(complianceOfficer);
        }

        public async Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(string name)
        {
            var complianceOfficer = await _context.ComplianceOfficers.AsNoTracking()
                .SingleOrDefaultAsync(e => name.Contains(e.FamilyName) && name.Contains(e.GivenName));

            if (complianceOfficer == null || complianceOfficer.Id == Guid.Empty)
            {
                return null;
            }

            return new ComplianceOfficerDetailDto(complianceOfficer);
        }

        public async Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync()
        {
            return await _context.ComplianceOfficers.AsNoTracking()
                .OrderBy(e => e.FamilyName)
                .Select(e => new ComplianceOfficerSummaryDto(e))
                .ToListAsync();
        }
        public Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer)
        {
            throw new NotImplementedException();
        }

        public Task UpdateComplianceOfficerAsync(Guid id, ComplianceOfficerEditDto complianceOfficerUpdates)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateComplianceOfficerStatusAsync(Guid id, bool active)
        {
            var complianceOfficer = await _context.ComplianceOfficers.FindAsync(id);

            if (complianceOfficer == null)
            {
                throw new ArgumentException("Compliance Officer ID not found");
            }

            complianceOfficer.Active = active;

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
