using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class FundingSourceRepository : IFundingSourceRepository
    {
        public readonly FmsDbContext _context;
        public FundingSourceRepository(FmsDbContext context) => _context = context;


        // Implement interface methods
        public Task<bool> FundingSourceExistsAsync(Guid id) =>
            _context.FundingSources.AnyAsync(e => e.Id == id);

        public Task<bool> FundingSourceNameExistsAsync(string name, Guid? ignoreId = null) =>
            _context.FundingSources.AnyAsync(e => 
            e.Name == name && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task<FundingSourceEditDto> GetFundingSourceByIdAsync(Guid id)
        {
            FundingSource fundingSource = await _context.FundingSources.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
            if (fundingSource == null)
            {
                return null;
            }
            return new FundingSourceEditDto(fundingSource);
        }

        public async Task<IReadOnlyList<FundingSourceSummaryDto>> GetFundingSourceListAsync() => await _context.FundingSources.AsNoTracking()
           .OrderBy(e => e.Name)
           .Select(e => new FundingSourceSummaryDto(e))
           .ToListAsync();

        public async Task<Guid> CreateFundingSourceAsync(FundingSourceCreateDto fundingSource)
        {
            Prevent.Null(fundingSource, nameof(fundingSource));
            Prevent.NullOrEmpty(fundingSource.Name, nameof(fundingSource.Name));

            return await CreateFundingSourceInternalAsync(fundingSource);
        }

        private async Task<Guid> CreateFundingSourceInternalAsync(FundingSourceCreateDto fundingSource)
        {
            var newFundingSource = new FundingSource(fundingSource);
            await _context.FundingSources.AddAsync(newFundingSource);
            await _context.SaveChangesAsync();

            return newFundingSource.Id;
        }

        public async Task UpdateFundingSourceAsync(Guid id, FundingSourceEditDto fundingSourceUpdates)
        {
            Prevent.NullOrEmpty(fundingSourceUpdates.Name, nameof(fundingSourceUpdates.Name));

            await UpdateFundingSourceInternalAsync(id, fundingSourceUpdates);
        }

        private async Task<Guid> UpdateFundingSourceInternalAsync(Guid id, FundingSourceEditDto fundingSourceUpdates)
        {
            var fundingSource = await _context.FundingSources.FindAsync(id) ?? throw new ArgumentException("Funding Source ID not found.", nameof(id));

            fundingSource.Name = fundingSourceUpdates.Name;

            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return fundingSource.Id;
        }

        public async Task UpdateFundingSourceStatusAsync(Guid id, bool active)
        {
            var fundingSource = await _context.FundingSources.FindAsync(id)
                ?? throw new ArgumentException("Funding Source ID not found");

            fundingSource.Active = active;

            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~FundingSourceRepository()
        {
            Dispose(false);
        }

        // Implement IDisposable pattern
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources here if necessary
                }

                // Dispose unmanaged resources here if necessary
                _disposed = true;
            }
        }

        #endregion
    }
}
