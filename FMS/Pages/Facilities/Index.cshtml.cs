using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly ISelectListHelper _listHelper;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        public FacilitySpec Spec { get; set; }

        // List of facilities resulting from the search
        public IReadOnlyList<FacilitySummaryDto> FacilityList { get; set; }

        public int? CountyArg { get; set; }

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        // Shows text results <div> if any results return
        public bool ShowResults { get; set; }

        // Shows if no results in result set
        public bool ShowNone { get; set; }

        // Select Lists
        public SelectList Files { get; private set; }
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public SelectList States => new SelectList(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }
        // todo: Add a name property to COs
        public SelectList ComplianceOfficers { get; private set; }

        // TODO: Restore these after the DTOs are fully built:

        //public SelectList FileCabinets { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //Spec = new FacilitySpec() { State = "Georgia" };
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilitySpec spec)
        {
            // Get the list of facilities matching the "Spec" criteria
            FacilityList = await _repository.GetFacilityListAsync(spec);

            // Set "divs" based on search results
            if (FacilityList.Count() > 0)
            {
                ShowResults = true;
                ShowNone = false;
            }
            else
            {
                ShowResults = false;
                ShowNone = true;
            }

            await PopulateSelectsAsync();
            return Page();
        }

        private async Task PopulateSelectsAsync()
        {
            Files = await _listHelper.FilesSelectListAsync();
            BudgetCodes = await _listHelper.BudgetCodesSelectListAsync();
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            EnvironmentalInterests = await _listHelper.EnvironmentalInterestsSelectListAsync();
            FacilityStatuses = await _listHelper.FacilityStatusesSelectListAsync();
            FacilityTypes = await _listHelper.FacilityTypesSelectListAsync();
            OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
        }
    }
}