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
    public class ContactTitleRepository : IContactTitleRepository
    {
        private readonly FmsDbContext _context;
        public ContactTitleRepository(FmsDbContext context) => _context = context;

        public Task<bool> ContactTitleExistsAsync(Guid id) =>
            _context.ContactTitles.AnyAsync(e => e.Id == id);

        public async Task<ContactTitleEditDto> GetContactTitleByIdAsync(Guid id)
        {
            var contactTitle = await _context.ContactTitles.AsNoTracking().
                SingleOrDefaultAsync(e => e.Id == id);
            if (contactTitle == null)
            {
                return null;
            }
            return new ContactTitleEditDto(contactTitle);
        }
        public async Task<IReadOnlyList<ContactTitleSummaryDto>> GetContactTitleListAsync() => await _context.ContactTitles.AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new ContactTitleSummaryDto(e))
            .ToListAsync();
        public Task<Guid> CreateContactTitleAsync(ContactTitleCreateDto contactType)
        {
            Prevent.Null(contactType, nameof(contactType));
            Prevent.NullOrEmpty(contactType.Name, nameof(contactType.Name));

            return CreateContactTitleInternalAsync(contactType);
        }
        private async Task<Guid> CreateContactTitleInternalAsync(ContactTitleCreateDto contactTitle)
        {
            var newCT = new ContactTitle(contactTitle);
            await _context.ContactTitles.AddAsync(newCT);
            await _context.SaveChangesAsync();

            return newCT.Id;
        }
        public Task UpdateContactTitleAsync(Guid id, ContactTitleEditDto contactTitleUpdates)
        {
            Prevent.Null(contactTitleUpdates, nameof(contactTitleUpdates));
            Prevent.NullOrEmpty(contactTitleUpdates.Name, nameof(contactTitleUpdates.Name));

            return UpdateContactTitleInternalAsync(id, contactTitleUpdates);
        }

        private async Task UpdateContactTitleInternalAsync(Guid id, ContactTitleEditDto contactTitleUpdates)
        {
            var contactTitle = await _context.ContactTitles.FindAsync(id);

            if (contactTitle == null)
            {
                throw new ArgumentException("Contact Title ID not found", nameof(id));
            }
            contactTitle.Name = contactTitleUpdates.Name;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactTitleStatusAsync(Guid id, bool active)
        {
            var contactTitleStatus = await _context.ContactTitles.FindAsync(id);

            if (contactTitleStatus == null)
            {
                throw new ArgumentException("Contact Title ID not found");
            }
            contactTitleStatus.Active = active;

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
        ~ContactTitleRepository()
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
