using Dapper;
using FMS.Domain.Dto;
using FMS.Domain.Dto.Reports;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Infrastructure.Repositories
{
    public class ReportingRepository : IReportingRepository
    {
        private readonly FmsDbContext _context;
        public ReportingRepository(FmsDbContext context) => _context = context;

        #region Assignment Reports
        public async Task<IReadOnlyList<AssignmentListReportByCODto>> GetAsignmentListByCOAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI" || e.FacilityType.Name == "VRP")
                .Select(e => new AssignmentListReportByCODto(e))
                .ToListAsync();

            var reportDto = facilityList
                .OrderBy(e => e.ComplianceOfficerName)
                .ThenBy(e => e.FacilityName)
                .GroupBy(e => e.ComplianceOfficerName)
                .SelectMany(g => g)
                .ToList();

            return reportDto;
        }

        public async Task<IReadOnlyList<AssignmentListReportByCountyDto>> GetAsignmentListByCountyAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI" || e.FacilityType.Name == "VRP")
                .Select(e => new AssignmentListReportByCountyDto(e))
                .ToListAsync();

            var reportDto = facilityList
                .OrderBy(e => e.CountyName)
                .ThenBy(e => e.FacilityName)
                .GroupBy(e => e.CountyName)
                .SelectMany(g => g)
                .ToList();

            return reportDto;
        }

        public async Task<IReadOnlyList<AssignmentListReportByHSIDto>> GetAsignmentListByHSIAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI")
                .Select(e => new AssignmentListReportByHSIDto(e))
                .ToListAsync();

            var reportDto = facilityList
                .OrderBy(e => e.ComplianceOfficerName)
                .ThenBy(e => e.FacilityNumber)
                .GroupBy(e => e.ComplianceOfficerName)
                .SelectMany(g => g)
                .ToList();

            return reportDto;
        }

        public async Task<IReadOnlyList<AssignmentListReportBySiteNameDto>> GetAsignmentListBySiteNameAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI")
                .OrderBy(e => e.Name)
                .Select(e => new AssignmentListReportBySiteNameDto(e))
                .ToListAsync();

            return facilityList;
        }

        public async Task<IReadOnlyList<AssignmentListReportByUnitDto>> GetAsignmentListByUnitAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI")
                .Select(e => new AssignmentListReportByUnitDto(e))
                .ToListAsync();

            var reportDto = facilityList
                .OrderBy(e => e.ComplianceOfficer)
                .ThenBy(e => e.FacilityName)
                .GroupBy(e => e.OrganizationalUnit)
                .SelectMany(g => g)
                .ToList();

            return reportDto;
        }
        #endregion

        #region Delisted Reports

        public async Task<IReadOnlyList<DelistedReportByDateDto>> GetDelistedByDateAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI" || e.FacilityType.Name == "VRP")
                .Where(e => e.HsrpFacilityProperties.DateDeListed != null)
                .Select(e => new DelistedReportByDateDto(e))
                .ToListAsync();
            var reportDto = facilityList
                .OrderBy(e => e.DelistedDate)
                .ToList();
            return reportDto;
        }

        public async Task<IReadOnlyList<DelistedReportByDateRangeDto>> GetDelistedByDateRangeAsync(DateOnly? startDate, DateOnly? endDate)
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.Parcels)
                .Where(e => e.FacilityType.Name == "HSI" || e.FacilityType.Name == "VRP")
                .Where(e => e.HsrpFacilityProperties.DateDeListed != null && e.HsrpFacilityProperties.DateDeListed >= startDate.GetValueOrDefault() && e.HsrpFacilityProperties.DateDeListed <= endDate.GetValueOrDefault())
                .Select(e => new DelistedReportByDateRangeDto(e))
                .ToListAsync();
            var reportDto = facilityList
                .OrderBy(e => e.HSIID)
                .GroupBy(e => e.HSRAComplianceOfficerName)
                .SelectMany(g => g)
                .ToList();
            return reportDto;
        }

        #endregion

        #region Events Reports

        public async Task<IList<EventReportDto>> GetEventsReportsAsync(
            List<string> facilityTypes = null,
            List<string> eventTypes = null
            )
        {
            List<EventReportDto> reportDtoList = await _context.Facilities
                .AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.StatusDetails)
                .Include(sd => sd.StatusDetails.OverallStatus)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.EventType)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.ActionTaken)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.ComplianceOfficer)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.EventContractor)
                .Where(e => facilityTypes.Contains(e.FacilityType.Name))
                .SelectMany(e => e.Events.Select(ev => new EventReportDto()
                {
                    Id = ev.Id,
                    FacilityId = e.Id,
                    ParentId = ev.ParentId,
                    FacilityNumber = e.FacilityNumber,
                    FacilityName = e.Name,
                    FacilityType = e.FacilityType,
                    EventType = ev.EventType,
                    ActionTaken = ev.ActionTaken,
                    StartDate = ev.StartDate,
                    DueDate = ev.DueDate,
                    CompletionDate = ev.CompletionDate,
                    DoneBy = ev.ComplianceOfficer,
                    OrganizationalUnit = e.OrganizationalUnit,
                    ComplianceOfficer = e.ComplianceOfficer,
                    EventAmount = ev.EventAmount,
                    EventContractor = ev.EventContractor,
                    Comment = ev.Comment,
                    OverallStatus = e.StatusDetails.OverallStatus,
                    ListDate = e.HsrpFacilityProperties.DateListed
                })
                .Where(ev => eventTypes == null || eventTypes.Contains(ev.EventType.Name)))
                .ToListAsync();

            return reportDtoList;
        }

        public async Task<IList<EventsNoActionTakenReportDto>> GetEventsNoActionTakenReportAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.StatusDetails.OverallStatus)
                .Include(e => e.ComplianceOfficer)
                .Where(e => e.FacilityType.Name == "HSI")
                .Where(e => e.StatusDetails.OverallStatus.Name == "NAT")
                .Select(e => new EventsNoActionTakenReportDto()
                {
                    HSIID = e.FacilityNumber,
                    FacilityName = e.Name,
                    ListDate = e.HsrpFacilityProperties.DateListed,
                    ComplianceOfficerName = e.ComplianceOfficer != null ? e.ComplianceOfficer.Name : "Unassigned"
                })
                .OrderBy(e => e.HSIID)
                .ToListAsync();

            return facilityList;
        }

        #endregion

        #region PAF Report

        public async Task<IReadOnlyList<PAFReportRawDto>> GetPAFReportAsync()
        {
            var conn = _context.Database.GetDbConnection();

            return (await conn.QueryAsync<PAFReportRawDto>("dbo.PAF_Report", commandType: CommandType.StoredProcedure)).ToList();
        }

        #endregion

        #region HSI List Reports

        public async Task<IReadOnlyList<HSIListReportDto>> GetHSIListReportAsync(HSISortBy sortBy)
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.LocationDetails)
                .Include(e => e.LocationDetails.LocationClass)
                .Include(e => e.County)
                .Where(e => e.FacilityType.Name == "HSI")
                .Select(e => new HSIListReportDto()
                {
                    HSINumber = e.FacilityNumber,
                    Name = e.Name,
                    County = e.County.Name,
                    ClassName = e.LocationDetails.LocationClass.Name
                })
                .ToListAsync();

            return OrderHSIReportQuery(facilityList, sortBy);
        }

        private static IReadOnlyList<HSIListReportDto> OrderHSIReportQuery(
            IList<HSIListReportDto> facilityList, HSISortBy sortBy) =>
            sortBy switch
            {
                HSISortBy.HSINumber => facilityList
                    .OrderBy(e => e.HSINumber)
                    .ToList(),
                HSISortBy.Name => facilityList
                    .OrderBy(e => e.Name)
                    .ToList(),
                HSISortBy.County => facilityList
                    .OrderBy(e => e.County)
                    .ThenBy(e => e.HSINumber)
                    .ToList(),
                HSISortBy.ClassName => facilityList
                    .OrderBy(e => e.ClassName)
                    .ThenBy(e => e.HSINumber)
                    .ToList(),
                _ => facilityList
                    .OrderBy(e => e.Name)
                    .ToList()
            };

        #endregion

        #region Abnd/Inac Status Tracker Report

        public async Task<IReadOnlyList<AbndInacStatusTrackerDto>> GetAbndInacStatusTrackerReportAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.GroundwaterScoreDetails)
                .Include(e => e.OnsiteScoreDetails)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.County)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.StatusDetails)
                .Include(e => e.StatusDetails.OverallStatus)
                .Include(e => e.StatusDetails.AbandonedInactive)
                .Include(e => e.StatusDetails.GAPSAssessment)
                .Where(e => e.FacilityType.Name == "HSI")
                .Where(e => e.StatusDetails.OverallStatus.Name == "ABND" || e.StatusDetails.OverallStatus.Name == "INAC")
                .Select(e => new AbndInacStatusTrackerDto()
                {
                    HSINumber = e.FacilityNumber,
                    FacilityName = e.Name,
                    City = e.City,
                    County = e.County.Name,
                    AbndInac = e.StatusDetails.AbandonedInactive != null ? e.StatusDetails.AbandonedInactive.Name : "None",
                    GAPSModelDate = e.StatusDetails.GAPSModelDate,
                    GAPSScore = e.StatusDetails.GAPSScore,
                    GAPSNoOfUnknowns = e.StatusDetails.GAPSNoOfUnknowns,
                    GAPSAssessment = e.StatusDetails.GAPSAssessment != null ? e.StatusDetails.GAPSAssessment.Name : "None",
                    CostEstimate = e.StatusDetails.CostEstimate,
                    AbndInacInfo = e.StatusDetails.AbandonedInactive != null ? e.StatusDetails.AbandonedInactive.Name : "None",
                    UnitName = e.OrganizationalUnit != null ? e.OrganizationalUnit.Name : "None",
                    EventComments = e.StatusDetails.ReportComments,
                    COName = e.ComplianceOfficer != null ? e.ComplianceOfficer.Name : "Unassigned",
                    GWScore = e.GroundwaterScoreDetails != null ? e.GroundwaterScoreDetails.GWScore : null,
                    OnSiteScore = e.OnsiteScoreDetails != null ? e.OnsiteScoreDetails.OnsiteScoreValue : null
                })
                .OrderBy(e => e.HSINumber)
                .ToListAsync();
            return facilityList;
        }

        public async Task<IReadOnlyList<AbndInacChecklistReviewDto>> GetAbndInacChecklistReviewAsync()
        {
            var eventList = await _context.Facilities.AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.County)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.EventType)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.ActionTaken)
                .Include(e => e.Events)
                .ThenInclude(ev => ev.ComplianceOfficer)
                .Include(e => e.StatusDetails)
                .Include(e => e.StatusDetails.OverallStatus)
                .Where(e => e.FacilityType.Name == "HSI")
                .Where(e => e.StatusDetails.OverallStatus.Name == "ABND" || e.StatusDetails.OverallStatus.Name == "INAC")
                .SelectMany(e => e.Events.Select(ev => new AbndInacChecklistReviewDto()
                {
                    HSINumber = e.FacilityNumber,
                    FacilityName = e.Name,
                    City = e.City,
                    County = e.County.Name,
                    AbndInac = e.StatusDetails.OverallStatus.Name,
                    EventType = ev.EventType,
                    ActionTaken = ev.ActionTaken,
                    StartDate = ev.StartDate,
                    DueDate = ev.DueDate,
                    CompletionDate = ev.CompletionDate,
                    ComplianceOfficer = ev.ComplianceOfficer,
                    Comment = ev.Comment
                }))
                .Where(ev => ev.EventType.Name == "Abandoned/Inactive Site Review")
                .OrderBy(ev => ev.HSINumber)
                .ThenBy(ev => ev.CompletionDate)
                .ToListAsync();

            return eventList;
        }

        public async Task<IReadOnlyList<AbndCostEstimateReportDto>> GetAbndCostEstimateReportAsync()
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.FacilityType)
                .Include(e => e.LocationDetails)
                .Include(e => e.LocationDetails.LocationClass)
                .Include(e => e.County)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.StatusDetails)
                .Where(e => e.FacilityType.Name == "HSI")
                .Where(e => e.StatusDetails.OverallStatus.Name == "ABND" || e.StatusDetails.OverallStatus.Name == "INAC")
                .Select(e => new AbndCostEstimateReportDto()
                {
                    HSINumber = e.FacilityNumber,
                    FacilityName = e.Name,
                    County = e.County.Name,
                    ClassName = e.LocationDetails.LocationClass.Name,
                    COName = e.ComplianceOfficer != null ? e.ComplianceOfficer.Name : "Unassigned",
                    GAPSScore = e.StatusDetails.GAPSScore,
                    GAPSModelDate = e.StatusDetails.GAPSModelDate,
                    GAPSNoOfUnknowns = e.StatusDetails.GAPSNoOfUnknowns,
                    GAPSAssessment = e.StatusDetails.GAPSAssessment != null ? e.StatusDetails.GAPSAssessment.Name : "None",
                    CostEstimate = e.StatusDetails.CostEstimate
                })
                .OrderByDescending(e => e.GAPSScore)
                .ToListAsync();
            return facilityList;
        }

        #endregion

        #region Site Summary Report

        public async Task<IReadOnlyList<SiteSummaryDto>> GetFacilitySiteSummaryDtoAsync
            (SiteSummaryQuerySpec spec)
        {
            var facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.FacilityType)
                .Include(e => e.BudgetCode)
                .Include(e => e.OrganizationalUnit)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.OrganizationalUnit)
                .Include(e => e.LocationDetails)
                .Include(e => e.LocationDetails.LocationClass)
                .Include(e => e.Parcels)
                .Include(e => e.Contacts)
                .ThenInclude(e => e.ContactType)
                .Include(e => e.ScoreDetails)
                .Include(e => e.GroundwaterScoreDetails)
                .ThenInclude(e => e.Substance)
                .ThenInclude(e => e.Chemical)
                .Include(e => e.OnsiteScoreDetails)
                .ThenInclude(e => e.Substance)
                .ThenInclude(e => e.Chemical)
                .Include(e => e.Substances)
                .ThenInclude(e => e.Chemical)
                .Include(e => e.StatusDetails)
                .Include(e => e.StatusDetails.SourceStatus)
                .Include(e => e.StatusDetails.SoilStatus)
                .Include(e => e.StatusDetails.GroundwaterStatus)
                .Include(e => e.StatusDetails.OverallStatus)
                .Include(e => e.StatusDetails.GAPSAssessment)
                .Where(e => e.FacilityType.Name == "HSI")
                .Where(e => string.IsNullOrEmpty(spec.FacilityNumber) || e.FacilityNumber == spec.FacilityNumber)
                .Where(e => !spec.CountyId.HasValue || e.County.Id == spec.CountyId.Value)
                .Where(e => !spec.ComplianceOfficerId.HasValue || e.ComplianceOfficer.Id.Equals(spec.ComplianceOfficerId))
                .Where(e => !spec.LocationClassId.HasValue || e.LocationDetails.LocationClass.Id.Equals(spec.LocationClassId))
                .Where(e => !spec.OrganizationalUnitId.HasValue || e.OrganizationalUnit.Id.Equals(spec.OrganizationalUnitId))
                .Where(e => !spec.AdditionalOrganizationalUnitId.HasValue || e.HsrpFacilityProperties.ComplianceOfficer.Id.Equals(spec.AdditionalOrganizationalUnitId))
                .Where(e => !spec.IsLandFill || e.StatusDetails.LandFill)
                .Select(e => new SiteSummaryDto(e))
                .ToListAsync();

            return facilityList;
        }

        #endregion

        #region IDisposable Support

        private bool _disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue) return;

            if (disposing)
            {
                // dispose managed state (managed objects)
                _context.Dispose();
            }

            // free unmanaged resources (unmanaged objects) and override finalizer
            // set large fields to null
            _disposedValue = true;
        }

        // override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        ~ReportingRepository()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
