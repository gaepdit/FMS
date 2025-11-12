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

namespace FMS.Pages.GroundwaterScore
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IGroundwaterScoreRepository _repository;
        private readonly IFacilityRepository _facilityRepository;
        private readonly ISelectListHelper _listHelper;

        public EditModel(
            IGroundwaterScoreRepository repository,
            IFacilityRepository facilityRepository,
            ISelectListHelper listHelper)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
            _listHelper = listHelper;
        }

        [BindProperty]
        public GroundwaterScoreEditDto GroundwaterScore { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public SelectList Chemicals { get; private set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            GroundwaterScore = await _repository.GetGroundwaterScoreByFacilityIdAsync(id);
            if (GroundwaterScore == null)
            {
                GroundwaterScoreCreateDto newGroundwaterScore = new GroundwaterScoreCreateDto() { FacilityId = id };
                Guid? newGroundwaterScoreId = await _repository.CreateGroundwaterScoreAsync(newGroundwaterScore);
                if (newGroundwaterScoreId != null)
                {
                    GroundwaterScore = await _repository.GetGroundwaterScoreByFacilityIdAsync(id);
                }
                else
                {
                    return NotFound();
                }
            }

            Facility = await _facilityRepository.GetFacilityAsync(GroundwaterScore.FacilityId);

            if (Facility == null)
            {
                return NotFound();
            }
            await PopulateSelectsAsync();
            ActiveTab = "Score";
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
                await _repository.UpdateGroundwaterScoreAsync(GroundwaterScore);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save changes: {ex.Message}");
                await PopulateSelectsAsync();
                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Groundwater Score successfully updated.");
            ActiveTab = "Score";
            return RedirectToPage("../Facilities/Details", new { id = GroundwaterScore.FacilityId });
        }

        private async Task PopulateSelectsAsync()
        {
            Chemicals = await _listHelper.ChemicalsSelectListAsync();
        }
    }
}
