using FMS.Domain.Data;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
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

        public EditModel(
            ILocationRepository repository, 
            IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        [BindProperty]
        public LocationEditDto Location { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList Classes => new(Data.Classes);

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

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _repository.UpdateLocationAsync(Location.FacilityId, Location);

            TempData?.SetDisplayMessage(Context.Success, $"Location successfully Updated.");

            return RedirectToPage("../Facilities/Details", new { id = Location.FacilityId, tab = "Location" });
        }
    }
}
