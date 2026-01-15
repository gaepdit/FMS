using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IParcelTypeRepository : IDisposable
    {
        Task<bool> ParcelTypeExistsAsync(Guid id);

        Task<bool> ParcelTypeNameExistsAsync(string name, Guid? ignoreId = null);

        Task<ParcelTypeEditDto> GetParcelTypeAsync(Guid id);

        Task<IReadOnlyList<ParcelTypeSummaryDto>> GetParcelTypeListAsync();

        Task<Guid> CreateParcelTypeAsync(ParcelTypeCreateDto parcelType);

        Task UpdateParcelTypeAsync(Guid Id, ParcelTypeEditDto parcelTypeUpdates);

        Task UpdateParcelTypeStatusAsync(Guid id, bool active);
    }
}
