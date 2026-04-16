using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FMS.Pages.Reporting.PAF
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;
        public IndexModel(IReportingRepository repository) => _repository = repository;

        public IReadOnlyList<PAFReportRawDto> PAFReportRaw { get; set; }

        public IList<PAFReportDto> PAFReport { get; set; }

        public decimal? PAFAmtTotal { get; set; } = 0;

        public int UniqueCO { get; set; }

        public int UniqueContractor { get; set; }

        public DisplayMessage DisplayMessage { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                PAFReportRaw = await _repository.GetPAFReportAsync();
                // Map to PAFReportDto
                PAFReport = PAFReportRaw.Select(raw => new PAFReportDto(raw)).ToList();

                // Get Column Totals
                IList<string> COList = [];
                IList<string> ContractorList = [];
                foreach (var p in PAFReport)
                {
                    PAFAmtTotal = PAFAmtTotal + p.PAFAmount;
                    if (!COList.Contains(p.ProjectOfficer))
                    {
                        COList.Add(p.ProjectOfficer);
                    }

                    if (!ContractorList.Contains(p.Contractor))
                    {
                        ContractorList.Add(p.Contractor);
                    }
                }

                UniqueCO = COList.Distinct().Count();
                UniqueContractor = ContractorList.Distinct().Count();

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

                PAFReportRaw = await _repository.GetPAFReportAsync();

                // Map to PAFReportDto
                PAFReport = PAFReportRaw.Select(raw => new PAFReportDto(raw)).ToList();

                // Export to Excel
                return File(PAFReport.ExportExcelAsByteArray(ExportHelper.ReportType.PAF), "application/vnd.ms-excel",
                    fileName);
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
