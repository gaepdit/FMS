using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IFileRepository : IDisposable
    {
        Task<bool> FileExistsAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(string id);
        Task<List<FacilitySummaryDto>> GetFacilitiesForFileAsync(Guid id);
        Task<bool> FileHasActiveFacilities(Guid id);
        Task<int> CountAsync(FileSpec spec);
        Task<IReadOnlyList<FileDetailDto>> GetFileListAsync(FileSpec spec);
        Task<int> GetNextSequenceForCountyAsync(int countId);
        Task<Guid> CreateFileAsync(int countyId);
        Task UpdateFileAsync(Guid id, bool active);

        // TODO #49: Add Cabinets relationship
        Task AddCabinetToFileAsync(Guid fileId, Guid cabinetId);
        Task RemoveCabinetFromFileAsync(Guid fileId, Guid cabinetId);
    }
}
