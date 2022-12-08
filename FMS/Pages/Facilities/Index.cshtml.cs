using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using DocumentFormat.OpenXml.Office2016.Excel;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using JetBrains.Annotations;
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
        [BindProperty]
        public FacilitySpec Spec { get; set; }

        // List of facilities resulting from the search
        public IPaginatedResult FacilityList { get; private set; }

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
            var fileName = $"FMS_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            // "FacilityReportList" Detailed Facility List to go to a report
            IReadOnlyList<FacilityDetailDto> facilityReportList = await _repository.GetFacilityDetailListAsync(Spec);
            var facilityDetailList = from p in facilityReportList select new 
            {
                FacilityNumber = p.FacilityNumber,
                FileLabel = p.FileLabel,
                Name = p.Name,
                Address = p.Address,
                City = p.City,
                County = p.County.Name,
                State = p.State,
                PostalCode = p.PostalCode,
                Location = p.Location,
                FacilityType = p.FacilityType.Name,
                ComplianceOfficer = p.ComplianceOfficer.Name,
                OrganizationalUnit = p.OrganizationalUnit.Name,
                BudgetCode = p.BudgetCode.Name,
                FacilityStatus = p.FacilityStatus.Name,
                CabinetsToString = p.CabinetsToString,
                RetentionRecords = p.RetentionRecordsToString
            };
            return File(facilityDetailList.ExportExcelAsByteArray(), "application/vnd.ms-excel", fileName);
        }

        private async Task PopulateSelectsAsync()
        {
            BudgetCodes = await _listHelper.BudgetCodesSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            FacilityStatuses = await _listHelper.FacilityStatusesSelectListAsync();
            FacilityTypes = await _listHelper.FacilityTypesSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
        }

    }
}