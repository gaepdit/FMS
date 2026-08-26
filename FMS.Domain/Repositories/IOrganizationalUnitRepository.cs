using FMS.Domain.Dto;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IOrganizationalUnitRepository : IDisposable
    {
        Task<bool> OrganizationalUnitExistsAsync(Guid id);
        Task<bool> OrganizationalUnitNameExistsAsync(string name, Guid? ignoreId = null);
        Task<OrganizationalUnitEditDto> GetOrganizationalUnitAsync(Guid id);
        Task<OrganizationalUnit> GetUnitByNameAsync(string name);
        Task<IReadOnlyList<OrganizationalUnitSummaryDto>> GetOrganizationalUnitListAsync();
        Task<Guid> CreateOrganizationalUnitAsync(OrganizationalUnitCreateDto organizationalUnit);
        Task UpdateOrganizationalUnitAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates, Guid? programId);
        Task UpdateOrganizationalUnitStatusAsync(Guid id, bool active);
    }
}