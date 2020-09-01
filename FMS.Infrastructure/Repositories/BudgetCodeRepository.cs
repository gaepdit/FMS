﻿using FMS.Domain.Dto;
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

        public Task<bool> BudgetCodeExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public Task<int> CountAsync(BudgetCodeSpec spec)
        {
            throw new NotImplementedException();
        }

        public Task<BudgetCodeDetailDto> GetBudgetCodeAsync(Guid id)
        {
            throw new NotImplementedException();
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

        public Task<int> DeleteBudgetCodeAsync(BudgetCodeDetailDto deletedBudgetCode)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBudgetCodeAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates)
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
