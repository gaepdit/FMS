using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface ICabinetRepository : IDisposable
    {
        Task<bool> CabinetExistsAsync(Guid id);
        Task<bool> CabinetNameExistsAsync(string name, Guid? ignoreId = null);
        Task<IReadOnlyList<CabinetSummaryDto>> GetCabinetListAsync(bool includeInactive = false);
        Task<CabinetSummaryDto> GetCabinetAsync(Guid id);
        Task<CabinetSummaryDto> GetCabinetByNameAsync(string name);
        Task<Guid> CreateCabinetAsync(CabinetCreateDto cabinetCreate);
        Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinetEdit);
    }
}
