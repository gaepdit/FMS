using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;

namespace FMS.Domain.Repositories
{
    public interface IFileRepository : IDisposable
    {
        Task<bool> FileExistsAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(string id);
        Task<bool> FileHasActiveFacilities(Guid id);
        Task<int> CountAsync(FileSpec spec);
        Task<PaginatedList<FileDetailDto>> GetFileListAsync(FileSpec spec, int pageNumber, int pageSize);
        Task<int> GetNextSequenceForCountyAsync(int countyId);
        // Task<Guid> GetPreferredCabinetForFile(string fileLabel);
        Task UpdateFileAsync(Guid id, bool active);

        Task<List<CabinetSummaryDto>> GetCabinetsForFileAsync(Guid id);
        Task<List<CabinetSummaryDto>> GetCabinetsAvailableForFileAsync(Guid id);
        Task AddCabinetToFileAsync(Guid cabinetId, Guid fileId);
        Task RemoveCabinetFromFileAsync(Guid cabinetId, Guid fileId);
    }
}