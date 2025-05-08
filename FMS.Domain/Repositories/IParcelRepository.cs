using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IParcelRepository
    {
        Task<bool> ParcelExistsAsync(Guid id);

        Task<OverallStatusEditDto> GetParcelByIdAsync(Guid id);

        Task<IReadOnlyList<ParcelSummaryDto>> GetParcelListAsync(Guid facilityId);

        Task<Guid> CreateParcelAsync(ParcelCreateDto parcelCreate);

        Task UpdateParcelAsync(Guid Id, ParcelEditDto parcelUpdates);

        Task UpdateParcelAsync(Guid id, bool active);
    }
}
