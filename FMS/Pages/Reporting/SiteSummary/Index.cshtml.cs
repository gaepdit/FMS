using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Reporting.SiteSummary
{
    public class IndexModel : PageModel
    {
        private readonly IReportingRepository _repository;
        private readonly ISelectListHelper _listHelper;

        public IndexModel(IReportingRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public SiteSummaryQuerySpec Spec { get; set; }

        [BindProperty]
        public IReadOnlyList<FacilityBasicDto> SummaryList { get; set; } = [];

        public bool ShowResults { get; private set; }

        // Select Lists
        public SelectList Counties => new(Data.Counties, "Id", "Name");
        public SelectList LocationClasses { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }
        public SelectList AddlOrgUnits { get; private set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Spec = new SiteSummaryQuerySpec();
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetPreviewAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;

            SummaryList = await _repository.GetHsiFacilitiesAsync(Spec);

            ShowResults = true;
            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync(true,
                ["Abandoned Sites", "Voluntary Remediation", "Response Development 1", "Response Development 2"]);
            LocationClasses = await _listHelper.LocationClassesSelectListAsync();
            AddlOrgUnits = await _listHelper.OrganizationalUnitsSelectListAsync(true,
            [
                "Remedial Sites 1", "Remedial Sites 2", "Remedial Sites 3", "DOD Facilities", "NPL Unit",
                "Treatment & Storage", "SW Env. Monitoring Compliance", "Voluntary Remediation"
            ]);
        }

        public async Task<IActionResult> OnPostExportButtonAsync()
        {
            var fileName = $"FMS_Site_Summary_List_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";

            // "FacilityReportList" Detailed Facility List to go to a report
            IReadOnlyList<SiteSummaryListDto> facilityReportList = await _repository.GetSiteSummaryListAsync(Spec);

            return File(facilityReportList.ExportExcelAsByteArray(ExportHelper.ReportType.SiteSummaryList), "application/vnd.ms-excel", fileName);
        }
    }
}
