using System;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IStatusRepository : IDisposable
    {
        Task<bool> StatusExistsAsync(Guid id);

        Task<SourceStatusEditDto> GetStatusAsync(Guid id);

        Task<Guid> CreateStatusAsync(StatusCreateDto status);

        Task UpdateStatusAsync(Guid id, StatusEditDto statusUpdates);

        Task UpdateStatusStatusAsync(Guid id, bool active);
    }
}
