using System.Threading.Tasks;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Facilities
{
    [Authorize(Policy = UserPolicies.FileCreatorOrEditor)]
    public class AddModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly ISelectListHelper _listHelper;

        [BindProperty]
        public FacilityCreateDto Facility { get; set; }

        public int? CountyArg { get; set; }

        // Select Lists
        public SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public SelectList States => new SelectList(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

        public AddModel(
            IFacilityRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await PopulateSelectsAsync();
            Facility = new FacilityCreateDto {State = "Georgia"};

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            Facility.TrimAll();

            // If File Label is provided, make sure it exists
            if (!string.IsNullOrWhiteSpace(Facility.FileLabel) &&
                !await _repository.FileLabelExists(Facility.FileLabel))
            {
                ModelState.AddModelError("Facility.FileLabel", "File Label entered does not exist.");
            }

            // When adding a new facility number, make sure the number doesn't already exist before trying to save.
            if (await _repository.FacilityNumberExists(Facility.FacilityNumber))
            {
                ModelState.AddModelError("Facility.FacilityNumber", "Facility Number entered already exists.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            var newFacilityId = await _repository.CreateFacilityAsync(Facility);

            TempData?.SetDisplayMessage(Context.Success, "Facility successfully created.");
            return RedirectToPage("./Details", new {id = newFacilityId});
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