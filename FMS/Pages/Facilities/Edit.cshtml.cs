using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using FMS.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FMS.Pages.Facilities
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        private readonly IFacilityTypeRepository _repositoryType;
        private readonly ISelectListHelper _listHelper;

        [BindProperty]
        public FacilityEditDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public string ConfirmedFacilityFileLabel { get; set; }

        public bool ConfirmFacility { get; private set; }

        [BindProperty]
        public bool IsNotSiteMaintenanceUser { get; set; }

        public IReadOnlyList<FacilityMapSummaryDto> NearbyFacilities { get; private set; }

        // Select Lists
        public static SelectList Counties => new(Data.Counties, "Id", "Name");
        public static SelectList States => new(Data.States);
        public SelectList FacilityStatuses { get; private set; }
        public SelectList FacilityTypes { get; private set; }
        public SelectList BudgetCodes { get; private set; }
        public SelectList OrganizationalUnits { get; private set; }
        public SelectList ComplianceOfficers { get; private set; }

        public EditModel(
            IFacilityRepository repository,
            IFacilityTypeRepository repositoryType,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _repositoryType = repositoryType;
            _listHelper = listHelper;
        }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var facilityDetail = await _repository.GetFacilityAsync(id.Value);

            if (facilityDetail == null)
            {
                return NotFound();
            }

            if (!facilityDetail.Active)
            {
                return RedirectToPage("./Details", new {id});
            }

            IsNotSiteMaintenanceUser = !User.IsInRole(UserRoles.SiteMaintenance);
            Id = id.Value;
            Facility = new FacilityEditDto(facilityDetail);

            await PopulateSelectsAsync();
            return Page();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "<Pending>")]
        public async Task<IActionResult> OnPostAsync()
        {
            // jQuery Validation of Edit Form
            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            Facility.TrimAll();
            
            // Reload facility and see if it has been deleted by another user
            FacilityDetailDto facilityDetail = await _repository.GetFacilityAsync(Id);
            if (facilityDetail is not null && !facilityDetail.Active)
            {
                TempData?.SetDisplayMessage(Context.Danger, "Facility has been deleted by another user.");
                return RedirectToPage("./Details", new { Id });
            }

            // Validate User input based on Business Logic
            // Populate FacilityTypeName to use for User Input validity
            Facility.FacilityTypeName = await _repositoryType.GetFacilityTypeNameAsync(Facility.FacilityTypeId);
            ModelErrorCollection errors = FormValidationHelper.ValidateFacilityEditForm(Facility);
            if (errors.Count > 0)
            {
                foreach (ModelError error in errors)
                {
                    string[] errMsg = error.ErrorMessage.Split("^");
                    ModelState.AddModelError(errMsg[0].ToString(), errMsg[1].ToString());
                }
            }

            // If new File Label is provided, make sure it exists
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

            try
            {
                await _repository.UpdateFacilityAsync(Id, Facility);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _repository.FacilityExistsAsync(Id))
                {
                    // Facility not found in DB
                    TempData?.SetDisplayMessage(Context.Danger, "Unable to update Facility. Does not exist in Database or connection issues.");
                    return RedirectToPage("./Index");
                }
                // Facility found in DB, but unable to update
                TempData?.SetDisplayMessage(Context.Danger, "Unable to update Facility. Database connection or data issue. Check connection and try again.");
                return RedirectToPage("./Details", new { Id });
            }

            TempData?.SetDisplayMessage(Context.Success, "Facility successfully updated.");
            return RedirectToPage("./Details", new {id = Id});
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

            // Validate User input based on Business Logic
            // Populate FacilityTypeName to use for User Input validity
            Facility.FacilityTypeName = await _repositoryType.GetFacilityTypeNameAsync(Facility.FacilityTypeId);

            ModelErrorCollection errors = FormValidationHelper.ValidateFacilityEditForm(Facility);
            if (errors.Count > 0)
            {
                foreach (ModelError error in errors)
                {
                    string[] errMsg = error.ErrorMessage.Split("^");
                    ModelState.AddModelError(errMsg[0].ToString(), errMsg[1].ToString());
                }
            }

            // If File Label is provided, make sure it exists
            if (!string.IsNullOrWhiteSpace(Facility.FileLabel) && Facility.FileLabel != "none" &&
                !await _repository.FileLabelExists(Facility.FileLabel))
            {
                ModelState.AddModelError("Facility.FileLabel", "File Label entered does not exist.");
            }

            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            Facility.FacilityTypeName = await _repositoryType.GetFacilityTypeNameAsync(Facility.FacilityTypeId);

            await _repository.UpdateFacilityAsync(Id, Facility, newFileId);

            TempData?.SetDisplayMessage(Context.Success, "Facility successfully updated.");
            return RedirectToPage("./Details", new { id = Id });
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