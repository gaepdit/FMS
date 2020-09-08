using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class EditModel : PageModel
    {
        private readonly IFacilityRepository _repository;

        private readonly IFileRepository _fileRepository;

        private readonly IBudgetCodeRepository _budgetCodeRepository;

        private readonly IComplianceOfficerRepository _complianceOfficerRepository;

        private readonly IEnvironmentalInterestRepository _environmentalInterestRepository;

        private readonly IFacilityStatusRepository _facilityStatusRepository;

        private readonly IFacilityTypeRepository _facilityTypeRepository;

        private readonly IOrganizationalUnitRepository _organizationalUnitRepository;

        [BindProperty]
        public FacilityEditDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public int? CountyArg { get; set; }

        public SelectList Files { get; set; }
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public SelectList States => new SelectList(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList EnvironmentalInterests { get; private set; }

        // todo: Add a name property to COs
        public SelectList ComplianceOfficers { get; set; }

        // TODO: Restore these after the DTOs are fully built:

        //public SelectList FileCabinets { get; private set; }

        [TempData]
        public bool ShowSuccessMessage { get; set; }

        public EditModel(
            IFacilityRepository repository,
            IFileRepository fileRepository,
            IBudgetCodeRepository budgetCodeRepository,
            IComplianceOfficerRepository complianceOfficerRepository,
            IEnvironmentalInterestRepository environmentalInterestRepository,
            IFacilityStatusRepository facilityStatusRepository,
            IFacilityTypeRepository facilityTypeRepository,
            IOrganizationalUnitRepository organizationalUnitRepository)
        {
            _repository = repository;
            _fileRepository = fileRepository;
            _budgetCodeRepository = budgetCodeRepository;
            _complianceOfficerRepository = complianceOfficerRepository;
            _environmentalInterestRepository = environmentalInterestRepository;
            _facilityStatusRepository = facilityStatusRepository;
            _facilityTypeRepository = facilityTypeRepository;
            _organizationalUnitRepository = organizationalUnitRepository;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Id = id.Value;
            Facility = new FacilityEditDto(await _repository.GetFacilityAsync(id.Value));

            if (Facility == null)
            {
                return NotFound();
            }

            await PopulateSelectsAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            // TODO: If editing facility number, make sure the new number doesn't already exist
            // before trying to save.
            // Alternatively, prohibit editing facility number on this page, and add a separate
            // page to edit facility number.


            try
            {
                await _repository.UpdateFacilityAsync(Id, Facility);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FacilityExistsAsync(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            ShowSuccessMessage = true;
            return RedirectToPage("./Details", new { id = Id });
        }

        private async Task PopulateSelectsAsync()
        {
            Files = new SelectList(await _fileRepository.GetFileListAsync(CountyArg), "Id", "FileLabel");

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