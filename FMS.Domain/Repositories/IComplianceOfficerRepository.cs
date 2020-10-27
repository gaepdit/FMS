using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FMS.Domain.Entities;

namespace FMS.Domain.Repositories
{
    public interface IComplianceOfficerRepository : IDisposable
    {
        Task<bool> ComplianceOfficerIdExistsAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(string familyName, string givenName);
        Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync();
        Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer);
        Task UpdateComplianceOfficerStatusAsync(Guid id, bool active);
    }
}
