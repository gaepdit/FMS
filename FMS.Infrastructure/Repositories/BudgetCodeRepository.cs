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
    public class BudgetCodeRepository : IBudgetCodeRepository
    {
        private readonly FmsDbContext _context;

        public BudgetCodeRepository(FmsDbContext context) => _context = context;

        public async Task<bool> BudgetCodeExistsAsync(Guid id) =>
            await _context.BudgetCodes.AnyAsync(e => e.Id == id);

        public async Task<int> CountAsync()
        {
            return await _context.BudgetCodes.AsNoTracking().CountAsync();
        }

        public async Task<BudgetCodeDetailDto> GetBudgetCodeAsync(Guid id)
        {
            var budgetCode = await _context.BudgetCodes.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (budgetCode == null)
            {
                return null;
            }

            return new BudgetCodeDetailDto(budgetCode);
        }

        public async Task<IReadOnlyList<BudgetCodeSummaryDto>> GetBudgetCodeListAsync()
        {
            return await _context.BudgetCodes.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new BudgetCodeSummaryDto(e))
                .ToListAsync();
        }

        public Task<Guid> CreateBudgetCodeAsync(BudgetCodeCreateDto budgetCode)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBudgetCodeAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates)
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
        ~BudgetCodeRepository()
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
