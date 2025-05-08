using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Repositories
{
    public interface ISourceStatusRepository : IDisposable
    {
        Task<bool> SourceStatusExistsAsync(Guid id);

        Task<bool> SourceStatusNameExistsAsync(string name, Guid? ignoreId = null);

        Task<SourceStatusEditDto> GetSourceStatusAsync(Guid id);

        Task<IReadOnlyList<SourceStatusSummaryDto>> GetSourceStatusListAsync();

        Task<Guid> CreateSourceStatusAsync(SourceStatusCreateDto sourceStatus);

        Task UpdateSourceStatusAsync(Guid id, SourceStatusEditDto sourceStatusUpdates);

        Task UpdateSourceStatusStatusAsync(Guid id, bool active);
    }
}
