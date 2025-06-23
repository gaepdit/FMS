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
    public class PhoneRepository : IPhoneRepository
    {
        private readonly FmsDbContext _context;
        public PhoneRepository(FmsDbContext context) => _context = context;

        public Task<bool> PhoneExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PhoneEditDto> GetPhoneByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<PhoneSummaryDto>> GetReadOnlyPhoneByContactIdAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<PhoneEditDto>> GetPhoneByContactIdAsync(Guid contactId)
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreatePhoneAsync(PhoneCreateDto phone)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePhoneAsync(Guid id, PhoneEditDto phoneUpdates)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePhoneStatusAsync(Guid id, bool active)
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
        ~PhoneRepository()
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
