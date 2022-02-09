using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly ISelectListHelper _listHelper;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        public FacilitySpec Spec { get; set; }

        // List of facilities resulting from the search
        public IPaginatedResult FacilityList { get; private set; }

        // Detailed Facility List to go to a report
        public IReadOnlyList<FacilityDetailDto> FacilityReportList { get; private set; }

        // Shows results section after searching
        public bool ShowResults { get; private set; }

        // Select Lists
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public SelectList States => new SelectList(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Spec = new FacilitySpec();
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilitySpec spec, [FromQuery] int p = 1)
        {
            // Get the list of facilities matching the "Spec" criteria
            FacilityList = await _repository.GetFacilityPaginatedListAsync(spec, p, GlobalConstants.PageSize);
            Spec = spec;
            ShowResults = true;
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            FacilityReportList = await _repository.GetFacilityDetailListAsync(Spec);
            return File(await FacilityReportList.GetCsvByteArrayAsync<FacilityReportMap>(), "text/csv",
                $"FMS_export_{DateTime.Now:yyyy-MM-dd.HH-mm-ss.FFF}.csv");
            //return RedirectToPage("../Reports/Index", spec);
        }

        private async Task PopulateSelectsAsync()
        {
            BudgetCodes = await _listHelper.BudgetCodesSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            FacilityStatuses = await _listHelper.FacilityStatusesSelectListAsync();
            FacilityTypes = await _listHelper.FacilityTypesSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
        }

        private sealed class FacilityReportMap : ClassMap<FacilityDetailDto>
        {
            public FacilityReportMap()
            {
                Map(m => m.FacilityNumber).Index(0).Name("Facility Number");
                Map(m => m.FileLabel).Index(1).Name("File Label");
                Map(m => m.Name).Index(2).Name("Facility Name");
                Map(m => m.Address).Index(3).Name("Street Address");
                Map(m => m.City).Index(4).Name("City");
                Map(m => m.County.Name).Index(5).Name("County");
                Map(m => m.State).Index(6).Name("State");
                Map(m => m.PostalCode).Index(7).Name("ZIP Code");
                Map(m => m.Location).Index(8).Name("Location Description");
                Map(m => m.FacilityType.Name).Index(9).Name("Type/Environmental Interest");
                Map(m => m.ComplianceOfficer.Name).Index(10).Name("Compliance Officer");
                Map(m => m.OrganizationalUnit.Name).Index(11).Name("Organizational Unit");
                Map(m => m.BudgetCode.Name).Index(12).Name("Budget Code");
                Map(m => m.FacilityStatus.Name).Index(13).Name("Facility Status");
                Map(m => m.CabinetsToString).Index(14).Name("Cabinets");
                Map(m => m.RetentionRecordsToString).Index(15).Name("Retention Records");
            }
        }
    }
}