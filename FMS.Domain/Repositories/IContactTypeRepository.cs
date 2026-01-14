using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IContactTypeRepository : IDisposable
    {
        Task<bool> ContactTypeExistsAsync(Guid id);

        Task<bool> ContactTypeNameExistsAsync(string name, Guid? ignoreId = null);

        Task<ContactTypeEditDto> GetContactTypeByIdAsync(Guid id);

        Task<IReadOnlyList<ContactTypeSummaryDto>> GetContactTypeListAsync();

        Task<Guid> CreateContactTypeAsync(ContactTypeCreateDto contactType);

        Task UpdateContactTypeAsync(Guid id, ContactTypeEditDto contactType);

        Task UpdateContactTypeStatusAsync(Guid id, bool active);
    }
}
