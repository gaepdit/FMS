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
            Facility = new FacilityCreateDto { State = "Georgia", NonHSILetterDate = DateOnly.FromDateTime(DateTime.Now) };

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