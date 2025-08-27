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
    public class ContactRepository : IContactRepository
    {
        private readonly FmsDbContext _context;
        public ContactRepository(FmsDbContext context) => _context = context;


        public Task<bool> ContactExistsAsync(Guid id) =>
            _context.Contacts.AnyAsync(e => e.Id == id);

        public async Task<ContactEditDto> GetContactByIdAsync(Guid id)
            {
            Prevent.NullOrEmpty(id, nameof(id));
            var contact = await _context.Contacts
                .AsNoTracking()
                .Include(e => e.ContactType)
                .Include(e => e.ContactTitle)
                .Include(e => e.Phones)
                .SingleOrDefaultAsync(e => e.Id == id);

            return contact == null ? null : new ContactEditDto(contact);
        }

        public async Task<IEnumerable<Contact>> GetContactsByFacilityIdAsync(Guid facilityId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));

            return await _context.Contacts
                .AsNoTracking()
                .Include(e => e.ContactType)
                .Include(e => e.ContactTitle)
                .Include(e => e.Phones)
                .Where(e => e.FacilityId == facilityId)
                .OrderByDescending(e => e.Active)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByFacilityIdAndTypeAsync(Guid facilityId, Guid contactTypeId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));
            Prevent.NullOrEmpty(contactTypeId, nameof(contactTypeId));

            return await _context.Contacts
                .AsNoTracking()
                .Include(e => e.ContactType)
                .Include(e => e.ContactTitle)
                .Include(e => e.Phones)
                .Where(e => e.FacilityId == facilityId && e.ContactTypeId == contactTypeId)
                .OrderByDescending(e => e.Active)
                .ToListAsync();
        }

        public Task<Guid> CreateContactAsync(ContactCreateDto contactCreate)
        {
            Prevent.Null(contactCreate, nameof(contactCreate));

            return CreateContactInternalAsync(contactCreate);
        }

        public async Task<Guid> CreateContactInternalAsync(ContactCreateDto contactCreate)
        {
            var newContact = new Contact
            {
                Id = Guid.NewGuid(),
                FacilityId = contactCreate.FacilityId,
                FamilyName = contactCreate.FamilyName,
                GivenName = contactCreate.GivenName,
                ContactTitleId = contactCreate.ContactTitleId,
                ContactTypeId = contactCreate.ContactTypeId,
                Company = contactCreate.Company,
                Address = contactCreate.Address,
                City = contactCreate.City,
                State = contactCreate.State,
                PostalCode = contactCreate.PostalCode,
                Email = contactCreate.Email,
                Active = contactCreate.Active,
            };

            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
            return newContact.Id;
        }

        public async Task UpdateContactAsync(ContactEditDto contact)
        {
            Prevent.Null(contact, nameof(contact));
            Prevent.NullOrEmpty(contact.Id, nameof(contact.Id));

            var existingContact = await _context.Contacts
                .SingleOrDefaultAsync(e => e.Id == contact.Id);

            if (existingContact == null)
            {
                throw new InvalidOperationException($"Contact with ID {contact.Id} does not exist.");
            }
            
            existingContact.FamilyName = contact.FamilyName;
            existingContact.GivenName = contact.GivenName;
            existingContact.ContactTitleId = contact.ContactTitleId;
            existingContact.ContactTypeId = contact.ContactTypeId;
            existingContact.Company = contact.Company;
            existingContact.Address = contact.Address;
            existingContact.City = contact.City;
            existingContact.State = contact.State;
            existingContact.PostalCode = contact.PostalCode;
            existingContact.Email = contact.Email;
            existingContact.Active = contact.Active;

            _context.Contacts.Update(existingContact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactActiveAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));

            var contact = await _context.Contacts
                .SingleOrDefaultAsync(e => e.Id == id);

            if (contact == null)
            {
                throw new InvalidOperationException($"Contact with ID {id} does not exist.");
            }

            contact.Active = active; 

            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Implementation

        private bool _disposed = false;

        ~ContactRepository()
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
