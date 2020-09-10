using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using FMS.Helpers;
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
        private readonly ISelectListHelper _listHelper;

        [BindProperty]
        public FacilityEditDto Facility { get; set; }

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
        public SelectList ComplianceOfficers { get; private set; }

        public EditModel(
            IFacilityRepository repository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _listHelper = listHelper;
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

            // TODO #19: If editing facility number, make sure the new number doesn't already exist before trying to save. 

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

            TempData?.SetDisplayMessage(Context.Success, "Facility successfully updated.");
            return RedirectToPage("./Details", new { id = Id });
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