using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IOverallStatusRepository : IDisposable
    {
        Task<bool> OverallStatusExistsAsync(Guid id);

        Task<OverallStatusEditDto> GetOverallStatusByIdAsync(Guid id);

        Task<IReadOnlyList<OverallStatusSummaryDto>> GetOverallStatusListsAsync();

        Task<Guid> CreateOverallStatusAsync(OverallStatusCreateDto overallStatus);

        Task UpdateOverallStatusAsync(Guid Id, OverallStatusEditDto overallStatusUpdates);

        Task UpdateOverallStatusStatusAsync(Guid id, bool active);
    }
}
