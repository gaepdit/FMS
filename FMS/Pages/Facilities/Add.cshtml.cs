using System;
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
        private readonly IFacilityTypeRepository _repositoryType;
        private readonly ISelectListHelper _listHelper;

        [BindProperty]
        public FacilityCreateDto Facility { get; set; }

        [BindProperty]
        public string ConfirmedFacilityFileLabel { get; set; }

        public bool ConfirmFacility { get; private set; }

        public IReadOnlyList<FacilityMapSummaryDto> NearbyFacilities { get; private set; }

        // Select Lists
        public static SelectList Counties => new(Data.Counties, "Id", "Name");
        public static SelectList States => new(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }


        // Add FacilityType Repo here, then use FacilityTypeName to compare for "RN" value
        public AddModel(
            IFacilityRepository repository,
            IFacilityTypeRepository repositoryType,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _repositoryType = repositoryType;
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
            Facility.FacilityTypeName = await _repositoryType.GetFacilityTypeNameAsync(Facility.FacilityTypeId);

            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            Facility.TrimAll();

            // Make sure Release Notifications have a "Date Received"
            if (Facility.FacilityTypeName == "RN" && Facility.RNDateReceived is null)
            {
                ModelState.AddModelError("Facility.RNDateReceived", "Date Received must be entered.");
            }

            // Make sure GeoCoordinates are withing the State of Georgia or both Zero
            GeoCoordHelper.CoordinateValidation EnumVal = GeoCoordHelper.ValidateCoordinates(Facility.Latitude, Facility.Longitude);
            string ValidationString = GeoCoordHelper.GetDescription(EnumVal);

            if (EnumVal != GeoCoordHelper.CoordinateValidation.Valid)
            {
                if (EnumVal == GeoCoordHelper.CoordinateValidation.LongNotInGeorgia)
                {
                    ModelState.AddModelError("Facility.Longitude", ValidationString);
                }
                else
                {
                    ModelState.AddModelError("Facility.Latitude", ValidationString);
                }
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

            bool newFileId = true;
            if (Facility.FileLabel == "none")
            {
                newFileId = false;
            }
            Facility.TrimAll();

            // If File Label is provided, make sure it exists
            if (!string.IsNullOrWhiteSpace(Facility.FileLabel) && Facility.FileLabel != "none" &&
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

            var newFacilityId = await _repository.CreateFacilityAsync(Facility, newFileId);

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