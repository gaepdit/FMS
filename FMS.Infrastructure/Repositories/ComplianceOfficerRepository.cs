using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FMS.Infrastructure.Repositories
{
    public class ComplianceOfficerRepository : IComplianceOfficerRepository
    {
        private readonly FmsDbContext _context;

        public ComplianceOfficerRepository(FmsDbContext context) => _context = context;

        public async Task<bool> ComplianceOfficerIdExistsAsync(Guid id) =>
            await _context.ComplianceOfficers.AnyAsync(e => e.Id == id);

        public async Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id)
        {
            var complianceOfficer = await _context.ComplianceOfficers.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return complianceOfficer == null ? null : new ComplianceOfficerDetailDto(complianceOfficer);
        }

        public async Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(string email)
        {
            var complianceOfficer = await _context.ComplianceOfficers.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Email == email);

            return complianceOfficer == null ? null : new ComplianceOfficerDetailDto(complianceOfficer);
        }

        public async Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync()
        {
            return await _context.ComplianceOfficers.AsNoTracking()
                .OrderBy(e => e.FamilyName)
                .ThenBy(e => e.GivenName)
                .Select(e => new ComplianceOfficerSummaryDto(e))
                .ToListAsync();
        }

        public async Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer)
        {
            Prevent.Null(complianceOfficer, nameof(complianceOfficer));

            var newCO = new ComplianceOfficer(complianceOfficer);

            await _context.ComplianceOfficers.AddAsync(newCO);
            await _context.SaveChangesAsync();

            return newCO.Id;
        }

        public async Task<Guid?> TryCreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer)
        {
            if (await _context.ComplianceOfficers.AnyAsync(e => e.Email == complianceOfficer.Email))
            {
                return null;
            }

            return await CreateComplianceOfficerAsync(complianceOfficer);
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