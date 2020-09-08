using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class AddModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly ISelectListHelper _listHelper;

        [BindProperty]
        public FacilityCreateDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public int? CountyArg { get; set; }

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
            Facility = new FacilityCreateDto { State = "Georgia" };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            await _repository.CreateFacilityAsync(Facility);

            return RedirectToPage("Details", new { id = Id, success = true });
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