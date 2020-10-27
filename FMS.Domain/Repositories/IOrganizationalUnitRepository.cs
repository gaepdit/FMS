using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IOrganizationalUnitRepository : IDisposable
    {
        Task<bool> OrganizationalUnitExistsAsync(Guid id);
        Task<bool> OrganizationalUnitNameExistsAsync(string name, Guid? ignoreId = null);
        Task<OrganizationalUnitEditDto> GetOrganizationalUnitAsync(Guid id);
        Task<IReadOnlyList<OrganizationalUnitSummaryDto>> GetOrganizationalUnitListAsync();
        Task<Guid> CreateOrganizationalUnitAsync(OrganizationalUnitCreateDto organizationalUnit);
        Task UpdateOrganizationalUnitAsync(Guid id, OrganizationalUnitEditDto organizationalUnitUpdates);
        Task UpdateOrganizationalUnitStatusAsync(Guid id, bool active);
    }
}