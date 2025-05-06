using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IAllowedActionTakenRepository : IDisposable
    {
        Task<bool> AllowedActionTakenExistsAsync(Guid id);

        Task<AllowedActionTakenEditDto> GetAllowedActionTakenAsync(Guid id);

        Task<IReadOnlyList<AllowedActionTakenSummaryDto>> GetAllowedActionTakenListAsync();

        Task<Guid> CreateAllowedActionTakenAsync(AllowedActionTakenCreateDto allowedActionTaken);

        Task UpdateAllowedActionTakenAsync(Guid id, AllowedActionTakenEditDto allowedActionTakenUpdates);

        Task UpdateAllowedActionTakenStatusAsync(Guid id, bool active);
    }
}
