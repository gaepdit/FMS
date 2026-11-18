using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Helpers;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FMS.Pages.Location
{
    [Authorize(Policy = UserPolicies.FileEditorOrComplianceOfficer)]
    public class EditModel : PageModel
    {
        private readonly ILocationRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;
        private readonly IConfiguration _configuration;

        public EditModel(
            ILocationRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper selectListHelper,
            IConfiguration configuration)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = selectListHelper;
            _configuration = configuration;
        }

        [BindProperty]
        public LocationEditDto Location { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [BindProperty]
        public bool Edited { get; set; } 

        public SelectList Classes { get; set; }
        public SelectList MapTypes => new(Data.MapTypes);
        public SelectList MapZooms => new(Data.MapZooms);

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            Location = await _repository.GetLocationByFacilityIdAsync(id);
            if (Location == null)
            {
                LocationCreateDto newLocation = new LocationCreateDto() { FacilityId = Id };
                Guid? newLocationId = await _repository.CreateLocationAsync(newLocation);
                if (newLocationId != null)
                {
                    Location = await _repository.GetLocationByIdAsync(newLocationId);
                }
                else
                {
                    return NotFound();
                }
            }

            Facility = await _facilityRepository.GetFacilityAsync(Location.FacilityId);
            if (Facility == null)
            {
                return NotFound();
            }

            ActiveTab = "Location";
            await PopulateSelectsAsync();
            return Page();
        }

        public async Task<IActionResult> OnGetPreviewAsync(LocationEditDto location)
        {
            Location = location;
            Edited = true;
            Facility = await _facilityRepository.GetFacilityAsync(Location.FacilityId);
            if (Facility == null)
            {
                return NotFound();
            }

            ActiveTab = "Location";
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

            try
            {
                await _repository.UpdateLocationAsync(Location.FacilityId, Location);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save changes: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Location successfully updated.");
            ActiveTab = "Location";
            return RedirectToPage("../Facilities/Details", null, new { id = Location.FacilityId, tab = ActiveTab },
                fragment: "TabPages");
        }

        public async Task<IActionResult> OnGetImage(Guid id)
        {
            Facility = await _facilityRepository.GetFacilityAsync(id);
            var imageFile = GetMapImage(Facility);

            if (!string.IsNullOrWhiteSpace(imageFile))
            {
                return PhysicalFile(imageFile, "image/png");
            }
            return null;
        }

        public string GetMapImage(FacilityDetailDto facility)
        {
            var MapProps = new MapProperties(facility.FacilityNumber, facility.Latitude, facility.Longitude, facility.LocationDetails.MapZoom, facility.LocationDetails.MapType);

            return new MapHelper(_configuration).GetMapImage(MapProps);
        }

        private async Task PopulateSelectsAsync()
        {
            Classes = await _listHelper.LocationClassesSelectListAsync();
        }

        public async Task<IActionResult> OnGetMapImageAsync(Guid id, bool edited = false)
        {
            var location = await _repository.GetLocationByFacilityIdAsync(id);

            if (location == null)
                return NotFound();

            var facility = await _facilityRepository.GetFacilityAsync(id);

            if (facility == null)
                return NotFound();

            var mapProps = new MapProperties(
                facility.FacilityNumber,
                facility.Latitude,
                facility.Longitude,
                location.MapZoom,
                location.MapType);

            var mapHelper = new MapHelper(_configuration);

            var imagePath = mapHelper.GetStoredMapImagePath(mapProps);

            if (!System.IO.File.Exists(imagePath) || edited)
            {
                var success = await mapHelper.SaveStoredMapImage(mapProps);

                if (!success)
                    return NotFound("Unable to generate map image.");
            }

            if (!System.IO.File.Exists(imagePath))
                return NotFound();

            return PhysicalFile(imagePath, "image/png");
        }

    }
}
