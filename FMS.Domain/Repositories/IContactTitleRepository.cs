using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IContactTitleRepository : IDisposable
    {
        Task<bool> ContactTitleExistsAsync(Guid id);

        Task<bool> ContactTitleNameExistsAsync(string name, Guid? ignoreId = null);

        Task<ContactTitleEditDto> GetContactTitleByIdAsync(Guid id);

        Task<IReadOnlyList<ContactTitleSummaryDto>> GetContactTitleListAsync();

        Task<Guid> CreateContactTitleAsync(ContactTitleCreateDto contactTitle);

        Task UpdateContactTitleAsync(Guid id, ContactTitleEditDto contactTitleUpdates);

        Task UpdateContactTitleStatusAsync(Guid id, bool active);
    }
}
