using FMS.Domain.Dto;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.Score
{
    [Authorize(Roles = UserRoles.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IScoreRepository _repository;
        private readonly IFacilityRepository _facilityRepository;

        public EditModel(
            IScoreRepository repository,
            IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        [BindProperty]
        public ScoreEditDto Score { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        [TempData]
        public string ActiveTab { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            Id = id;

            Score = await _repository.GetScoreEditByFacilityIdAsync(id);
            if (Score == null)
            {
                ScoreCreateDto newScore = new ScoreCreateDto() { FacilityId = id };
                Guid? newScoreId = await _repository.CreateScoreAsync(newScore);
                if (newScoreId != null)
                {
                    Score = await _repository.GetScoreByIdAsync(newScoreId.Value);
                }
                else
                {
                    return NotFound();
                }
            }

            Facility = await _facilityRepository.GetFacilityAsync(Score.FacilityId);
            if (Facility == null)
            {
                return NotFound();
            }

            ActiveTab = "Score";
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _repository.UpdateScoreAsync(Score.FacilityId, Score);
            ActiveTab = "Score";
            return RedirectToPage("../Facilities/Details", new { id = Score.FacilityId, tab = ActiveTab });
        }

    }
}
