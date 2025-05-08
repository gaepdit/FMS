using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IPhoneRepository : IDisposable
    {
        Task<bool> PhoneExistsAsync(Guid id);

        Task<FundingSourceEditDto> GetPhoneByIdAsync(Guid id);

        Task<IReadOnlyList<FundingSourceSummaryDto>> GetReadOnlyPhoneByContactIdAsync(Guid contactId);

        Task<IList<FundingSourceEditDto>> GetPhoneByContactIdAsync(Guid contactId);

        Task<Guid> CreatePhoneAsync(PhoneCreateDto phone);

        Task UpdatePhoneAsync(Guid Id, PhoneEditDto phoneUpdates);

        Task UpdatePhoneStatusAsync(Guid id, bool active);
    }
}
