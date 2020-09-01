using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IOrganizationalUnitRepository : IDisposable
    {
        Task<bool> OrganizationalUnitExistsAsync(Guid id);
        Task<OrganizationalUnitDetailDto> GetOrganizationalUnitAsync(Guid id);
        Task<int> CountAsync(OrganizationalUnitSpec spec);
        Task<IReadOnlyList<OrganizationalUnitSummaryDto>> GetOrganizationalUnitListAsync();
        Task<Guid> CreateOrganizationalUnitAsync(OrganizationalUnitCreateDto organizationalUnit);
        Task UpdateOrganizationalUnitAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates);
        Task<int> DeleteOrganizationalUnitAsync(OrganizationalUnitDetailDto deletedOrganizationalUnit);
    }
}
