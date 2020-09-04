using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IFileRepository : IDisposable
    {
        Task<bool> FileExistsAsync(Guid id);
        Task<FileDetailDto> GetFileAsync(Guid id);
        Task<int> CountAsync(FileSpec spec);
        Task<IReadOnlyList<FileSummaryDto>> GetFileListAsync(int? Cnty);
        Task<Guid> CreateFileAsync(FileCreateDto file);
        Task UpdateFileAsync(Guid id, FileEditDto fileUpdates);
    }
}
