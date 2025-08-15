using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.HsrpFacilityProperties
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IHsrpFacilityPropertiesRepository _repository;
        private readonly IFacilityRepository _facilityRepository;

        public EditModel(
            IHsrpFacilityPropertiesRepository repository, 
            IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        public HsrpFacilityPropertiesEditDto HsrpFacilityProperties { get; set; }

        public FacilityDetailDto Facility { get; set; }

        public string FacilityNumber { get; set; }

        public Guid Id { get; set; }

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

            FacilityNumber = Facility.FacilityNumber;


            return Page();
        }
    }
}
