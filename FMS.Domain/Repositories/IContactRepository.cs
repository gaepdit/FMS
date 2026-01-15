using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IContactRepository : IDisposable
    {
        Task<bool> ContactExistsAsync(Guid id);
       
        Task<ContactEditDto> GetContactByIdAsync(Guid id);

        Task<IEnumerable<Contact>> GetContactsByFacilityIdAsync(Guid facilityId);

        Task<IEnumerable<Contact>> GetContactsByFacilityIdAndTypeAsync(Guid facilityId, Guid contactTypeId);

        Task<Guid> CreateContactAsync(ContactCreateDto contactCreate);

        Task UpdateContactAsync(Guid id, ContactEditDto contact);

        Task UpdateContactActiveAsync(Guid id, bool active);
    }
}
