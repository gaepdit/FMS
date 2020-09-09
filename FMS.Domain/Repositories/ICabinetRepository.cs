using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface ICabinetRepository : IDisposable
    {
        Task<bool> CabinetExistsAsync(Guid id);
        Task<CabinetDetailDto> GetCabinetAsync(Guid id);
        Task<IReadOnlyList<CabinetDetailDto>> GetCabinetListAsync();
        Task<Guid> CreateCabinetAsync(CabinetCreateDto cabinet);
        Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinet);
    }
}
