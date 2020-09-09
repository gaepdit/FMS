using FMS.Domain.Dto;
using System;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface ICabinetRepository : IDisposable
    {
        Task<bool> CabinetExistsAsync(Guid id);
        Task<bool> CabinetNameExistsAsync(string name, Guid? ignoreId);
        Task<CabinetDetailDto> GetCabinetAsync(Guid id);
        Task<Guid> CreateCabinetAsync(CabinetCreateDto cabinetCreate);
        Task UpdateCabinetAsync(Guid id, CabinetEditDto cabinetEdit);
    }
}
