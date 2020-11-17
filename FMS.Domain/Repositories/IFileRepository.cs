using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;

namespace FMS.Domain.Repositories
{
    public interface IFileRepository : IDisposable
    {
        Task<bool> FileExistsAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(string fileLabel);
        Task<bool> FileHasActiveFacilities(Guid id);
        Task<int> CountAsync(FileSpec spec);
        Task<PaginatedList<FileDetailDto>> GetFileListAsync(FileSpec spec, int pageNumber, int pageSize);
        Task UpdateFileAsync(Guid id, bool active);
    }
}