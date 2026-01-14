using FMS.Domain.Dto;

namespace FMS.Domain.Repositories
{
    public interface IReportingRepository : IDisposable
    {
        Task<IReadOnlyList<AssignmentListReportByCODto>> GetAsignmentListByCOAsync();

        Task<IReadOnlyList<AssignmentListReportByCountyDto>> GetAsignmentListByCountyAsync();

        Task<IReadOnlyList<AssignmentListReportByHSIDto>> GetAsignmentListByHSIAsync();

        Task<IReadOnlyList<AssignmentListReportBySiteNameDto>> GetAsignmentListBySiteNameAsync();

        Task<IReadOnlyList<AssignmentListReportByUnitDto>> GetAsignmentListByUnitAsync();
    }
}
