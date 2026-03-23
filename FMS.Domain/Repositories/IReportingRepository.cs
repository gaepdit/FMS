using FMS.Domain.Dto;
using FMS.Domain.Dto.Reports;
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

        Task<IList<EventReportDto>> GetEventsReportsAsync(List<string> facilityTypes = null, List<string> eventTypes = null);

        Task<IList<EventsNoActionTakenReportDto>> GetEventsNoActionTakenReportAsync();

        #endregion

        #region PAF Report

        Task<IReadOnlyList<PAFReportRawDto>> GetPAFReportAsync();

        #endregion

        #region HSI ListReports

        Task<IReadOnlyList<HSIListReportDto>> GetHSIListReportAsync(HSISortBy sortBy);

        #endregion

        #region Abandonment/Inactivity Reports

        Task<IReadOnlyList<AbndInacStatusTrackerDto>> GetAbndInacStatusTrackerReportAsync();

        Task<IReadOnlyList<AbndInacChecklistReviewDto>> GetAbndInacChecklistReviewAsync();

        Task<IReadOnlyList<AbndCostEstimateReportDto>> GetAbndCostEstimateReportAsync();

        #endregion

        #region Site Summary Report

        Task<IReadOnlyList<SiteSummaryReportDto>> GetFacilitySiteSummaryDtoAsync
            (SiteSummaryQuerySpec spec);

        Task<IReadOnlyList<FacilityBasicDto>> GetHsiFacilitiesAsync(SiteSummaryQuerySpec spec);

        #endregion
    }
}
