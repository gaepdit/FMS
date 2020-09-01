using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IEnvironmentalInterestRepository : IDisposable
    {
        Task<bool> EnvironmentalInterestExistsAsync(Guid id);
        Task<EnvironmentalInterestDetailDto> GetEnvironmentalInterestAsync(Guid id);
        Task<int> CountAsync(EnvironmentalInterestSpec spec);
        Task<IReadOnlyList<EnvironmentalInterestSummaryDto>> GetEnvironmentalInterestListAsync();
        Task<Guid> CreateEnvironmentalInterestAsync(EnvironmentalInterestCreateDto environmentalInterest);
        Task UpdateEnvironmentalInterestAsync(Guid id, EnvironmentalInterestEditDto environmentalInterestUpdates);
        Task<int> DeleteEnvironmentalInterestAsync(EnvironmentalInterestDetailDto deletedEnvironmentalInterest);
    }
}
