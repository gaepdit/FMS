using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFileCabinetRepository : IDisposable
    {
        Task<bool> FileCabinetExistsAsync(Guid id);
        Task<FileCabinetDetailDto> GetFileCabinetAsync(Guid id);
        Task<int> CountAsync(FileCabinetSpec spec);
        Task<IReadOnlyList<FileCabinetSummaryDto>> GetFileCabinetListAsync();
        Task<Guid> CreateFileCabinetAsync(FileCabinetCreateDto fileCabinet);
        Task UpdateFileCabinetAsync(Guid id, FileCabinetEditDto fileCabinetUpdates);
        Task<int> DeleteFileCabinetAsync(FileCabinetDetailDto deletedFileCabinet);
    }
}
