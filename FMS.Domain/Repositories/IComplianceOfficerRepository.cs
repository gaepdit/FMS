using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IComplianceOfficerRepository : IDisposable
    {
        Task<bool> ComplianceOfficerIdExistsAsync(Guid id);
        Task<bool> ComplianceOfficerNameExistsAsync(string name);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(string name);
        Task<int> CountAsync(ComplianceOfficerSpec spec);
        Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync();
        Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer);
        Task UpdateComplianceOfficerAsync(Guid id, ComplianceOfficerEditDto complianceOfficerUpdates);
        Task UpdateComplianceOfficerStatusAsync(Guid id, bool active);
    }
}
