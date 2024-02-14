using System;
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
using Microsoft.EntityFrameworkCore;

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

            Id = id.Value;
            Facility = new FacilityEditDto(facilityDetail);

            await PopulateSelectsAsync();
            return Page();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Critical Code Smell", "S3776:Cognitive Complexity of methods should not be too high", Justification = "<Pending>")]
        public async Task<IActionResult> OnPostAsync()
        {
            Facility.FacilityTypeName = await _repositoryType.GetFacilityTypeNameAsync(Facility.FacilityTypeId);

            if (!ModelState.IsValid)
            {
                await PopulateSelectsAsync();
                return Page();
            }

            // Reload facility and see if it has been deleted by another user
            var facilityDetail = await _repository.GetFacilityAsync(Id);
            if (facilityDetail is not null && !facilityDetail.Active)
            {
                TempData?.SetDisplayMessage(Context.Danger, "Facility deleted by another user.");
                return RedirectToPage("./Details", new { Id });
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

                throw;
            }

            TempData?.SetDisplayMessage(Context.Success, "Facility successfully updated.");
            return RedirectToPage("./Details", new {id = Id});
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