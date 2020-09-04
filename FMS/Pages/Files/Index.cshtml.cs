using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Pages.Files
{
    public class IndexModel : PageModel
    {
        private readonly IFileRepository _repository;

        private readonly IFacilityRepository _facilityRepository;

        public IndexModel(
            IFileRepository repository,
            IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        public FileDetailDto FileDetail { get; set; }

        public FacilitySpec FacilitySpec { get; set; }

        public IReadOnlyList<FacilitySummaryDto> FacilityList { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            FileDetail = await _repository.GetFileAsync(id);

            if (FileDetail == null)
            {
                return NotFound();
            }

            FacilitySpec = new FacilitySpec() { FileId = id };

            await GetFacilities();

            return Page();
        }

        private async Task GetFacilities()
        {
            FacilityList = await _facilityRepository.GetFacilityListAsync(FacilitySpec);
        }
    }
}