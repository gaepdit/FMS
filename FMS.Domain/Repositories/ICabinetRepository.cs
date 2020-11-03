using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ICabinetRepository : IDisposable
    {
        Task<bool> CabinetExistsAsync(Guid id);
        Task<bool> CabinetNameExistsAsync(string name, Guid? ignoreId = null);
        Task<IReadOnlyList<CabinetSummaryDto>> GetCabinetListAsync(bool includeInactive = true);
        Task<CabinetSummaryDto> GetCabinetSummaryAsync(Guid id);
        Task<CabinetDetailDto> GetCabinetDetailsAsync(string name);
        Task CreateCabinetAsync(CabinetEditDto cabinet);
        Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinetEdit);
    }
}