using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IComplianceOfficerRepository : IDisposable
    {
        Task<bool> ComplianceOfficerIdExistsAsync(Guid id);
        Task<ComplianceOfficerDetailDto> GetComplianceOfficerAsync(Guid id);
        Task<ComplianceOfficerSummaryDto> GetComplianceOfficerSummaryAsync(Guid id);
        Task<IReadOnlyList<ComplianceOfficerSummaryDto>> GetComplianceOfficerListAsync();
        Task<List<Guid>> GetComplianceOfficerListByUnitAsync(Guid UnitId);
        Task<List<Guid>> GetComplianceOfficerListByProgramAsync(Guid UnitId, bool IncludeInactive = false);
        Task<Guid?> TryCreateComplianceOfficerAsync(ComplianceOfficerCreateDto complianceOfficer);
        Task UpdateComplianceOfficerStatusAsync(Guid id, bool active);
        Task DeleteComplianceOfficerAsync(Guid id);
    }
}