using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface ISoilStatusRepository : IDisposable
    {
        Task<bool> SoilStatusExistsAsync(Guid id);

        Task<bool> SoilStatusHasActiveFacilities(Guid id);

        Task<List<SoilStatusSummaryDto>> GetSoilStatusListAsync();

        Task<SoilStatusEditDto> GetSoilStatusAsync(Guid id);

        Task<SoilStatusEditDto> GetSoilStatusAsync(string name);

        Task<bool> CreateSoilStatusAsync(SoilStatusCreateDto soilStatusCreateDto);

        Task UpdateSoilStatusAsync(Guid id, SoilStatusEditDto soilStatusEditDto);

        Task UpdateSoilStatusAsync(Guid id, bool active);
    }
}
