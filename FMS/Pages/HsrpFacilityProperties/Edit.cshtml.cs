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

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            HsrpFacilityProperties = await _repository.GetHsrpFacilityPropertiesEditByFacilityIdAsync(id);
            if (HsrpFacilityProperties == null)
            {
                HsrpFacilityPropertiesCreateDto newHsrp = new HsrpFacilityPropertiesCreateDto()
                {
                    FacilityId = id
                };
                Guid? newHsrpId = await _repository.CreateHsrpFacilityPropertiesAsync(newHsrp);
                if (newHsrpId != null)
                {
                    HsrpFacilityProperties = await _repository.GetHsrpFacilityPropertiesByIdAsync(newHsrpId);
                }
                else
                {
                    return NotFound();
                }
            }

            Facility = await _facilityRepository.GetFacilityAsync(HsrpFacilityProperties.FacilityId);
            if (Facility == null)
            {
                return NotFound();
            }

            await PopulateSelectsAsync();

            ActiveTab = "HSIProperties";

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

            TempData?.SetDisplayMessage(Context.Success, $"HSI Properties successfully Updated.");

            ActiveTab = "HSIProperties";

            return RedirectToPage("../Facilities/Details", new { id = HsrpFacilityProperties.FacilityId, tab = ActiveTab });
        }

        private async Task PopulateSelectsAsync()
        {
            ComplianceOfficers = await _listHelper.ComplianceOfficersSelectListAsync(true);
            OrganizationalUnit = await _listHelper.OrganizationalUnitsSelectListAsync();
        }
    }
}
