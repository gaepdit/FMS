using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Admin
{
    [Authorize(Roles = UserConstants.AdminRole)]
    public class IndexModel : PageModel
    {
        private readonly IFileRepository _fileRepository;

        private readonly IBudgetCodeRepository _budgetCodeRepository;

        private readonly IComplianceOfficerRepository _complianceOfficerRepository;

        private readonly IEnvironmentalInterestRepository _environmentalInterestRepository;

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        private readonly IFacilityTypeRepository _facilityTypeRepository;

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        
        public SelectList Files { get; set; }
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }
        public SelectList ComplianceOfficers { get; set; }
        
        public Guid FileId { get; set; }
        public Guid FacilityStatusId { get; private set; }
        public Guid FacilityTypeId { get; private set; }
        public Guid BudgetCodeId { get; private set; }
        public Guid OrganizationalUnitId { get; private set; }
        public Guid EnvironmentalInterestId { get; private set; }
        public Guid ComplianceOfficerId { get; set; }
        
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

        public async Task<IActionResult> OnGetSearchAsync()
        {
            await PopulateResultAsync();
            return Page();
        }

        private async Task PopulateResultAsync()
        {
            switch (DropDownSelection)
            {
                case 1:
                    BudgetCodes = new SelectList(await _budgetCodeRepository.GetBudgetCodeListAsync(), "Id", "Name");
                    break;
                case 2:
                    ComplianceOfficers = new SelectList(await _complianceOfficerRepository.GetComplianceOfficerListAsync(), "Id", "Name");
                    break;
                case 3:
                    EnvironmentalInterests = new SelectList(await _environmentalInterestRepository.GetEnvironmentalInterestListAsync(), "Id", "Name");
                    break;
                case 4:
                    FacilityStatuses = new SelectList(await _facilityStatusRepository.GetFacilityStatusListAsync(), "Id", "Status");
                    break;
                case 5:
                    FacilityTypes = new SelectList(await _facilityTypeRepository.GetFacilityTypeListAsync(), "Id", "Name");
                    break;
                case 6:
                    Files = new SelectList(await _fileRepository.GetFileListAsync(null), "Id", "FileLabel");
                    break;
                case 7:
                    OrganizationalUnits = new SelectList(await _organizationalUnitRepository.GetOrganizationalUnitListAsync(), "Id", "Name");
                    break;
                default:
                    break;
            }
        }
    }
}