using FMS.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Domain.Repositories
{
    public interface IReportingRepository : IDisposable
    {
        #region Asignment Reports
        Task<IReadOnlyList<AssignmentListReportByCODto>> GetAsignmentListByCOAsync();

        Task<IReadOnlyList<AssignmentListReportByCountyDto>> GetAsignmentListByCountyAsync();

        Task<IReadOnlyList<AssignmentListReportByHSIDto>> GetAsignmentListByHSIAsync();

        Task<IReadOnlyList<AssignmentListReportBySiteNameDto>> GetAsignmentListBySiteNameAsync();

        Task<IReadOnlyList<AssignmentListReportByUnitDto>> GetAsignmentListByUnitAsync();
        #endregion

        #region Delisted Reports

        Task<IReadOnlyList<DelistedReportByDateDto>> GetDelistedByDateAsync();

        Task<IReadOnlyList<DelistedReportByDateRangeDto>> GetDelistedByDateRangeAsync(DateOnly? startDate, DateOnly? endDate);

        #endregion

        #region Events Reports

        Task<IList<EventReportDto>> GetEventsReportsAsync(List<string> selectedFacilityTypes = null, List<string> eventTypes = null);

        #endregion
    }
}
