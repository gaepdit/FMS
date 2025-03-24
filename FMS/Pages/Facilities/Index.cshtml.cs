using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Dto.PaginatedList;
using FMS.Domain.Repositories;
using FMS.Domain.Services;
using FMS.Helpers;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly IFacilityTypeRepository _repositoryType;
        private readonly ISelectListHelper _listHelper;
        private readonly IUserService _userService;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        [BindProperty]
        public FacilitySpec Spec { get; set; }

        // List of facilities resulting from the search
        public IPaginatedResult FacilityList { get; private set; }

        public DisplayMessage Message { get; private set; }

        // Shows results section after searching
        [BindProperty]
        public bool ShowResults { get; private set; }

        // Shows Checkbox for Pending RNs
        [BindProperty]
        public bool ShowPendingOnlyCheckBox { get; private set; }

        // Select Lists
        public SelectList Counties => new(Data.Counties, "Id", "Name");
        public SelectList States => new(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
            IFacilityTypeRepository repositoryType,
            ISelectListHelper listHelper,
            IUserService userService)
        {
            _repository = repository;
            _repositoryType = repositoryType;
            _listHelper = listHelper;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // First time through search will sort by name,
            // but for pending RNs will sort by ReceivedDate
            Spec = new FacilitySpec() { FirstPass = true };
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilitySpec spec, [FromQuery] int p = 1)
        {
            
            // Sort by Received Date for Pending Release Notifications
            if (spec.ShowPendingOnly && spec.FirstPass) 
            {
                spec.SortBy = FacilitySort.RNDateReceived;
                spec.FirstPass = false;
            };

            // Get the list of facilities matching the "Spec" criteria.
            FacilityList = await _repository.GetFacilityPaginatedListAsync(spec, p, GlobalConstants.PageSize);
            Spec = spec;
            
            ShowPendingOnlyCheckBox = await _repositoryType.GetFacilityTypeNameAsync(Spec.FacilityTypeId) == "RN";

            ShowResults = true;  
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostExportButtonAsync()
        {
            var fileName = $"FMS_Facility_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            // "FacilityReportList" Detailed Facility List to go to a report
            IReadOnlyList<FacilityDetailDto> facilityReportList = await _repository.GetFacilityDetailListAsync(Spec);
            var facilityDetailList = from p in facilityReportList select new FacilityDetailDtoScalar(p);
            return File(facilityDetailList.ExportExcelAsByteArray(ExportHelper.ReportType.Normal), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostPendingButtonAsync()
        {
            var fileName = $"FMS_PendingRN_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.xlsx";
            // "FacilityPendingList" Detailed Facility List to go to a report
            // sorted by Received Date
            IReadOnlyList<FacilityDetailDto> facilityReportList = await _repository.GetFacilityDetailListAsync(Spec);
            var facilityDetailList = from p in facilityReportList select new FacilityPendingDtoScalar(p);
            return File(facilityDetailList.ExportExcelAsByteArray(ExportHelper.ReportType.Pending), "application/vnd.ms-excel", fileName);
        }

        public async Task<IActionResult> OnPostDownloadRetentionRecordsAsync()
        {
            var currentUser = await _userService.GetCurrentUserAsync();
            var fileName = $"FMS_Retention_Records_export_{DateTime.Now:yyyy-MM-dd-HH-mm-ss.FFF}.pdf";
            // "FacilityReportList" Detailed Retention Record List to export
            IEnumerable<RetentionRecordDetailDto> retentionRecordDetailList =
                await _repository.GetRetentionRecordsListAsync(Spec);
            if (retentionRecordDetailList.Count() > 180)
            {
                TempData?.SetDisplayMessage(Context.Danger, "You have requested " + retentionRecordDetailList.Count() + " Retention Records, which is over the 180 allowed! Please narrow search results to send to PDF creator.");
                Message = TempData?.GetDisplayMessage();
                await PopulateSelectsAsync();
                return Page();
            }
            return File(ExportHelper.ExportPdfAsByteArray(retentionRecordDetailList, currentUser),
                "application/pdf", fileName);
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