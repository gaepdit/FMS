using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IOverallStatusRepository : IDisposable
    {
        Task<bool> OverallStatusExistsAsync(Guid id);

        Task<bool> OverallStatusNameExistsAsync(string name, Guid? ignoreId = null);

        Task<bool> OverallStatusDescriptionExistsAsync(string description, Guid? ignoreId = null);

        Task<OverallStatusEditDto> GetOverallStatusAsync(Guid id);

        Task<string> GetOverallStatusNameAsync(Guid? id);

        Task<IReadOnlyList<OverallStatusSummaryDto>> GetOverallStatusListAsync();

        Task<Guid> CreateOverallStatusAsync(OverallStatusCreateDto overallStatus);

        Task UpdateOverallStatusAsync(Guid id, OverallStatusEditDto overallStatusUpdates);

        Task UpdateOverallStatusStatusAsync(Guid id, bool active);
    }
}
