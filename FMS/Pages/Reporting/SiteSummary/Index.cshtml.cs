using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IActionResult> OnGetReportAsync(SiteSummaryQuerySpec spec)
        {
            Spec = spec;
            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync(true, new List<string> { "Abandoned Sites", "Voluntary Remediation", "Response Development 1", "Response Development 2" });
            LocationClasses = await _listHelper.LocationClassesSelectListAsync();
            AddlOrgUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
        }
    }
}
