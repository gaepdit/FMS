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
    internal class ContactTypeRepository : IContactTypeRepository
    {
        public readonly FmsDbContext _context;
        public ContactTypeRepository(FmsDbContext context) => _context = context;
        public Task<bool> ContactTypeExistsAsync(Guid id) =>
            _context.ContactType.AnyAsync(e => e.Id == id);
        public async Task<ContactTypeEditDto> GetContactTypeByIdAsync(Guid id)
        {
            var contactType = await _context.ContactType.AsNoTracking().
                SingleOrDefaultAsync(e => e.Id == id);
            if (contactType == null)
            {
                return null;
            }
            return new ContactTypeEditDto(contactType);
        }
        public async Task<IReadOnlyList<ContactTypeSummaryDto>> GetContactTypeListsAsync() => await _context.ContactType.AsNoTracking()
           .OrderBy(e => e.Name)
           .Select(e => new ContactTypeSummaryDto(e))
           .ToListAsync();
        public Task<Guid> CreateContactTypeAsync(ContactTypeCreateDto contactType)
        {
            Prevent.Null(contactType, nameof(contactType));
            Prevent.NullOrEmpty(contactType.Name, nameof(contactType.Name));

            return CreateContactTypeInternalAsync(contactType);
        }
        private async Task<Guid> CreateContactTypeInternalAsync(ContactTypeCreateDto contactType)
        {
            var newCT = new ContactType(contactType);
            await _context.ContactType.AddAsync(newCT);
            await _context.SaveChangesAsync();

            return newCT.Id;
        }
        public Task UpdateContactTypeAsync(Guid id, ContactTypeEditDto contactType)
        {
            Prevent.NullOrEmpty(contactType.Name, nameof(contactType.Name));
            return UpdateContactTYpeInternalAsync(id, contactType);
        }
        private async Task<Guid> UpdateContactTYpeInternalAsync(Guid id, ContactTypeEditDto contactTypeUpdates)
        {
            var contactType = await _context.ContactType.FindAsync(id) ?? throw new ArgumentException("Contact Type ID not found.", nameof(id));

            contactType.Name = contactTypeUpdates.Name;

            await _context.SaveChangesAsync();

            // Ensure all code paths return a value
            return contactType.Id;
        }
        public async Task UpdateContactTypeStatusAsync(Guid id, bool active)
        {
            var contactType = await _context.ContactType.FindAsync(id)
                ?? throw new ArgumentException("Contact Type ID not found");
            contactType.Active = active;
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
        ~ContactTypeRepository()
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
