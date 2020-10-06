using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IBudgetCodeRepository _budgetCodeRepository;

        private readonly IComplianceOfficerRepository _complianceOfficerRepository;

        private readonly IEnvironmentalInterestRepository _environmentalInterestRepository;

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        private readonly IFacilityTypeRepository _facilityTypeRepository;

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        
        private readonly ISelectListHelper _listHelper;

        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }
        public SelectList ComplianceOfficers { get; set; }

        public Guid BudgetCodeId { get; private set; }
        public Guid ComplianceOfficerId { get; set; }
        public Guid EnvironmentalInterestId { get; private set; }
        public Guid FacilityStatusId { get; private set; }
        public Guid FacilityTypeId { get; private set; }
        public Guid OrganizationalUnitId { get; private set; }
        
        [Display(Name = "Select a Drop-Down Menu to Edit")]
        [BindProperty(SupportsGet =true)]
        public int DropDownSelection { get; set; }

        public IndexModel(
            IFileRepository fileRepository,
            IBudgetCodeRepository budgetCodeRepository,
            IComplianceOfficerRepository complianceOfficerRepository,
            IEnvironmentalInterestRepository environmentalInterestRepository,
            IFacilityStatusRepository facilityStatusRepository,
            IFacilityTypeRepository facilityTypeRepository,
            IOrganizationalUnitRepository organizationalUnitRepository)
        {
            _fileRepository = fileRepository;
            _budgetCodeRepository = budgetCodeRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _environmentalInterestRepository = environmentalInterestRepository;
            _facilityStatusRepository = facilityStatusRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
        }

        public void OnGet()
        {
            //PopulateListBoxes();
            DropDownSelection = 0;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await PopulateResultAsync();
            return Page();
        }

        private async Task PopulateResultAsync()
        {
            switch (DropDownSelection)
            {
                case 1:
                    BudgetCodes = await _listHelper.BudgetCodesSelectListAsync();
                    break;
                case 2:
                    ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
                    break;
                case 3:
                    EnvironmentalInterests = await _listHelper.EnvironmentalInterestsSelectListAsync();
                    break;
                case 4:
                    FacilityStatuses = await _listHelper.FacilityStatusesSelectListAsync();
                    break;
                case 5:
                    FacilityTypes = await _listHelper.FacilityTypesSelectListAsync();
                    break;
                case 6:
                    OrganizationalUnits = await _listHelper.OrganizationalUnitsSelectListAsync();
                    break;
                default:
                    break;
            }
        }
    }
}