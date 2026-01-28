using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.AssignmentList
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public IndexModel(IReportingRepository repository) => _repository = repository;

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostByCOAsync()
        {
            var fileName = $"Assignment_by_CO_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "assignmentByCOReportList" Detailed Facility List to go to a report
            IReadOnlyList<AssignmentListReportByCODto> assignmentByCOReportList = await _repository.GetAsignmentListByCOAsync();

            return File(assignmentByCOReportList.ExportExcelAsByteArray(ExportHelper.ReportType.None), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByCountyAsync()
        {
            var fileName = $"Assignment_by_County_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "assignmentByCountyReportList" Detailed Facility List to go to a report
            IReadOnlyList<AssignmentListReportByCountyDto> assignmentByCountyReportList = await _repository.GetAsignmentListByCountyAsync();

            return File(assignmentByCountyReportList.ExportExcelAsByteArray(ExportHelper.ReportType.None), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByHSIAsync()
        {
            var fileName = $"Assignment_by_HSI_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "assignmentByHSIReportList" Detailed Facility List to go to a report
            IReadOnlyList<AssignmentListReportByHSIDto> assignmentByHSIReportList = await _repository.GetAsignmentListByHSIAsync();

            return File(assignmentByHSIReportList.ExportExcelAsByteArray(ExportHelper.ReportType.None), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostBySiteNameAsync()
        {
            var fileName = $"Assignment_by_Site_Name_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "assignmentBySiteNameReportList" Detailed Facility List to go to a report
            IReadOnlyList<AssignmentListReportBySiteNameDto> assignmentBySiteNameReportList = await _repository.GetAsignmentListBySiteNameAsync();

            return File(assignmentBySiteNameReportList.ExportExcelAsByteArray(ExportHelper.ReportType.None), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByUnitAsync()
        {
            var fileName = $"Assignment_by_Unit_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "assignmentByUnitReportList" Detailed Facility List to go to a report
            IReadOnlyList<AssignmentListReportByUnitDto> assignmentByUnitReportList = await _repository.GetAsignmentListByUnitAsync();

            return File(assignmentByUnitReportList.ExportExcelAsByteArray(ExportHelper.ReportType.None), "application/vnd.ms-excel", fileName);
        }
    }
}
