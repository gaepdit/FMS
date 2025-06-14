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
    
    public class OverallStatusRepository : IOverallStatusRepository
    {
        public Task<bool> OverallStatusExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OverallStatusEditDto> GetOverallStatusByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateOverallStatusAsync(OverallStatusCreateDto overallStatus)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OverallStatusSummaryDto>> GetOverallStatusListsAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateOverallStatusAsync(Guid Id, OverallStatusEditDto overallStatusUpdates)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOverallStatusStatusAsync(Guid id, bool active)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~OverallStatusRepository()
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
