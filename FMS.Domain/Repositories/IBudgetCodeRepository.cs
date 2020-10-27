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
        Task<IReadOnlyList<BudgetCodeSummaryDto>> GetBudgetCodeListAsync();
        Task<Guid> CreateBudgetCodeAsync(BudgetCodeCreateDto budgetCode);
        Task UpdateBudgetCodeAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates);
        Task<bool> BudgetCodeCodeExistsAsync(string code, Guid? ignoreId = null);
        Task<bool> BudgetCodeNameExistsAsync(string name, Guid? ignoreId = null);
        Task UpdateBudgetCodeStatusAsync(Guid id, bool active);
    }
}
