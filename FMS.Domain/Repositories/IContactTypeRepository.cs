using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IContactTypeRepository : IDisposable
    {
        Task<bool> ContactTypeExistsAsync(Guid id);

        Task<ContactTypeEditDto> GetContactTypeByIdAsync(Guid id);

        Task<IReadOnlyList<ContactTypeSummaryDto>> GetContactTypeListsAsync();

        Task<Guid> CreateContactTypeAsync(ContactTypeCreateDto contactType);

        Task UpdateContactTypeAsync(Guid Id, ContactTypeEditDto contactTypeUpdates);

        Task UpdateContactTypeStatusAsync(Guid id, bool active);
    }
}
