using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface ICabinetRepository : IDisposable
    {
        Task<bool> CabinetExistsAsync(Guid id);
        Task<IReadOnlyList<CabinetSummaryDto>> GetCabinetListAsync(bool includeInactive = true);
        Task<CabinetSummaryDto> GetCabinetSummaryAsync(Guid id);
        Task<CabinetSummaryDto> GetCabinetSummaryAsync(string name);
        Task<CabinetDetailDto> GetCabinetDetailsAsync(Guid id);
        Task<CabinetDetailDto> GetCabinetDetailsAsync(int cabinetNumber);
        Task<string> GetNextCabinetName();
        Task<int> CreateCabinetAsync();
        Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinetEdit);
    }
}
