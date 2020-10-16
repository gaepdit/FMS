using FMS.Domain.Dto;
using FMS.Domain.Entities;
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

        private async Task<bool> BudgetCodeExistsAsync(string code) =>
            await _context.BudgetCodes.AnyAsync(e => e.Code == code);

        public async Task<int> CountAsync()
        {
            return await _context.BudgetCodes.AsNoTracking().CountAsync();
        }

        public async Task<BudgetCodeEditDto> GetBudgetCodeAsync(Guid id)
        {
            var budgetCode = await _context.BudgetCodes.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            if (budgetCode == null)
            {
                return null;
            }

            return new BudgetCodeEditDto(budgetCode);
        }

        public async Task<IReadOnlyList<BudgetCodeSummaryDto>> GetBudgetCodeListAsync()
        {
            return await _context.BudgetCodes.AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new BudgetCodeSummaryDto(e))
                .ToListAsync();
        }

        public async Task<Guid> CreateBudgetCodeAsync(BudgetCodeCreateDto budgetCode)
        {
            if (string.IsNullOrWhiteSpace(budgetCode.Code))
            {
                throw new ArgumentException("New Code for Budget Code is required.");
            }

            if (string.IsNullOrWhiteSpace(budgetCode.Name))
            {
                throw new ArgumentException("New Name for Budget Code is required.");
            }

            return await CreateBudgetCodeInternalAsync(budgetCode);
        }

        public async Task<Guid> CreateBudgetCodeInternalAsync(BudgetCodeCreateDto budgetCode)
        {
            if (await BudgetCodeExistsAsync(budgetCode.Code))
            {
                throw new ArgumentException($"Budget Code {budgetCode.Code} Already Exists.");
            }

            var newBC = new BudgetCode(budgetCode);

            await _context.BudgetCodes.AddAsync(newBC);
            await _context.SaveChangesAsync();

            return newBC.Id;
        }

        public Task UpdateBudgetCodeAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates)
        {
            if (string.IsNullOrWhiteSpace(budgetCodeUpdates.Code))
            {
                throw new ArgumentException("Budget Code Code is required.");
            }
            if (string.IsNullOrWhiteSpace(budgetCodeUpdates.Name))
            {
                throw new ArgumentException("Budget Code Name is required.");
            }
            return UpdateBudgetCodeInternalAsync(id, budgetCodeUpdates);
        }

        public async Task UpdateBudgetCodeInternalAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates)
        {
            var budgetCode = await _context.BudgetCodes.FindAsync(id);

            if (budgetCode == null)
            {
                throw new ArgumentException("Budget Code ID not found.", nameof(id));
            }

            if (await BudgetCodeCodeExistsAsync(budgetCode.Code, id))
            {
                throw new ArgumentException($"Budget Code Code '{budgetCode.Code}' already exists.");
            }

            budgetCode.Code = budgetCodeUpdates.Code;
            budgetCode.Name = budgetCodeUpdates.Name;
            budgetCode.OrganizationNumber = budgetCodeUpdates.OrganizationNumber;
            budgetCode.ProjectNumber = budgetCodeUpdates.ProjectNumber;
            budgetCode.Active = true;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> BudgetCodeCodeExistsAsync(string budgetCodeCode, Guid? ignoreId = null) => await _context.BudgetCodes.AnyAsync(e => e.Code == budgetCodeCode && (!ignoreId.HasValue || e.Id != ignoreId.Value));

        public async Task UpdateBudgetCodeStatusAsync(Guid id, bool active)
        {
            var budgetCode = await _context.BudgetCodes.FindAsync(id);

            if (budgetCode == null)
            {
                throw new ArgumentException("Budget Code ID not found");
            }

            budgetCode.Active = active;

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
