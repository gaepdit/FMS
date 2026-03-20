using FMS.Domain.Dto.Reports;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.HSISiteLists
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public IndexModel(IReportingRepository repository) => _repository = repository;

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostByNumberAsync() 
        {
            var fileName = $"HSI_Sites_By_Number_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsPendingList" List to go to a report
            IReadOnlyList<HSIListReportDto> hsiReportList = await _repository.GetHSIListReportAsync(HSISortBy.HSINumber);

            // Export to Excel
            return File(hsiReportList.ExportExcelAsByteArray(ExportHelper.ReportType.HSIListByNumber), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByNameAsync()
        {
            var fileName = $"HSI_Sites_By_Name_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsPendingList" List to go to a report
            IReadOnlyList<HSIListReportDto> hsiReportList = await _repository.GetHSIListReportAsync(HSISortBy.Name);

            // Export to Excel
            return File(hsiReportList.ExportExcelAsByteArray(ExportHelper.ReportType.HSIListByName), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByCountyAsync()
        {
            var fileName = $"HSI_Sites_By_County_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsPendingList" List to go to a report
            IReadOnlyList<HSIListReportDto> hsiReportList = await _repository.GetHSIListReportAsync(HSISortBy.County);

            // Export to Excel
            return File(hsiReportList.ExportExcelAsByteArray(ExportHelper.ReportType.HSIListByCounty), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByClassAsync()
        {
            var fileName = $"HSI_Sites_By_Class_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "eventsPendingList" List to go to a report
            IReadOnlyList<HSIListReportDto> hsiReportList = await _repository.GetHSIListReportAsync(HSISortBy.ClassName);

            // Export to Excel
            return File(hsiReportList.ExportExcelAsByteArray(ExportHelper.ReportType.HSIListByClass), "application/vnd.ms-excel", fileName);
        }
    }
}
