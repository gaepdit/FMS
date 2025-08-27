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
    public class PhoneRepository : IPhoneRepository
    {
        private readonly FmsDbContext _context;
        public PhoneRepository(FmsDbContext context) => _context = context;

        public Task<bool> PhoneExistsAsync(Guid id) =>
            _context.Phones.AnyAsync(e => e.Id == id);

        public Task<bool> PhoneNumberExistsAsync(string phoneNumber) => 
            _context.Phones.AnyAsync(e => e.Number == phoneNumber);

        public async Task<PhoneEditDto> GetPhoneByIdAsync(Guid id)
        {
            var phone = await _context.Phones.AsNoTracking()
                .SingleOrDefaultAsync(e => e.Id == id);

            return phone == null ? null : new PhoneEditDto(phone);
        }

        public async Task<PhoneEditDto> GetPhoneByIdAndContactIdAsync(Guid id, Guid contactId)
        {
            var phone = await _context.Phones.AsNoTracking()
                .SingleOrDefaultAsync(e => e.ContactId == contactId && e.Id == id);

            return phone == null ? null : new PhoneEditDto(phone);
        }

        public async Task<IReadOnlyList<PhoneSummaryDto>> GetPhoneListByContactIdAsync(Guid contactId)
        {
            Prevent.NullOrEmpty(contactId, nameof(contactId));

            return await _context.Phones.AsNoTracking()
                .OrderByDescending(e => e.Active)
                .Where(e => e.ContactId == contactId)
                .Select(e => new PhoneSummaryDto(e))
                .ToListAsync();
        }

        public Task<Guid> CreatePhoneAsync(PhoneCreateDto phone)
        {
            Prevent.Null(phone, nameof(phone));
            Prevent.NullOrWhiteSpace(phone.Number, nameof(phone.Number));

            return CreatePhoneInternalAsync(phone);
        }

        public async Task<Guid> CreatePhoneInternalAsync(PhoneCreateDto phone)
        {
            if (await PhoneExistsAsync(phone.Id))
            {
                throw new ArgumentException($"Phone: {phone.Id} Already Exists.");
            }
            if (await PhoneNumberExistsAsync(phone.Number))
            {
                throw new ArgumentException($"Phone Number: {phone.Number} Already Exists.");
            }

            var newPhone = new Phone()
            {
                Id = Guid.NewGuid(),
                ContactId = phone.ContactId,
                Number = phone.Number,
                PhoneType = phone.PhoneType,
                Active = phone.Active
            };

            await _context.Phones.AddAsync(newPhone);
            await _context.SaveChangesAsync();
            return newPhone.Id;
        }

        public Task UpdatePhoneAsync(PhoneEditDto phoneUpdates)
        {
            Prevent.Null(phoneUpdates, nameof(phoneUpdates));
            Prevent.NullOrWhiteSpace(phoneUpdates.Number, nameof(phoneUpdates.Number));

            return UpdatePhoneInternalAsync(phoneUpdates);
        }

        public async Task UpdatePhoneInternalAsync(PhoneEditDto phoneUpdates)
        {
            if (!await PhoneExistsAsync(phoneUpdates.Id))
            {
                throw new ArgumentException($"Phone Id {phoneUpdates.Id} does not exist.");
            }

            var existingPhone = await _context.Phones.FindAsync(phoneUpdates.Id);

            existingPhone.Number = phoneUpdates.Number;
            existingPhone.PhoneType = phoneUpdates.PhoneType;
            existingPhone.ContactId = phoneUpdates.ContactId;
            existingPhone.Active = phoneUpdates.Active;

            _context.Phones.Update(existingPhone);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePhoneStatusAsync(Guid id, bool active)
        {
            Prevent.NullOrEmpty(id, nameof(id));

            var phone = await _context.Phones
                .SingleOrDefaultAsync(e => e.Id == id);

            if (phone == null)
            {
                throw new InvalidOperationException($"Phone with ID {id} does not exist.");
            }

            phone.Active = active;

            _context.Phones.Update(phone);
            await _context.SaveChangesAsync();
        }

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~PhoneRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
