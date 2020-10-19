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
        [BindProperty]
        public RetentionRecordCreateDto RecordCreate { get; set; }

        [BindProperty]
        [HiddenInput]
        public Guid FacilityId { get; set; }

        private readonly IFacilityRepository _repository;
        public DetailsModel(IFacilityRepository repository) => _repository = repository;

        public FacilityDetailDto FacilityDetail { get; set; }

        public DisplayMessage Message { get; set; }

        [TempData]
        public Guid HighlightRecord { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id, Guid? hr)
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

            if (hr.HasValue)
            {
                HighlightRecord = hr.Value;
            }

            FacilityId = FacilityDetail.Id;
            Message = TempData?.GetDisplayMessage();
            return Page();
        }

        public async Task<IActionResult> OnPostRetentionRecordAsync()
        {
            if (!ModelState.IsValid)
            {
                FacilityDetail = await _repository.GetFacilityAsync(FacilityId);

                if (FacilityDetail == null)
                {
                    return NotFound();
                }

                return Page();
            }

            RecordCreate.TrimAll();

            HighlightRecord = await _repository.CreateRetentionRecordAsync(FacilityId, RecordCreate);
            FacilityDetail = await _repository.GetFacilityAsync(FacilityId);

            if (FacilityDetail == null)
            {
                return NotFound();
            }

            return RedirectToPage();
        }
    }
}