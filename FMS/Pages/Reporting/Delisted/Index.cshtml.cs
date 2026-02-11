using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.Today;
using Microsoft.Kiota.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.Delisted
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;

        public IndexModel(IReportingRepository repository) => _repository = repository;

        [Display(Name = "Start Date")]
        [BindProperty]
        public DateOnly? StartDate { get; set; } = null;

        [Display(Name = "End Date")]
        [BindProperty]
        public DateOnly? EndDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public void OnGet()
        {
            // Method intentionally left empty.
        }

        public async Task<IActionResult> OnPostByDateAsync()
        {
            var fileName = $"Delisted_by_Date_and_CO_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "delistedByDateReportList" Detailed Facility List to go to a report
            IReadOnlyList<DelistedReportByDateDto> delistedByDateReportList = await _repository.GetDelistedByDateAsync();

            return File(delistedByDateReportList.ExportExcelAsByteArray(ExportHelper.ReportType.Delisted), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostByDateRangeAsync()
        {
            var fileName = $"Delisted_by_Date_Range_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "delistedByDateRangeReportList" Detailed Facility List to go to a report
            IReadOnlyList<DelistedReportByDateRangeDto> delistedByDateRangeReportList = await _repository.GetDelistedByDateRangeAsync(StartDate, EndDate);

            return File(delistedByDateRangeReportList.ExportExcelAsByteArray(ExportHelper.ReportType.DelistedByRange, StartDate, EndDate), "application/vnd.ms-excel", fileName);
        }
    }
}
