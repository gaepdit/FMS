using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FMS.Pages.Maintenance
{
    [Authorize(Roles = UserRoles.SiteMaintenance)]
    public class IndexModel : PageModel
    {
        private readonly IBudgetCodeRepository _budgetCodeRepository;
        private readonly IComplianceOfficerRepository _complianceOfficerRepository;
        private readonly IFacilityStatusRepository _facilityStatusRepository;
        private readonly IFacilityTypeRepository _facilityTypeRepository;
        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        public IReadOnlyList<FacilityStatusSummaryDto> FacilityStatuses { get; private set; }
        public IReadOnlyList<FacilityTypeSummaryDto> FacilityTypes { get; private set; }
        public IReadOnlyList<BudgetCodeSummaryDto> BudgetCodes { get; private set; }
        public IReadOnlyList<OrganizationalUnitSummaryDto> OrganizationalUnits { get; private set; }
        public IReadOnlyList<ComplianceOfficerSummaryDto> ComplianceOfficers { get; private set; }

        [Display(Name = "Select a Drop-Down Menu to Edit")]
        [BindProperty(SupportsGet = true)]
        public string MaintenanceSelection { get; set; }

        public DisplayMessage DisplayMessage { get; set; }

        public IndexModel(
            IBudgetCodeRepository budgetCodeRepository,
            IComplianceOfficerRepository complianceOfficerRepository,
            IFacilityStatusRepository facilityStatusRepository,
            IFacilityTypeRepository facilityTypeRepository,
            IOrganizationalUnitRepository organizationalUnitRepository)
        {
            _budgetCodeRepository = budgetCodeRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _facilityStatusRepository = facilityStatusRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
        }

        public IActionResult OnGet()
        {
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnGetSelectAsync()
        {
            await PopulateResultAsync();
            DisplayMessage = TempData?.GetDisplayMessage();
            return Page();
        }

        private async Task PopulateResultAsync()
        {
            switch (MaintenanceSelection)
            {
                case MaintenanceOptions.BudgetCode:
                    BudgetCodes = await _budgetCodeRepository.GetBudgetCodeListAsync();
                    break;
                case MaintenanceOptions.ComplianceOfficer:
                    ComplianceOfficers = await _complianceOfficerRepository.GetComplianceOfficerListAsync();
                    break;
                case MaintenanceOptions.FacilityStatus:
                    FacilityStatuses = await _facilityStatusRepository.GetFacilityStatusListAsync();
                    break;
                case MaintenanceOptions.FacilityType:
                    FacilityTypes = await _facilityTypeRepository.GetFacilityTypeListAsync();
                    break;
                case MaintenanceOptions.OrganizationalUnit:
                    OrganizationalUnits = await _organizationalUnitRepository.GetOrganizationalUnitListAsync();
                    break;
            }
        }
    }

    public static class MaintenanceOptions
    {
        public const string BudgetCode = "Budget Code";
        public const string ComplianceOfficer = "Compliance Officer";
        public const string FacilityStatus = "Facility Status";
        public const string FacilityType = "Type/Environmental Interest";
        public const string OrganizationalUnit = "Organizational Unit";
    }
}