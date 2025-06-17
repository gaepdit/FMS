using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ISourceStatusRepository : IDisposable
    {
        Task<bool> SourceStatusExistsAsync(Guid id);

        Task<bool> SourceStatusNameExistsAsync(string name, Guid? ignoreId = null);

        Task<bool> SourceStatusDescriptionExistsAsync(string description, Guid? ignoreId = null);

        Task<SourceStatusEditDto> GetSourceStatusAsync(Guid id);

        Task<SourceStatusEditDto> GetSourceStatusByNameAsync(string name);

        Task<IReadOnlyList<SourceStatusSummaryDto>> GetSourceStatusListAsync();

        Task<bool> CreateSourceStatusAsync(SourceStatusCreateDto sourceStatus);

        Task UpdateSourceStatusAsync(Guid id, SourceStatusEditDto sourceStatusUpdates);

        Task UpdateSourceStatusStatusAsync(Guid id, bool active);
    }
}
