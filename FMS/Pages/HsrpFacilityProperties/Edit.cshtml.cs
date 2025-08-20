using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Entities;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.HsrpFacilityProperties
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IHsrpFacilityPropertiesRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IHsrpFacilityPropertiesRepository repository, 
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public HsrpFacilityPropertiesEditDto HsrpFacilityProperties { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList ComplianceOfficers { get; private set; }
        public SelectList OrganizationalUnit { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            HsrpFacilityProperties = await _repository.GetHsrpFacilityPropertiesEditAsync(id);
            if (HsrpFacilityProperties == null)
            {
                return NotFound();
            }

            Facility = await _facilityRepository.GetFacilityAsync(HsrpFacilityProperties.FacilityId);
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
           
            await _repository.UpdateHsrpFacilityPropertiesAsync(HsrpFacilityProperties.FacilityId, HsrpFacilityProperties);

            return RedirectToPage("../Facilities/Details", new { id = HsrpFacilityProperties.FacilityId });
        }

        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync();
            OrganizationalUnit = await _listHelper.OrganizationalUnitsSelectListAsync();
        }
    }
}
