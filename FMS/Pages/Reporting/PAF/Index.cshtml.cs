using Dapper;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Infrastructure.Contexts;
using FMS.Pages.Maintenance;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Reporting.PAF
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;
        public IndexModel(IReportingRepository repository) => _repository = repository;

        public IReadOnlyList<PAFReportDto> PAFReport { get; set; }

        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                PAFReport = await _repository.GetPAFReportAsync();
                TempData?.SetDisplayMessage(Context.Success, "Successfully Loaded PAF Report");
                DisplayMessage = TempData?.GetDisplayMessage();
                return Page();
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Error loading PAF Report: {ex.Message}");
                DisplayMessage = TempData?.GetDisplayMessage();
                return Page();
            }
            
        }

        public async Task<IActionResult> OnPostExportButtonAsync()
        {
            try
            {
                var fileName = $"PAF_Report_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

                PAFReport = await _repository.GetPAFReportAsync();

                // Export to Excel
                return File(PAFReport.ExportExcelAsByteArray(ExportHelper.ReportType.EventPending), "application/vnd.ms-excel", fileName);
            }
            catch (Exception ex)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Export failed: {ex.Message}");
                return RedirectToPage("./Index");
            }
            finally
            {
                // Clear the PAFReport data to free up memory
                PAFReport = null;
                
            }
        }
    }
}
