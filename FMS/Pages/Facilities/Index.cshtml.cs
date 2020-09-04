using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using FMS.Domain.Repositories;
using FMS.Domain.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Facilities
{
    public class IndexModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        private readonly IFileRepository _fileRepository;

        private readonly ICountyRepository _countyRepository;

        private readonly IBudgetCodeRepository _budgetCodeRepository;

        private readonly IComplianceOfficerRepository _complianceOfficerRepository;

        private readonly IEnvironmentalInterestRepository _environmentalInterestRepository;

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        private readonly IFacilityTypeRepository _facilityTypeRepository;

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        // "Spec" is the Facility DTO bound to the HTML Page elements
        public FacilitySpec Spec { get; set; }

        // List of facilities resulting from the search
        public IReadOnlyList<FacilitySummaryDto> FacilityList {get; set;}

        public int? CountyArg { get; set; }

        // true when checkbox is checked to show only active sites
        [BindProperty]
        public bool ActiveOnly { get; set; }

        // Shows text results <div> if any results return
        public bool ShowResults { get; set; }

        // Shows if no results in result set
        public bool ShowNone { get; set; }

        public SelectList Files { get; set; }
        public SelectList Counties { get; private set; }
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }

        // todo: Add a name property to COs
        public SelectList ComplianceOfficers { get; set; }

        // TODO: Restore these after the DTOs are fully built:

        //public SelectList FileCabinets { get; private set; }

        public IndexModel(
            IFacilityRepository repository,
            IFileRepository fileRepository,
            ICountyRepository countyRepository,
            IBudgetCodeRepository budgetCodeRepository,
            IComplianceOfficerRepository complianceOfficerRepository,
            IEnvironmentalInterestRepository environmentalInterestRepository,
            IFacilityStatusRepository facilityStatusRepository,
            IFacilityTypeRepository facilityTypeRepository,
            IOrganizationalUnitRepository organizationalUnitRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
            _countyRepository = countyRepository;
            _budgetCodeRepository = budgetCodeRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _environmentalInterestRepository = environmentalInterestRepository;
            _facilityStatusRepository = facilityStatusRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetSearchAsync(FacilitySpec spec)
        {
            // TODO: check File number versus County ID number


            // Get the list of facilities matching the "Spec" criteria
            FacilityList = await _repository.GetFacilityListAsync(spec);
            
            // Set "divs" based on search results
            if(FacilityList.Count() > 0)
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
            Files = new SelectList(await _fileRepository.GetFileListAsync(CountyArg), "Id", "FileLabel");

            Counties = new SelectList(await _countyRepository.GetCountyListAsync(), "Id", "Name");

            BudgetCodes = new SelectList(await _budgetCodeRepository.GetBudgetCodeListAsync(), "Id", "Name");

            // need to get a Name property instead of Empl. Id. to Populate DropDown
            ComplianceOfficers = new SelectList(await _complianceOfficerRepository.GetComplianceOfficerListAsync(), "Id", "Name");

            EnvironmentalInterests = new SelectList(await _environmentalInterestRepository.GetEnvironmentalInterestListAsync(), "Id", "Name");

            FacilityStatuses = new SelectList(await _facilityStatusRepository.GetFacilityStatusListAsync(), "Id", "Status");

            FacilityTypes = new SelectList(await _facilityTypeRepository.GetFacilityTypeListAsync(), "Id", "Name");

            OrganizationalUnits = new SelectList(await _organizationalUnitRepository.GetOrganizationalUnitListAsync(), "Id", "Name");
        }
    }
}