using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IPhoneRepository : IDisposable
    {
        Task<bool> PhoneExistsAsync(Guid id);

        Task<bool> PhoneNumberExistsAsync(string phoneNumber);

        Task<PhoneEditDto> GetPhoneByIdAsync(Guid id);

        Task<PhoneEditDto> GetPhoneByIdAndContactIdAsync(Guid id, Guid contactId);

        Task<IReadOnlyList<PhoneSummaryDto>> GetPhoneListByContactIdAsync(Guid contactId);

        Task<Guid> CreatePhoneAsync(PhoneCreateDto phone);

        Task UpdatePhoneAsync(PhoneEditDto phoneUpdates);

        Task UpdatePhoneStatusAsync(Guid id, bool active);
    }
}
