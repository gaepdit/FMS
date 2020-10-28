using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IComplianceOfficerRepository : IDisposable
    {
        Task<bool> ComplianceOfficerIdExistsAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(string email);
        Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync();
        Task<Guid> CreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer);
        Task<Guid?> TryCreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer);
        Task UpdateComplianceOfficerStatusAsync(Guid id, bool active);
    }
}