using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IUserProgramRepository : IDisposable
    {
        Task<bool> UserProgramExistsAsync(Guid id);

        Task<bool> UserProgramNameExistsAsync(string name, Guid? ignoreId = null);

        Task<bool> UserProgramDescriptionExistsAsync(string description, Guid? ignoreId = null);

        Task<UserProgramEditDto> GetUserProgramAsync(Guid id);

        Task<UserProgram> GetUserProgramByNameAsync(string name);

        Task<IReadOnlyList<UserProgramSummaryDto>> GetUserProgramListAsync();

        Task<Guid> CreateUserProgramAsync(UserProgramCreateDto userProgram);

        Task UpdateUserProgramAsync(Guid Id, UserProgramEditDto userProgramUpdates);

        Task UpdateUserProgramStatusAsync(Guid id, bool active);
    }
}
