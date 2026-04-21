using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IAbandonedInactiveRepository : IDisposable
    {
        Task<bool> AbandonedInactiveExistsAsync(Guid id);
        Task<bool> AbandonedInactiveNameExistsAsync(string name, Guid? ignoreId = null);
        Task<bool> AbandonedInactiveDescriptionExistsAsync(string description, Guid? ignoreId = null);
        Task<IReadOnlyList<AbandonedInactiveSummaryDto>> GetAbandonedInactiveListAsync(bool activeOnly = false);
        Task<AbandonedInactiveEditDto> GetAbandonedInactiveAsync(Guid id);
        Task<Guid> CreateAbandonedInactiveAsync(AbandonedInactiveCreateDto abandonedInactive);
        Task UpdateAbandonedInactiveAsync(Guid id, AbandonedInactiveEditDto abandonedInactive);
        Task UpdateAbandonedInactiveStatusAsync(Guid id, bool active);
    }
}
