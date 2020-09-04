using FMS.Domain.Repositories;
using FMS.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace FMS.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IFileRepository _fileRepository;

        private readonly IBudgetCodeRepository _budgetCodeRepository;

        private readonly IComplianceOfficerRepository _complianceOfficerRepository;

        private readonly IEnvironmentalInterestRepository _environmentalInterestRepository;

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        private readonly IFacilityTypeRepository _facilityTypeRepository;

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        private readonly IFileCabinetRepository _fileCabinetRepository;

        public SelectList Files { get; set; }
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }
        public SelectList ComplianceOfficers { get; set; }
        public SelectList FileCabinets { get; set; }

        public Guid FileId { get; set; }
        public Guid FacilityStatusId { get; private set; }
        public Guid FacilityTypeId { get; private set; }
        public Guid BudgetCodeId { get; private set; }
        public Guid OrganizationalUnitId { get; private set; }
        public Guid EnvironmentalInterestId { get; private set; }
        public Guid ComplianceOfficerId { get; set; }
        public Guid FileCabinetId { get; set; }


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
            IOrganizationalUnitRepository organizationalUnitRepository,
            IFileCabinetRepository fileCabinetRepository)
        {
            _fileRepository = fileRepository;
            _budgetCodeRepository = budgetCodeRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _environmentalInterestRepository = environmentalInterestRepository;
            _facilityStatusRepository = facilityStatusRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
            _fileCabinetRepository = fileCabinetRepository;
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
                    FileCabinets = new SelectList(await _fileCabinetRepository.GetFileCabinetListAsync(), "Id", "Name");
                    break;
                case 7:
                    Files = new SelectList(await _fileRepository.GetFileListAsync(null), "Id", "FileLabel");
                    break;
                case 8:
                    OrganizationalUnits = new SelectList(await _organizationalUnitRepository.GetOrganizationalUnitListAsync(), "Id", "Name");
                    break;
                default:
                    break;
            }
        }
    }
}