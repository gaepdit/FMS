using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Infrastructure.Repositories
{
    internal class ContactTypeRepository
    {
        public Task<bool> ContactTypeExistsAsync(Guid id) =>
            ;
        public async Task<ContactTypeEditDto> GetContactTypeByIdAsync(Guid id);
        public async Task<IReadOnlyList<ContactTypeSummaryDto>> GetContactTypeListAsync();
        public Task<Guid> CreateContactTypeAsync(ContactTypeCreateDto contactType);
        public Task UpdateContactTypeAsync(Guid Id, ContactTypeEditDto contactTypeUpdate);
        public async Task UpdateContactTypeStatusAsync(Guid id, bool active);
    }
}
