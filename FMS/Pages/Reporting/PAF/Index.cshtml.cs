using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
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

        [BindProperty(SupportsGet = true)]
        public string SiteName { get; set; }


        [BindProperty(SupportsGet = true)]
        public string ProjectOfficer { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<string> Contractor { get; set; } = new List<string>();

        [BindProperty(SupportsGet = true)]
        public int? PAFIssueYear { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? PAFIssueDateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? PAFIssueDateTo { get; set; }

        public List<int> PAFIssueYearList { get; set; }
        public List<string> SiteNameList { get; set; }
        public List<string> ProjectOfficerList { get; set; }
        public List<string> ContractorList { get; set; }
        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {

                PAFIssueYearList = Enumerable.Range(2000, DateTime.Now.Year - 2000 + 1).Reverse().ToList();

                var data = await _repository.GetPAFReportAsync();

                SiteNameList = data.Select(p => p.SiteName).Where(s => !string.IsNullOrEmpty(s)).Distinct().OrderBy(s => s).ToList();

                ProjectOfficerList = data.Select(p => p.ProjectOfficer).Where(s => !string.IsNullOrEmpty(s)).Distinct().OrderBy(s => s).ToList();

                ContractorList = data.Select(p => p.Contractor).Where(s => !string.IsNullOrEmpty(s)).Distinct().OrderBy(s => s).ToList();

                await LoadDataAsync(data);

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

                var data = await _repository.GetPAFReportAsync();
                await LoadDataAsync(data);

                // Export to Excel
                return File(PAFReport.ExportExcelAsByteArray(ExportHelper.ReportType.PAF), "application/vnd.ms-excel", fileName);
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


        private async Task LoadDataAsync(IEnumerable<PAFReportDto> data)
        {
            PAFReport = data.Where(p => (string.IsNullOrEmpty(ProjectOfficer) || (!string.IsNullOrEmpty(p.ProjectOfficer) && p.ProjectOfficer.Contains(ProjectOfficer, StringComparison.OrdinalIgnoreCase))) &&
                                            (string.IsNullOrEmpty(SiteName) || (!string.IsNullOrEmpty(p.SiteName) && p.SiteName.Contains(SiteName, StringComparison.OrdinalIgnoreCase))) &&
                                              (Contractor == null || Contractor.Count == 0 || (!string.IsNullOrEmpty(p.Contractor) && Contractor.Contains(p.Contractor))) && // multi-select filter
                                            (!PAFIssueDateFrom.HasValue || (p.PAFIssueDate.HasValue && p.PAFIssueDate.Value.Date >= PAFIssueDateFrom.Value.Date)) &&
                                            (!PAFIssueDateTo.HasValue || (p.PAFIssueDate.HasValue && p.PAFIssueDate.Value.Date <= PAFIssueDateTo.Value.Date)) &&
                                            (!PAFIssueYear.HasValue || (p.PAFIssueDate.HasValue && p.PAFIssueDate.Value.Year == PAFIssueYear.Value))
                                            ).ToList();

        }
    }
}
