using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IBudgetCodeRepository : IDisposable
    {
        Task<bool> BudgetCodeExistsAsync(Guid id);
        Task<BudgetCodeEditDto> GetBudgetCodeAsync(Guid id);
        Task<int> CountAsync();
        Task<IReadOnlyList<BudgetCodeSummaryDto>> GetBudgetCodeListAsync();
        Task<Guid> CreateBudgetCodeAsync(BudgetCodeCreateDto budgetCode);
        Task<Guid> CreateBudgetCodeInternalAsync(BudgetCodeCreateDto budgetCode);
        Task UpdateBudgetCodeAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates);
        Task UpdateBudgetCodeInternalAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates);
        Task<bool> BudgetCodeCodeExistsAsync(string budgetCodeCode, Guid? ignoreId = null);
        Task UpdateBudgetCodeStatusAsync(Guid id, bool active);
    }
}
