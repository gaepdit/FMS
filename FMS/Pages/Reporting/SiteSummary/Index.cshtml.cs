using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using FMS.Infrastructure.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var fileName = $"FMS_Retention_Records_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.pdf";
            // "FacilityReportList" Detailed Retention Record List to export
            IEnumerable<SiteSummaryDto> siteSummaryList =
                await _repository.GetFacilitySiteSummaryDtoAsync(Spec);
            
            //var generatedFile =  File(ExportHelper.ExportPdfAsByteArray(siteSummaryList),
            //    "application/pdf", fileName);

            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync(true, ["Abandoned Sites", "Voluntary Remediation", "Response Development 1", "Response Development 2"]);
            LocationClasses = await _listHelper.LocationClassesSelectListAsync();
            AddlOrgUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
        }
    }
}
