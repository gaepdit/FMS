using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class GroundwaterStatusRepository : IGroundwaterStatusRepository
    {
        public Task<Guid> CreateGroundwaterStatusAsync(GroundwaterStatusCreateDto groundwaterStatus)
        {
            throw new NotImplementedException();
        }

        public Task<GroundwaterStatusEditDto> GetGroundwaterStatusAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<GroundwaterStatusSummaryDto>> GetGroundwaterStatusListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetGroundwaterStatusNameAsync(Guid? id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GroundwaterStatusDescriptionExistsAsync(string description, Guid? ignoreId = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GroundwaterStatusExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> GroundwaterStatusNameExistsAsync(string name, Guid? ignoreId = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGroundwaterStatusAsync(Guid id, GroundwaterStatusEditDto groundwaterStatusUpdates)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGroundwaterStatusStatusAsync(Guid id, bool active)
        {
            throw new NotImplementedException();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~GroundwaterStatusRepository()
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
