using FMS.Domain.Dto;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IAllowedActionTakenRepository : IDisposable
    {
        Task<bool> AllowedActionTakenExistsAsync(Guid id);

        Task<bool> AllowedActionTakenExistsAsync(Guid eventTypeId, Guid actionTakenId);

        Task<AllowedActionTaken> GetAllowedActionTakenAsync(Guid eventTypeId, Guid actionTakenId);

        Task<IReadOnlyList<AllowedActionTakenSummaryDto>> GetAllowedActionTakenListAsync(Guid eventTypeId);

        Task<Guid> UpdateAllowedActionTakenAsync(Guid actionTakenId, Guid eventTypeId);
    }
}
