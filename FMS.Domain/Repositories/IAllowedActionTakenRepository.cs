using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IAllowedActionTakenRepository : IDisposable
    {
        Task<bool> AllowedActionTakenExistsAsync(Guid id);

        Task<bool> AllowedActionTakenExistsAsync(Guid eventTypeId, Guid actionTakenId);

        Task<AllowedActionTakenSpec> GetAllowedActionTakenByAATIdAsync(Guid? id);

        Task<IList<AllowedActionTakenSpec>> GetAllowedActionTakenListAsync(Guid eventTypeId);

        Task<Guid> CreateAllowedActionTakenAsync(AllowedActionTakenSpec allowedActionTaken);

        Task<Guid> UpdateAllowedActionTakenAsync(AllowedActionTakenSpec allowedActionTaken);

        Task<Guid> DeleteAllowedActionTakenAsync(Guid? id);
    }
}
