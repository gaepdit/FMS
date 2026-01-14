using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
