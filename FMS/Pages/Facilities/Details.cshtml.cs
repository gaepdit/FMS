using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Facilities
{
    public class DetailsModel : PageModel
    {
        private readonly IFacilityRepository _repository;
        public DetailsModel(IFacilityRepository repository) => _repository = repository;

        public FacilityDetailDto FacilityDetail { get; set; }
        public bool ShowSuccessMessage { get; set; } = false;

        public async Task<IActionResult> OnGetAsync(Guid? id, bool? success)
        {
            if (id == null)
            {
                return NotFound();
            }

            FacilityDetail = await _repository.GetFacilityAsync(id.Value);

            if (FacilityDetail == null)
            {
                return NotFound();
            }

            if (success.HasValue && success.Value) ShowSuccessMessage = true;
            return Page();
        }
    }
}