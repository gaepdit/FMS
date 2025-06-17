using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ISoilStatusRepository : IDisposable
    {
        Task<bool> SoilStatusExistsAsync(Guid id);

        Task<bool> SoilStatusNameExistsAsync(string name, Guid? ignoreId = null);

        Task<bool> SoilStatusDescriptionExistsAsync(string description, Guid? ignoreId = null);

        Task<SoilStatusEditDto> GetSoilStatusAsync(Guid id);

        Task<SoilStatusEditDto> GetSoilStatusByNameAsync(string name);

        Task<List<SoilStatusSummaryDto>> GetSoilStatusListAsync();

        Task<bool> CreateSoilStatusAsync(SoilStatusCreateDto soilStatus);

        Task UpdateSoilStatusAsync(Guid id, SoilStatusEditDto soilStatusUpdates);

        Task UpdateSoilStatusStatusAsync(Guid id, bool active);
    }
}
