using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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

        public IReadOnlyList<FacilityStatusSummaryDto> FacilityStatuses { get; private set; }
        public IReadOnlyList<FacilityTypeSummaryDto> FacilityTypes { get; private set; }
        public IReadOnlyList<BudgetCodeSummaryDto> BudgetCodes { get; private set; }
        public IReadOnlyList<OrganizationalUnitSummaryDto> OrganizationalUnits { get; private set; }
        public IReadOnlyList<EnvironmentalInterestSummaryDto> EnvironmentalInterests { get; private set; }
        public IReadOnlyList<ComplianceOfficerSummaryDto> ComplianceOfficers { get; set; }
        
        [Display(Name = "Select a Drop-Down Menu to Edit")]
        [BindProperty(SupportsGet =true)]
        public int DropDownSelection { get; set; }
        public string Message { get; set; }

        public IndexModel(
            IBudgetCodeRepository budgetCodeRepository,
            IComplianceOfficerRepository complianceOfficerRepository,
            IEnvironmentalInterestRepository environmentalInterestRepository,
            IFacilityStatusRepository facilityStatusRepository,
            IFacilityTypeRepository facilityTypeRepository,
            IOrganizationalUnitRepository organizationalUnitRepository)
        {
            _budgetCodeRepository = budgetCodeRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _environmentalInterestRepository = environmentalInterestRepository;
            _facilityStatusRepository = facilityStatusRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
        }

        public void OnGet()
        {
            DropDownSelection = 0;
            Message = "";
        }

        public async Task<IActionResult> OnGetSearchAsync()
        {
            await PopulateResultAsync();
            if(DropDownSelection == 0) { Message = "Please Make A Selection"; }
            return Page();
        }

        private async Task PopulateResultAsync()
        {
            switch (DropDownSelection)
            {
                case 1:
                    BudgetCodes = await _budgetCodeRepository.GetBudgetCodeListAsync();
                    break;
                case 2:
                    ComplianceOfficers = await _complianceOfficerRepository.GetComplianceOfficerListAsync();
                    break;
                case 3:
                    EnvironmentalInterests = await _environmentalInterestRepository.GetEnvironmentalInterestListAsync();
                    break;
                case 4:
                    FacilityStatuses = await _facilityStatusRepository.GetFacilityStatusListAsync();
                    break;
                case 5:
                    FacilityTypes = await _facilityTypeRepository.GetFacilityTypeListAsync();
                    break;
                case 6:
                    OrganizationalUnits = await _organizationalUnitRepository.GetOrganizationalUnitListAsync();
                    break;
                default:
                    break;
            }
        }
    }
}