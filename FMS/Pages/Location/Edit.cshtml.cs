using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Location
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly ILocationRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;
        public EditModel(
            ILocationRepository repository, 
            IFacilityRepository facilityRepository,
            ISelectListHelper selectListHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = selectListHelper;
        }

        [BindProperty]
        public LocationEditDto Location { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList Classes { get; set; }

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
            return RedirectToPage("../Facilities/Details", null, new { id = Location.FacilityId, tab = ActiveTab }, fragment: "TabPages");
        }

        private async Task PopulateSelectsAsync()
        {
            Classes = await _listHelper.LocationClassesSelectListAsync();
        }
    }
}
