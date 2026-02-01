using DocumentFormat.OpenXml.Office2010.Excel;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<IReadOnlyList<EventReportDto>> GetEventsReportsAsync(EventReportType eventReportType)
        {
            List<EventReportDto> facilityList = await _context.Facilities.AsNoTracking()
                .Include(e => e.County)
                .Include(e => e.FacilityStatus)
                .Include(e => e.HsrpFacilityProperties)
                .Include(e => e.HsrpFacilityProperties.ComplianceOfficer)
                .Include(e => e.FacilityType)
                .Include(e => e.Events)
                .Include(e => e.ComplianceOfficer)
                .Include(e => e.OrganizationalUnit)
                .Where(e => (e.FacilityType.Name == "HSI" || e.FacilityType.Name == "VRP"))
                .SelectMany(e => e.Events.Where(ev => ev.Active)
                .Select(ev => new EventReportDto(ev)))
                .ToListAsync();

            List<EventReportDto> reportDto;
              
            switch (eventReportType)
            {
                case EventReportType.Pending:
                    reportDto = facilityList.Where(e => e.Status == "Pending").ToList();
                    break;
                case EventReportType.Compliance:
                    reportDto = facilityList.Where(e => e.Status == "Compliance").ToList();
                    break;
                case EventReportType.Completed:
                    reportDto = facilityList.Where(e => e.Status == "Completed").ToList();
                    break;
                case EventReportType.CompletedOutstanding:
                    reportDto = facilityList.Where(e => e.Status == "CompletedOutstanding").ToList();
                    break;
                case EventReportType.CompletedByCO:
                    reportDto = facilityList.Where(e => e.Status == "CompletedByCO").ToList();
                    break;
                default:
                    reportDto = facilityList;
                    break;
            }

            return reportDto;
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
