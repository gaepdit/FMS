using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IGroundwaterStatusRepository : IDisposable
    {
        Task<bool> GroundwaterStatusExistsAsync(Guid id);
        Task<bool> GroundwaterStatusNameExistsAsync(string name, Guid? ignoreId = null);
        Task<bool> GroundwaterStatusDescriptionExistsAsync(string description, Guid? ignoreId = null);
        Task<GroundwaterStatusEditDto> GetGroundwaterStatusAsync(Guid id);
        Task<string> GetGroundwaterStatusNameAsync(Guid? id);
        Task<IReadOnlyList<GroundwaterStatusSummaryDto>> GetGroundwaterStatusListAsync();
        Task<Guid> CreateGroundwaterStatusAsync(GroundwaterStatusCreateDto groundwaterStatus);
        Task UpdateGroundwaterStatusAsync(Guid id, GroundwaterStatusEditDto groundwaterStatusUpdates);
        Task UpdateGroundwaterStatusStatusAsync(Guid id, bool active);
    }
}
