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

namespace FMS.Pages.OnsiteScore
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IOnsiteScoreRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IOnsiteScoreRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public OnsiteScoreEditDto OnsiteScore { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList Chemicals { get; private set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            OnsiteScore = await _repository.GetOnsiteScoreByFacilityIdAsync(id);

            if (OnsiteScore == null)
            {
                OnsiteScoreCreateDto newOnsiteScore = new OnsiteScoreCreateDto() { FacilityId = id };
                Guid? newOnsiteScoreId = await _repository.CreateOnsiteScoreAsync(newOnsiteScore);
                if (newOnsiteScoreId != null)
                {
                    OnsiteScore = await _repository.GetOnsiteScoreByFacilityIdAsync(id);
                }
                else
                {
                    return NotFound();
                }
            }

            Facility = await _facilityRepository.GetFacilityAsync(OnsiteScore.FacilityId);
            
            if (OnsiteScore == null || Facility == null)
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

            try
            {
                await _repository.UpdateOnsiteScoreAsync(OnsiteScore);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save changes: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Onsite Score successfully Updated.");

            return RedirectToPage("../Facilities/Details", new { id = OnsiteScore.FacilityId, tab = "Score" });
        }

        private async Task PopulateSelectsAsync()
        {
            Chemicals = await _listHelper.ChemicalsSelectListAsync();
        }
    }
}
