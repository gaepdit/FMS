using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IStatusRepository : IDisposable
    {
        Task<bool> StatusExistsAsync(Guid id);

        Task<StatusEditDto> GetStatusAsync(Guid facilityId);

        Task<Guid> CreateStatusAsync(StatusCreateDto status);

        Task<bool> UpdateStatusAsync(Guid id, StatusEditDto statusUpdates);

        Task UpdateStatusStatusAsync(Guid id, bool active);
    }
}
