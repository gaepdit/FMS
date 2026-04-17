using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IActionTakenRepository : IDisposable
    {
        Task<bool> ActionTakenExistsAsync(Guid id);
        Task<bool> ActionTakenNameExistsAsync(string name, Guid? ignoreId = null);
        Task<ActionTakenEditDto> GetActionTakenAsync(Guid id);
        Task<IReadOnlyList<ActionTakenSummaryDto>> GetActionTakenListAsync(bool ActiveOnly = false);
        Task<Guid> CreateActionTakenAsync(ActionTakenCreateDto actionTaken);
        Task UpdateActionTakenAsync(Guid id, ActionTakenEditDto actionTaken);
        Task UpdateActionTakenStatusAsync(Guid id, bool active);
    }
}
