using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Entities.Users;
using FMS.Domain.Repositories;
using FMS.Platform.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace FMS.Pages.GroundwaterScore
{
    [Authorize(Policy = UserPolicies.FileEditor)]
    public class EditModel : PageModel
    {
        private readonly IGroundwaterScoreRepository _repository;
        private readonly IFacilityRepository _facilityRepository;

        public EditModel(
            IGroundwaterScoreRepository repository,
            IFacilityRepository facilityRepository)
        {
            _repository = repository;
            _facilityRepository = facilityRepository;
        }

        [BindProperty]
        public GroundwaterScoreEditDto GroundwaterScore { get; set; }

        public FacilityDetailDto Facility { get; set; }

        [BindProperty]
        public Guid Id { get; set; }

        public Chemical Chemical { get; private set; }

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
           
            if (GroundwaterScore.SubstanceId == null)
            {
                TempData?.SetDisplayMessage(Context.Danger, $"Substance for Groundwater Scoring has not been chosen. Please choose a Substance for Groundwater Scoring.");
                ActiveTab = "Substances";
                return RedirectToPage("../Facilities/Details", new { id });
            }

            Chemical = GroundwaterScore.Substance?.Chemical;

            GroundwaterScore.ChemName = Chemical?.ChemicalName;
            GroundwaterScore.CASNO = Chemical?.CasNo;
            GroundwaterScore.Other = Chemical?.CommonName;

            Facility = await _facilityRepository.GetFacilityAsync(GroundwaterScore.FacilityId);

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
            try
            {
                await _repository.UpdateGroundwaterScoreAsync(GroundwaterScore);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save changes: {ex.Message}");

                return Page();
            }

            TempData?.SetDisplayMessage(Context.Success, $"Groundwater Score successfully updated.");
            ActiveTab = "Score";
            return RedirectToPage("../Facilities/Details", new { id = GroundwaterScore.FacilityId, tab = ActiveTab });
        }
    }
}
