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
    internal class ContactRepository : IContactRepository
    {
        private readonly FmsDbContext _context;

        public ContactRepository(FmsDbContext context) => _context = context;

        public Task<bool> ContactExistAsync(Guid id) =>
            _context.Contacts.AnyAsync(e => e.Id == id);

        public async Task<ContactEditDto> GetContactByIdAsync(Guid id)
        {
            var contact = await _context.Contacts.AsNoTracking()
                .SingleOrDefault(e => e.Id == id);

            return contact == null ? null : new ContactEditDto(contact);
        }
        public async Task<Chemical> GetContactsByFacilityIdAsync(Guid facilityId)
        {

        }
        
        public async Task<Contact> GetContactsByFacilityIdAndStatusAsync(Guid facilityId, string status)
        {

        }
        public async Task<Contact> GetContactsByFacilityIdAndTypeAsync(Guid facilityId, Guid contactTypeId)
        {

        }
        public Task AddContactAsync(ContactCreateDto contact)
        {

        }
    }

}
