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

        public async Task<Contact> GetContactByIdAsync(Guid id)
            {
            Prevent.NullOrEmpty(id, nameof(id));
            return await _context.Contacts
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Contact>> GetContactsByFacilityIdAsync(Guid facilityId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));
            return await _context.Contacts
                .AsNoTracking()
                .Where(e => e.FacilityId == facilityId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByFacilityIdAndStatusAsync(Guid facilityId, string status)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));
            Prevent.NullOrEmpty(status, nameof(status));
            return await _context.Contacts
                .AsNoTracking()
                .Where(e => e.FacilityId == facilityId && e.Status == status)
                .ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetContactsByFacilityIdAndTypeAsync(Guid facilityId, Guid contactTypeId)
        {
            Prevent.NullOrEmpty(facilityId, nameof(facilityId));
            Prevent.NullOrEmpty(contactTypeId, nameof(contactTypeId));
            return await _context.Contacts
                .AsNoTracking()
                .Where(e => e.FacilityId == facilityId && e.ContactTypeId == contactTypeId)
                .ToListAsync();
        }

        public async Task AddContactAsync(ContactCreateDto contact)
        {
            Prevent.Null(contact, nameof(contact));
            Prevent.NullOrEmpty(contact.FacilityId, nameof(contact.FacilityId));

            var newContact = new Contact(Guid.NewGuid(), contact);

            if (await _context.Contacts.AnyAsync(e => e.Email == contact.Email && e.FacilityId == contact.FacilityId))
            {
                throw new ArgumentException($"Contact with email {contact.Email} already exists for this facility.");
            }
            if (await _context.Contacts.AnyAsync(e => e.GivenName == contact.GivenName && e.FacilityId == contact.FacilityId && e.FamilyName == contact.FamilyName))
            {
                throw new ArgumentException($"Contact with name {contact.GivenName} {contact.FamilyName} already exists for this facility.");
            }

            _context.Contacts.Add(newContact);
            await _context.SaveChangesAsync();
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
            existingContact.Status = contact.Status;


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
