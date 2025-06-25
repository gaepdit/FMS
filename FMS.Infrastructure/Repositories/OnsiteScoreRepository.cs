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
    public class OnsiteScoreRepository : IOnsiteScoreRepository
    {
        private readonly FmsDbContext _context;
        public OnsiteScoreRepository(FmsDbContext context) => _context = context;


        public Task<bool> OnsiteScoreExistsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<OnsiteScoreEditDto> GetOnsiteScoreByScoreIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateOnsiteScoreAsync(OnSiteScoreCreateDto onSiteScore)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOnsiteScoreAsync(OnsiteScoreEditDto onSiteScore)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteOnsiteScoreAsync(Guid id)
        {
            throw new NotImplementedException();
        }


        #region IDisposable Support

        private bool _disposedValue; // Corrected: 'private' modifier now precedes the member type

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
        ~OnsiteScoreRepository()
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
