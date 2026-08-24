using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IUserPositionRepository : IDisposable
    {
        Task<bool> UserPositionExistsAsync(Guid id);

        Task<bool> UserPositionNameExistsAsync(string name, Guid? ignoreId = null);

        Task<bool> UserPositionDescriptionExistsAsync(string description, Guid? ignoreId = null);

        Task<UserPositionEditDto> GetUserPositionAsync(Guid id);

        Task<UserPosition> GetPositionByNameAsync(string name);

        Task<IReadOnlyList<UserPositionSummaryDto>> GetUserPositionListAsync();

        Task<Guid> CreateUserPositionAsync(UserPositionCreateDto userPosition);

        Task UpdateUserPositionAsync(Guid Id, UserPositionEditDto userPositionUpdates);

        Task UpdateUserPositionStatusAsync(Guid id, bool active);
    }
}
