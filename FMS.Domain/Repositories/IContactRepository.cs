using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Entities;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IContactRepository : IDisposable
    {
        Task<bool> ContactExistsAsync(Guid id);
       
        Task<Contact> GetContactByIdAsync(Guid id);

        Task<IEnumerable<Contact>> GetContactsByFacilityIdAsync(Guid facilityId);

        Task<IEnumerable<Contact>> GetContactsByFacilityIdAndTypeAsync(Guid facilityId, Guid contactTypeId);

        Task<Guid> CreateContactAsync(ContactCreateDto contactCreate);

        Task UpdateContactAsync(ContactEditDto contact);

        Task UpdateContactActiveAsync(Guid id, bool active);
    }
}
