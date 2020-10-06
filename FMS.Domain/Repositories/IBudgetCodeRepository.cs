using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IBudgetCodeRepository : IDisposable
    {
        Task<bool> BudgetCodeExistsAsync(Guid id);
        Task<BudgetCodeDetailDto> GetBudgetCodeAsync(Guid id);
        Task<int> CountAsync();
        Task<IReadOnlyList<BudgetCodeSummaryDto>> GetBudgetCodeListAsync();
        Task<Guid> CreateBudgetCodeAsync(BudgetCodeCreateDto budgetCode);
        Task UpdateBudgetCodeAsync(Guid id, BudgetCodeEditDto budgetCodeUpdates);
    }
}
