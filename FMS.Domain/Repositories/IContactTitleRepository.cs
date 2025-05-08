using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IContactTitleRepository : IDisposable
    {
        Task<bool> ContactTitleExistsAsync(Guid id);

        Task<ContactTitleEditDto> GetContactTitleByIdAsync(Guid id);

        Task<IReadOnlyList<ContactTitleSummaryDto>> GetContactTitleListsAsync();

        Task<Guid> CreateContactTitleAsync(ContactTitleCreateDto contactTitle);

        Task UpdateContactTitleAsync(Guid Id, ContactTitleEditDto contactTitleUpdates);

        Task UpdateContactTitleStatusAsync(Guid id, bool active);
    }
}
