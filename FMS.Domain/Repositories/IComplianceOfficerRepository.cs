using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IComplianceOfficerRepository : IDisposable
    {
        Task<bool> ComplianceOfficerExistsAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id);
        Task<int> CountAsync(ComplianceOfficerSpec spec);
        Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync();
        Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer);
        Task UpdateComplianceOfficerAsync(Guid id, ComplianceOfficerEditDto complianceOfficerUpdates);
    }
}
