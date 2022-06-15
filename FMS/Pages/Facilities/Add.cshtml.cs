using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
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

        [BindProperty]
        public string ConfirmedFacilityFileLabel { get; set; }

        public bool ConfirmFacility { get; private set; }
        public IReadOnlyList<FacilityMapSummaryDto> NearbyFacilities { get; private set; }

        // Select Lists
        public static SelectList Counties => new SelectList(Data.Counties, "Id", "Name");
        public static SelectList States => new SelectList(Data.States);
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

            // If Latitude is 0.00 then Longitude must also be 0.00
            if (!GeoCoordHelper.BothZeros((decimal)Facility.Latitude, (decimal)Facility.Longitude))
            {
                ModelState.AddModelError("Facility.Latitude", "Latitude and Longitude must both be zero(0) or both valid coordinates. ");
            }

            // If Latitude and/or Longitude fall outside State of Georgia, then alert user
            if (GeoCoordHelper.InvalidLat((decimal)Facility.Latitude))
            {
                ModelState.AddModelError("Facility.Latitude", "Latitude entered is outside State of Georgia. Must be between 30.36 and 35.0 North Latitude or zero if unknown.");
            }

            if (GeoCoordHelper.InvalidLong((decimal)Facility.Longitude))
            {
                ModelState.AddModelError("Facility.Longitude", "Longitude entered is outside State of Georgia. Must be between -80.84 and -85.61 West Longitude or zero if unknown.");
            }

            // If File Label is provided, make sure it exists
            if (!string.IsNullOrWhiteSpace(Facility.FileLabel))
            {
                if (!Domain.Entities.File.IsValidFileLabelFormat(Facility.FileLabel))
                {
                    ModelState.AddModelError("Facility.FileLabel", "File Label entered is not valid.");
                }
                else if (!await _repository.FileLabelExists(Facility.FileLabel))
                {
                    ModelState.AddModelError("Facility.FileLabel", "File Label entered does not exist.");
                }
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

            var mapSearchSpec = new FacilityMapSpec
            {
                Latitude = Facility.Latitude,
                Longitude = Facility.Longitude,
                Radius = 0.5m,
            };

            NearbyFacilities = await _repository.GetFacilityListAsync(mapSearchSpec);

            if (NearbyFacilities != null && NearbyFacilities.Count > 0)
            {
                ConfirmedFacilityFileLabel = Facility.FileLabel ?? string.Empty;
                await PopulateSelectsAsync();
                ConfirmFacility = true;
                return Page();
            }

            var newFacilityId = await _repository.CreateFacilityAsync(Facility);

            TempData?.SetDisplayMessage(Context.Success, "Facility successfully created.");
            return RedirectToPage("./Details", new {id = newFacilityId});
        }

        public async Task<IActionResult> OnPostConfirmAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            Facility.FileLabel = ConfirmedFacilityFileLabel;
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