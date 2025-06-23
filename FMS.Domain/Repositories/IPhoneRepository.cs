using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IPhoneRepository : IDisposable
    {
        Task<bool> PhoneExistsAsync(Guid id);

        Task<PhoneEditDto> GetPhoneByIdAsync(Guid id);

        Task<IReadOnlyList<PhoneSummaryDto>> GetReadOnlyPhoneByContactIdAsync(Guid contactId);

        Task<IList<PhoneEditDto>> GetPhoneByContactIdAsync(Guid contactId);

        Task<Guid> CreatePhoneAsync(PhoneCreateDto phone);

        Task UpdatePhoneAsync(Guid id, PhoneEditDto phoneUpdates);

        Task UpdatePhoneStatusAsync(Guid id, bool active);
    }
}
