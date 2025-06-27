using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IParcelRepository : IDisposable
    {
        Task<bool> ParcelExistsAsync(Guid id);

        Task<ParcelEditDto> GetParcelByIdAsync(Guid id);

        Task<IReadOnlyList<ParcelSummaryDto>> GetParcelListAsync(Guid locationId);

        Task<Guid> CreateParcelAsync(ParcelCreateDto parcelCreate);

        Task UpdateParcelAsync(Guid id, ParcelEditDto parcelUpdates);

        Task UpdateParcelStatusAsync(Guid id, bool active);
    }
}
