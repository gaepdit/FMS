using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IFundingSourceRepository
    {
        Task<bool> FundingSourceExistsAsync(Guid id);

        Task<FundingSourceEditDto> GetFundingSourceByIdAsync(Guid id);

        Task<IReadOnlyList<FundingSourceSummaryDto>> GetFundingSourceListsAsync();

        Task<Guid> CreateFundingSourceAsync(FundingSourceCreateDto fundingSource);

        Task UpdateFundingSourceAsync(Guid Id, FundingSourceEditDto fundingSourceUpdates);

        Task UpdateFundingSourceStatusAsync(Guid id, bool active);
    }
}
