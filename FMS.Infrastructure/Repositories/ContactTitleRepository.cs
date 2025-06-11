using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Domain.Utils;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class ContactTitleRepository : IContactTitleRepository
    {
        private readonly FmsDbContext _context;
        public ContactTitleRepository(FmsDbContext context) => _context = context;

        public Task<bool> ContactTitleExistAsync(Guid id) =>
            _context.ContactTitles.AnyAsync(e => e.Id == id);

        public async Task<ContactTitleEditDto> GetContactTitleByIdAsync(Guid id)
        {
            var contactTitle = await _context.ContactTitles.AsNoTracking().
                SingleOrDefaultAsync(e => e.Id == id);
            if (contactTitle == null)
            {
                return null;
            }
            return new ContactTitleEditDto(contactTitle);
        }
        public async Task<IReadOnlyList<ContactTitleSummaryDto>> GetContactTitleListAsync() => await _context.ContactTitles.AsNoTracking()
            .OrderBy(e => e.Id)
            .Select(e => new ContactTitleSummaryDto(e))
            .ToListAsync();
        public Task<Guid> CreateContactTilteAsync(ContactTitleCreateDto contactType)
        {
            Prevent.Null(contactType, nameof(contactType));
            Prevent.NullOrEmpty(contactType.Name, nameof(contactType.Name));

            return CreateContactTitleInternalAsync(contactType);
        }
        private async Task<Guid> CreateContactTitleInternalAsync(ContactTitleCreateDto contactTitle)
        {
            var newCT = new ContactTitle(contactTitle);
            await _context.ContactTitles.AddAsync(newCT);
            await _context.SaveChangesAsync();

            return newCT.Id;
        }
    }
}
