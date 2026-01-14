using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ILocationClassRepository : IDisposable
    {
        Task<bool> LocationClassExistsAsync(Guid id);
        Task<bool> LocationClassNameExistsAsync(string name, Guid? ignoreId = null);
        Task<LocationClassEditDto> GetLocationClassAsync(Guid id);
        Task<string> GetLocationClassNameAsync(Guid? id);
        Task<IReadOnlyList<LocationClassSummaryDto>> GetLocationClassListAsync();
        Task<Guid> CreateLocationClassAsync(LocationClassCreateDto locationClass);
        Task UpdateLocationClassAsync(Guid id, LocationClassEditDto locationClassUpdates);
        Task UpdateLocationClassStatusAsync(Guid id, bool active);
    }
}
